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
using PMMX.Modelo.Entidades;

namespace Sitio.Areas.Apis.Controllers.Seguridad
{
    public class EntornoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Entorno
        public IQueryable<Entorno> GetEntorno()
        {
            return db.Entornos;
        }

        // GET: api/Entorno/5
        [ResponseType(typeof(Entorno))]
        public IHttpActionResult GetEntorno(int id)
        {
            Entorno entorno = db.Entornos.Find(id);
            if (entorno == null)
            {
                return NotFound();
            }

            return Ok(entorno);
        }

        // PUT: api/Entorno/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntorno(int id, Entorno entorno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entorno.Id)
            {
                return BadRequest();
            }

            db.Entry(entorno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntornoExists(id))
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

        // POST: api/Entorno
        [ResponseType(typeof(Entorno))]
        public IHttpActionResult PostEntorno(Entorno entorno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entornos.Add(entorno);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = entorno.Id }, entorno);
        }

        // DELETE: api/Entorno/5
        [ResponseType(typeof(Entorno))]
        public IHttpActionResult DeleteEntorno(int id)
        {
            Entorno entorno = db.Entornos.Find(id);
            if (entorno == null)
            {
                return NotFound();
            }

            db.Entornos.Remove(entorno);
            db.SaveChanges();

            return Ok(entorno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntornoExists(int id)
        {
            return db.Entornos.Count(e => e.Id == id) > 0;
        }
    }
}