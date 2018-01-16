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

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class DesperdiciosController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        public IQueryable<Desperdicio> GetDesperdicios()
        {
            return db.Desperdicios;
        }

        [ResponseType(typeof(Desperdicio))]
        public IHttpActionResult GetDesperdicio(int id)
        {
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return NotFound();
            }

            return Ok(desperdicio);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutDesperdicio(int id, Desperdicio desperdicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != desperdicio.Id)
            {
                return BadRequest();
            }

            db.Entry(desperdicio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesperdicioExists(id))
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

        [ResponseType(typeof(Desperdicio))]
        public IHttpActionResult PostDesperdicio(Desperdicio desperdicio)
        {
            desperdicio.Fecha = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Desperdicios.Add(desperdicio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = desperdicio.Id }, desperdicio);
        }

        [ResponseType(typeof(Desperdicio))]
        public IHttpActionResult DeleteDesperdicio(int id)
        {
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return NotFound();
            }

            db.Desperdicios.Remove(desperdicio);
            db.SaveChanges();

            return Ok(desperdicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DesperdicioExists(int id)
        {
            return db.Desperdicios.Count(e => e.Id == id) > 0;
        }
    }
}