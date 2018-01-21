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
    public class ActividadEnVentanaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Rechazos
        public IQueryable<Rechazos> GetActividadEnVentana()
        {
            return db.Rechazos;
        }

        // GET: api/Rechazos/5
        [ResponseType(typeof(Rechazos))]
        public IHttpActionResult GetActividadEnVentana(int id)
        {
            Rechazos Rechazos = db.Rechazos.Find(id);
            if (Rechazos == null)
            {
                return NotFound();
            }

            return Ok(Rechazos);
        }

        // PUT: api/Rechazos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActividadEnVentana(int id, Rechazos Rechazos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Rechazos.Id)
            {
                return BadRequest();
            }

            db.Entry(Rechazos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadEnVentanaExists(id))
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

        // POST: api/Rechazos
        [ResponseType(typeof(Rechazos))]
        public IHttpActionResult PostActividadEnVentana(Rechazos Rechazos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rechazos.Add(Rechazos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Rechazos.Id }, Rechazos);
        }

        // DELETE: api/Rechazos/5
        [ResponseType(typeof(Rechazos))]
        public IHttpActionResult DeleteActividadEnVentana(int id)
        {
            Rechazos Rechazos = db.Rechazos.Find(id);
            if (Rechazos == null)
            {
                return NotFound();
            }

            db.Rechazos.Remove(Rechazos);
            db.SaveChanges();

            return Ok(Rechazos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActividadEnVentanaExists(int id)
        {
            return db.Rechazos.Count(e => e.Id == id) > 0;
        }
    }
}