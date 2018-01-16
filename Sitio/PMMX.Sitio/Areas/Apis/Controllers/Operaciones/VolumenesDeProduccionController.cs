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
    public class VolumenesDeProduccionController : ApiController
    {
        private PMMXContext db = new PMMXContext();
        
        public IQueryable<VolumenDeProduccion> GetVolumenesDeProduccion()
        {
            return db.VolumenesDeProduccion;
        }

        [ResponseType(typeof(VolumenDeProduccion))]
        public IHttpActionResult GetVolumenDeProduccion(int id)
        {
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return NotFound();
            }

            return Ok(volumenDeProduccion);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutVolumenDeProduccion(int id, VolumenDeProduccion volumenDeProduccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volumenDeProduccion.Id)
            {
                return BadRequest();
            }

            db.Entry(volumenDeProduccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolumenDeProduccionExists(id))
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

        [ResponseType(typeof(VolumenDeProduccion))]
        public IHttpActionResult PostVolumenDeProduccion(VolumenDeProduccion volumenDeProduccion)
        {
            volumenDeProduccion.Fecha = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VolumenesDeProduccion.Add(volumenDeProduccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = volumenDeProduccion.Id }, volumenDeProduccion);
        }

        [ResponseType(typeof(VolumenDeProduccion))]
        public IHttpActionResult DeleteVolumenDeProduccion(int id)
        {
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return NotFound();
            }

            db.VolumenesDeProduccion.Remove(volumenDeProduccion);
            db.SaveChanges();

            return Ok(volumenDeProduccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VolumenDeProduccionExists(int id)
        {
            return db.VolumenesDeProduccion.Count(e => e.Id == id) > 0;
        }
    }
}