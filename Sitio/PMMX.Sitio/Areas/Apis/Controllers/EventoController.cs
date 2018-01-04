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
using Sitio.Helpers;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    public class EventoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Evento
        public IQueryable<Evento> GetEvento()
        {
            return db.Evento;
        }

        // GET: api/JustDoIt/5
        [ResponseType(typeof(EventoView))]
        public IHttpActionResult GetEventobyResponsableandFecha(int idResponsable, int dias, bool activo)
        {
            var today = DateTime.Now.Date;
            DateTime dateInit = today.AddDays(dias);

            var evento = db.EventoResponsable
                 .Where(e => (e.IdResponsable == idResponsable) && (e.Evento.FechaInicio >= dateInit) && (e.Evento.Activo == activo))
                 .Select(s => new EventoView
                 {
                     Id = s.Evento.Id,
                     Descripcion = s.Evento.Descripcion,
                     IdAsignador = s.Evento.IdAsignador,
                     FechaInicio = s.Evento.FechaInicio,
                     FechaFin = s.Evento.FechaFin,
                     Nota = s.Evento.Nota,
                     Activo = s.Evento.Activo
                 }).ToList();
            
            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [ResponseType(typeof(ModuloSeccionView))]
        public IHttpActionResult GetEventobyResponsableandFecha(int idResponsable, DateTime fecha, bool activo)
        {
            DateTime hoy = fecha.Date;
            DateTime mañana = hoy.AddDays(1);
            
               var evento = db.EventoResponsable
                 .Where(d => (d.IdResponsable == idResponsable) && (d.Evento.FechaInicio >= hoy && d.Evento.FechaInicio <= mañana) && (d.Evento.Activo == activo))
                 .Select(s => new EventoView
                 {
                     Id = s.Evento.Id,
                     Descripcion = s.Evento.Descripcion,
                     IdAsignador = s.Evento.IdAsignador,
                     FechaInicio = s.Evento.FechaInicio,
                     FechaFin = s.Evento.FechaFin,
                     Nota = s.Evento.Nota,
                     Activo = s.Evento.Activo
                 }).ToList();

            return Ok(evento);
        }

        // GET: api/Evento/5
        [ResponseType(typeof(EventoView))]
        public IHttpActionResult GetEvento(int id)
        {
            var evento = db.Evento.Where(e => (e.Id == id))
                .Select(d => new EventoView
                {
                    Id = d.Id,
                    Descripcion = d.Descripcion,
                    FechaInicio = d.FechaInicio,
                    FechaFin = d.FechaFin,
                    Nota = d.Nota,
                    Activo = d.Activo,
                    JustDoIt = d.JustDoIt.Select(j => new JustDoItView
                    {
                        Id = j.Id,
                        IdEvento = j.IdEvento,
                        IdOrigen = j.IdOrigen,
                        Descripcion = j.Descripcion,
                        IdReportador = j.IdReportador,
                        IdResponsable = j.IdResponsable
                    }).ToList()
                })
                .ToList();

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        // PUT: api/Evento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvento(int id, Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento.Id)
            {
                return BadRequest();
            }

            db.Entry(evento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Evento
        [ResponseType(typeof(Evento))]
        public IHttpActionResult PostEvento(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Evento.Add(evento);
            db.SaveChanges();

            NotificationService notify = new NotificationService();
            UsuarioServicio usuarioServicio = new UsuarioServicio();

            List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(evento.Id);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
               notify.SendPushNotification(notificacion, "Se le ha asignado un nuevo evento: " + evento.Descripcion + ". ", "");
            }

            return CreatedAtRoute("DefaultApi", new { id = evento.Id }, evento);
        }

        // DELETE: api/Evento/5
        [ResponseType(typeof(Evento))]
        public IHttpActionResult DeleteEvento(int id)
        {
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return NotFound();
            }

            db.Evento.Remove(evento);
            db.SaveChanges();

            return Ok(evento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoExists(int id)
        {
            return db.Evento.Count(e => e.Id == id) > 0;
        }
    }
}