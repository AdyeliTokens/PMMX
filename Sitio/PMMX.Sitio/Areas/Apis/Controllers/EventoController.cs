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

        // GET: api/GembaWalk/5
        [ResponseType(typeof(EventoView))]
        public IHttpActionResult GetEventobyResponsableporDias(int idResponsable, int idCategoria, int dias, bool activo)
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
                     IdCategoria = s.Evento.IdCategoria,
                     Activo = s.Evento.Activo
                 }).ToList();

            if( idCategoria !=0 )
            {
                evento = evento.Where(e => (e.IdCategoria == idCategoria)).ToList();
            }
            
            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [ResponseType(typeof(EventoView))]
        public IHttpActionResult GetEventobyResponsableporFecha(int idResponsable, int idCategoria, DateTime fecha, bool activo)
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
                     IdCategoria = s.Evento.IdCategoria,
                     Activo = s.Evento.Activo
                 }).ToList();

            if (idCategoria != 0)
            {
                evento = evento.Where(e => (e.IdCategoria == idCategoria)).ToList();
            }

            if (evento == null)
            {
                return NotFound();
            }

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
                    IdCategoria = d.IdCategoria,
                    Activo = d.Activo
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

            if (evento.Nota == null)
                evento.Nota = " ";

            evento.FechaInicio = DateTime.Now;
            evento.FechaFin = DateTime.Now;
            evento.Activo = true;
            
            db.Evento.Add(evento);
            db.SaveChanges();

            try
            {
                NotificationService notify = new NotificationService();
                UsuarioServicio usuarioServicio = new UsuarioServicio();

                List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(evento.Id);
                List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                foreach (string notificacion in llaves)
                {
                    notify.SendPushNotification(notificacion, "Se le ha asignado un nuevo evento: " + evento.Descripcion + ". ", "");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return Ok(evento);
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