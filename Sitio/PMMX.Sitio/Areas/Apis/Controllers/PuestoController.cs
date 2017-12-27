using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using System.Web.Http;
using PMMX.Modelo.Entidades;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace Sitio.Areas.Seguridad.Controllers
{
    public class PuestoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Puesto
        public IQueryable<Puesto> GetPuesto()
        {
            return db.Puestos;
        }

        // GET: api/Puesto/5
        [ResponseType(typeof(Puesto))]
        public IHttpActionResult GetPuesto(int id)
        {
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }

            return Ok(puesto);
        }

        // PUT: api/Puesto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPuesto(int id, Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != puesto.Id)
            {
                return BadRequest();
            }

            db.Entry(puesto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoExists(id))
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

        // POST: api/Puesto
        [ResponseType(typeof(Puesto))]
        public IHttpActionResult PostPuesto(Puesto puesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puestos.Add(puesto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = puesto.Id }, puesto);
        }

        // DELETE: api/Puesto/5
        [ResponseType(typeof(Puesto))]
        public IHttpActionResult DeletePuesto(int id)
        {
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return NotFound();
            }

            db.Puestos.Remove(puesto);
            db.SaveChanges();

            return Ok(puesto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PuestoExists(int id)
        {
            return db.Puestos.Count(e => e.Id == id) > 0;
        }
    }
}