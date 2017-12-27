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
using PMMX.Modelo.Entidades;

namespace Sitio.Areas
{
    public class RemitentesController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Remitentes
        public IQueryable<Remitentes> GetRemitentes()
        {
            return db.Remitentes;
        }

        // GET: api/Remitentes/5
        [ResponseType(typeof(Remitentes))]
        public IHttpActionResult GetRemitentes(int id)
        {
            Remitentes Remitentes = db.Remitentes.Find(id);
            if (Remitentes == null)
            {
                return NotFound();
            }

            return Ok(Remitentes);
        }

        // PUT: api/Remitentes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRemitentes(int id, Remitentes Remitentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Remitentes.Id)
            {
                return BadRequest();
            }

            db.Entry(Remitentes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemitentesExists(id))
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

        // POST: api/Remitentes
        [ResponseType(typeof(Remitentes))]
        public IHttpActionResult PostRemitentes(Remitentes Remitentes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Remitentes.Add(Remitentes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Remitentes.Id }, Remitentes);
        }

        // DELETE: api/Remitentes/5
        [ResponseType(typeof(Remitentes))]
        public IHttpActionResult DeleteRemitentes(int id)
        {
            Remitentes Remitentes = db.Remitentes.Find(id);
            if (Remitentes == null)
            {
                return NotFound();
            }

            db.Remitentes.Remove(Remitentes);
            db.SaveChanges();

            return Ok(Remitentes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RemitentesExists(int id)
        {
            return db.Remitentes.Count(e => e.Id == id) > 0;
        }
    }
}