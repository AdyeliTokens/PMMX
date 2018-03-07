using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using PMMX.Seguridad.Servicios;
using Sitio.Helpers;

namespace Sitio.Areas.Apis
{
    public class StatusVentanaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/StatusVentana
        public IQueryable<StatusVentana> GetStatusVentana()
        {
            return db.StatusVentana;
        }

        // GET: api/StatusVentana/5
        [ResponseType(typeof(StatusVentana))]
        public IHttpActionResult GetStatusVentana(int id)
        {
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            if (statusVentana == null)
            {
                return NotFound();
            }

            return Ok(statusVentana);
        }

        // PUT: api/StatusVentana/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStatusVentana(int id, StatusVentana statusVentana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statusVentana.Id)
            {
                return BadRequest();
            }

            db.Entry(statusVentana).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusVentanaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StatusVentana
        [ResponseType(typeof(StatusVentana))]
        public IHttpActionResult PostStatusVentana(StatusVentana statusVentana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var IdSubCategoria = db.Ventana.Where(x => x.Id == statusVentana.IdVentana).Select(x => x.IdSubCategoria).FirstOrDefault();

            WorkFlowServicio workflowServicio = new WorkFlowServicio();
            IRespuestaServicio<WorkFlowView> workFlow = workflowServicio.nextEstatus(IdSubCategoria, statusVentana.IdStatus, false);

            statusVentana.IdStatus = workFlow.Respuesta.EstatusSiguiente.Id;
            statusVentana.Fecha = DateTime.Now;
            db.StatusVentana.Add(statusVentana);
            db.SaveChanges();

            var ventana = db.Ventana
                        .Include(v => v.StatusVentana)
                        .Include(v => v.StatusVentana.Select(s => s.Status))
                        .Include(v => v.BitacoraVentana)
                        .Include(v => v.BitacoraVentana.Select(b => b.Estatus))
                        .Include(v => v.BitacoraVentana.Select(b => b.Rechazo))
                        .Include(v => v.Evento)
                        .Where(x => x.Id == statusVentana.IdVentana)
                        .FirstOrDefault();

            try
            {
                UsuarioServicio usuarioServicio = new UsuarioServicio();
                NotificationService notify = new NotificationService();

                string senders = usuarioServicio.GetEmailByEvento(ventana.IdEvento);

                if (senders != "")
                {
                    EmailService emailService = new EmailService();
                    emailService.SendMail(senders, ventana);
                }

                List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(statusVentana.Ventana.IdEvento);
                List<string> llaves = dispositivos.Select(x => x.Llave).ToList();
                var estatus = ventana.StatusVentana.OrderByDescending(s => s.Fecha).Select(s => s.Status).FirstOrDefault();

                if (llaves.Count > 0)
                {
                    foreach (string notificacion in llaves)
                    {
                        notify.SendPushNotification(notificacion, " Cambio de estatus Ventana: " + ventana.Evento.Descripcion + ". ", " Cambio de estatus a " + estatus.Nombre);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            StatusVentana statusVentanaAdded = db.StatusVentana.Find(statusVentana.Id);

            return Ok(statusVentanaAdded);
        }

        // DELETE: api/StatusVentana/5
        [ResponseType(typeof(StatusVentana))]
        public IHttpActionResult DeleteStatusVentana(int id)
        {
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            if (statusVentana == null)
            {
                return NotFound();
            }

            db.StatusVentana.Remove(statusVentana);
            db.SaveChanges();

            return Ok(statusVentana);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatusVentanaExists(int id)
        {
            return db.StatusVentana.Count(e => e.Id == id) > 0;
        }
    }
}