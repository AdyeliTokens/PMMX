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

        // GET: api/ActividadEnVentana
        public IQueryable<ActividadEnVentana> GetActividadEnVentana()
        {
            return db.ActividadEnVentana;
        }

        // GET: api/ActividadEnVentana/5
        [ResponseType(typeof(ActividadEnVentana))]
        public IHttpActionResult GetActividadEnVentana(int id)
        {
            ActividadEnVentana actividadEnVentana = db.ActividadEnVentana.Find(id);
            if (actividadEnVentana == null)
            {
                return NotFound();
            }

            return Ok(actividadEnVentana);
        }

        // PUT: api/ActividadEnVentana/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActividadEnVentana(int id, ActividadEnVentana actividadEnVentana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actividadEnVentana.Id)
            {
                return BadRequest();
            }

            db.Entry(actividadEnVentana).State = EntityState.Modified;

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

        // POST: api/ActividadEnVentana
        [ResponseType(typeof(ActividadEnVentana))]
        public IHttpActionResult PostActividadEnVentana(ActividadEnVentana actividadEnVentana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActividadEnVentana.Add(actividadEnVentana);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actividadEnVentana.Id }, actividadEnVentana);
        }

        // DELETE: api/ActividadEnVentana/5
        [ResponseType(typeof(ActividadEnVentana))]
        public IHttpActionResult DeleteActividadEnVentana(int id)
        {
            ActividadEnVentana actividadEnVentana = db.ActividadEnVentana.Find(id);
            if (actividadEnVentana == null)
            {
                return NotFound();
            }

            db.ActividadEnVentana.Remove(actividadEnVentana);
            db.SaveChanges();

            return Ok(actividadEnVentana);
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
            return db.ActividadEnVentana.Count(e => e.Id == id) > 0;
        }
    }
}