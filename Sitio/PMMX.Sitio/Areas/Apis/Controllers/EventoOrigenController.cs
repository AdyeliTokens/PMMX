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
using PMMX.Modelo.Vistas;
using PMMX.Modelo.Entidades.Operaciones;

namespace Sitio.Areas.Apis.Controllers
{
    public class EventoOrigenController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/EventoOrigen
        public IQueryable<EventoOrigen> GetEventoOrigen()
        {
            return db.EventoOrigen;
        }

        // GET: api/EventoOrigen/5
        [ResponseType(typeof(EventoOrigen))]
        public IHttpActionResult GetEventoOrigen(int id)
        {
            EventoOrigen eventoOrigen = db.EventoOrigen.Find(id);
            if (eventoOrigen == null)
            {
                return NotFound();
            }

            return Ok(eventoOrigen);
        }
        
        // PUT: api/EventoOrigen/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventoOrigen(int id, EventoOrigen eventoOrigen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventoOrigen.Id)
            {
                return BadRequest();
            }

            db.Entry(eventoOrigen).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoOrigenExists(id))
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

        // POST: api/EventoOrigen
        [ResponseType(typeof(EventoOrigen))]
        public IHttpActionResult PostEventoOrigen(EventoOrigen eventoOrigen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventoOrigen.Add(eventoOrigen);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventoOrigen.Id }, eventoOrigen);
        }

        // DELETE: api/EventoOrigen/5
        [ResponseType(typeof(EventoOrigen))]
        public IHttpActionResult DeleteEventoOrigen(int id)
        {
            EventoOrigen eventoOrigen = db.EventoOrigen.Find(id);
            if (eventoOrigen == null)
            {
                return NotFound();
            }

            db.EventoOrigen.Remove(eventoOrigen);
            db.SaveChanges();

            return Ok(eventoOrigen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoOrigenExists(int id)
        {
            return db.EventoOrigen.Count(e => e.Id == id) > 0;
        }
    }
}