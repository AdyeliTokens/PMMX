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
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis
{
    public class EstatusController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Estatus
        public IQueryable<Estatus> GetEstatus()
        {
            return db.Estatus;
        }

        // GET: api/Estatus/5
        [ResponseType(typeof(Estatus))]
        public IHttpActionResult GetEstatus(int id)
        {
            Estatus Estatus = db.Estatus.Find(id);
            if (Estatus == null)
            {
                return NotFound();
            }

            return Ok(Estatus);
        }

        [ResponseType(typeof(Estatus))]
        public IHttpActionResult GetEstatusbyCategoria(int idCategoria)
        {
            var Estatus = db.Estatus
                .Where(e => (e.IdCategoria == idCategoria))
                .Select(e => new EstatusView
                {
                    Id = e.Id,
                    IdCategoria = e.IdCategoria,
                    Nombre = e.Nombre,
                    Activo = e.Activo
                }).ToList();

            if (Estatus == null)
            {
                return NotFound();
            }

            return Ok(Estatus);
        }

        // PUT: api/Estatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstatus(int id, Estatus Estatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Estatus.Id)
            {
                return BadRequest();
            }

            db.Entry(Estatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstatusExists(id))
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

        // POST: api/Estatus
        [ResponseType(typeof(Estatus))]
        public IHttpActionResult PostEstatus(Estatus Estatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Estatus.Add(Estatus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Estatus.Id }, Estatus);
        }

        // DELETE: api/Estatus/5
        [ResponseType(typeof(Estatus))]
        public IHttpActionResult DeleteEstatus(int id)
        {
            Estatus Estatus = db.Estatus.Find(id);
            if (Estatus == null)
            {
                return NotFound();
            }

            db.Estatus.Remove(Estatus);
            db.SaveChanges();

            return Ok(Estatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstatusExists(int id)
        {
            return db.Estatus.Count(e => e.Id == id) > 0;
        }
    }
}