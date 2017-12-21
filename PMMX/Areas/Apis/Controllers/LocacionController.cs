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
using PMMX.Modelo.Entidades.Warehouse;

namespace Sitio.Areas.Apis.Controllers
{
    public class LocacionController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Locacion
        public IQueryable<Locacion> GetLocacion()
        {
            return db.Locacion;
        }

        // GET: api/Locacion/5
        [ResponseType(typeof(Locacion))]
        public IHttpActionResult GetLocacion(int id)
        {
            Locacion locacion = db.Locacion.Find(id);
            if (locacion == null)
            {
                return NotFound();
            }

            return Ok(locacion);
        }

        // PUT: api/Locacion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocacion(int id, Locacion locacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locacion.Id)
            {
                return BadRequest();
            }

            db.Entry(locacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacionExists(id))
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

        // POST: api/Locacion
        [ResponseType(typeof(Locacion))]
        public IHttpActionResult PostLocacion(Locacion locacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locacion.Add(locacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = locacion.Id }, locacion);
        }

        // DELETE: api/Locacion/5
        [ResponseType(typeof(Locacion))]
        public IHttpActionResult DeleteLocacion(int id)
        {
            Locacion locacion = db.Locacion.Find(id);
            if (locacion == null)
            {
                return NotFound();
            }

            db.Locacion.Remove(locacion);
            db.SaveChanges();

            return Ok(locacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocacionExists(int id)
        {
            return db.Locacion.Count(e => e.Id == id) > 0;
        }
    }
}