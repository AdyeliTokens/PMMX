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

            db.StatusVentana.Add(statusVentana);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = statusVentana.Id }, statusVentana);
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