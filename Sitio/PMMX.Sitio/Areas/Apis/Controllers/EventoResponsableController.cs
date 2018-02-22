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

namespace Sitio.Areas.Apis.Controllers
{
    public class EventoResponsableController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/EventoResponsable
        public IQueryable<EventoResponsable> GetEventoResponsable()
        {
            return db.EventoResponsable;
        }

        // GET: api/EventoResponsable/5
        [ResponseType(typeof(EventoResponsable))]
        public IHttpActionResult GetEventoResponsable(int id)
        {
            EventoResponsable eventoResponsable = db.EventoResponsable.Find(id);
            if (eventoResponsable == null)
            {
                return NotFound();
            }

            return Ok(eventoResponsable);
        }

        // PUT: api/EventoResponsable/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventoResponsable(int id, EventoResponsable eventoResponsable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventoResponsable.Id)
            {
                return BadRequest();
            }

            db.Entry(eventoResponsable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoResponsableExists(id))
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

        // POST: api/EventoResponsable
        [ResponseType(typeof(EventoResponsable))]
        public IHttpActionResult PostEventoResponsable(EventoResponsable eventoResponsable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventoResponsable.Add(eventoResponsable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventoResponsable.Id }, eventoResponsable);
        }

        // DELETE: api/EventoResponsable/5
        [ResponseType(typeof(EventoResponsable))]
        public IHttpActionResult DeleteEventoResponsable(int id)
        {
            EventoResponsable eventoResponsable = db.EventoResponsable.Find(id);
            if (eventoResponsable == null)
            {
                return NotFound();
            }

            db.EventoResponsable.Remove(eventoResponsable);
            db.SaveChanges();

            return Ok(eventoResponsable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoResponsableExists(int id)
        {
            return db.EventoResponsable.Count(e => e.Id == id) > 0;
        }
    }
}