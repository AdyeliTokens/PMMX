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
using PMMX.Modelo.Entidades.Maquinaria;

namespace Sitio.Areas.Apis.Controllers
{
    public class ModuloController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Modulo
        public IQueryable<Modulo> GetModulo()
        {
            return db.Modulos;
        }

        // GET: api/Modulo/5
        [ResponseType(typeof(Modulo))]
        public IHttpActionResult GetModulo(int id)
        {
            Modulo modulo = db.Modulos.Find(id);
            if (modulo == null)
            {
                return NotFound();
            }

            return Ok(modulo);
        }


        // PUT: api/Modulo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModulo(int id, Modulo modulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modulo.Id)
            {
                return BadRequest();
            }

            db.Entry(modulo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuloExists(id))
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

        // POST: api/Modulo
        [ResponseType(typeof(Modulo))]
        public IHttpActionResult PostModulo(Modulo modulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Modulos.Add(modulo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = modulo.Id }, modulo);
        }

        // DELETE: api/Modulo/5
        [ResponseType(typeof(Modulo))]
        public IHttpActionResult DeleteModulo(int id)
        {
            Modulo modulo = db.Modulos.Find(id);
            if (modulo == null)
            {
                return NotFound();
            }

            db.Modulos.Remove(modulo);
            db.SaveChanges();

            return Ok(modulo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModuloExists(int id)
        {
            return db.Modulos.Count(e => e.Id == id) > 0;
        }
    }
}