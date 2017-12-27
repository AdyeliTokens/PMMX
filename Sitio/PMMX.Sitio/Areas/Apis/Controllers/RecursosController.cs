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
    public class RecursosController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Recursos
        public IQueryable<Recursos> GetRecursos()
        {
            return db.Recursos;
        }

        // GET: api/Recursos/5
        [ResponseType(typeof(Recursos))]
        public IHttpActionResult GetRecursos(int id)
        {
            Recursos recursos = db.Recursos.Find(id);
            if (recursos == null)
            {
                return NotFound();
            }

            return Ok(recursos);
        }

        // PUT: api/Recursos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecursos(int id, Recursos recursos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recursos.Id)
            {
                return BadRequest();
            }

            db.Entry(recursos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecursosExists(id))
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

        // POST: api/Recursos
        [ResponseType(typeof(Recursos))]
        public IHttpActionResult PostRecursos(Recursos recursos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recursos.Add(recursos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recursos.Id }, recursos);
        }

        // DELETE: api/Recursos/5
        [ResponseType(typeof(Recursos))]
        public IHttpActionResult DeleteRecursos(int id)
        {
            Recursos recursos = db.Recursos.Find(id);
            if (recursos == null)
            {
                return NotFound();
            }

            db.Recursos.Remove(recursos);
            db.SaveChanges();

            return Ok(recursos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecursosExists(int id)
        {
            return db.Recursos.Count(e => e.Id == id) > 0;
        }
    }
}