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
    public class TipoOperacionController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/TipoOperacion
        public IQueryable<TipoOperacion> GetTipoOperacions()
        {
            return db.TipoOperacions;
        }

        // GET: api/TipoOperacion/5
        [ResponseType(typeof(TipoOperacion))]
        public IHttpActionResult GetTipoOperacion(int id)
        {
            TipoOperacion tipoOperacion = db.TipoOperacions.Find(id);
            if (tipoOperacion == null)
            {
                return NotFound();
            }

            return Ok(tipoOperacion);
        }

        // PUT: api/TipoOperacion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoOperacion(int id, TipoOperacion tipoOperacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoOperacion.Id)
            {
                return BadRequest();
            }

            db.Entry(tipoOperacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoOperacionExists(id))
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

        // POST: api/TipoOperacion
        [ResponseType(typeof(TipoOperacion))]
        public IHttpActionResult PostTipoOperacion(TipoOperacion tipoOperacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoOperacions.Add(tipoOperacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoOperacion.Id }, tipoOperacion);
        }

        // DELETE: api/TipoOperacion/5
        [ResponseType(typeof(TipoOperacion))]
        public IHttpActionResult DeleteTipoOperacion(int id)
        {
            TipoOperacion tipoOperacion = db.TipoOperacions.Find(id);
            if (tipoOperacion == null)
            {
                return NotFound();
            }

            db.TipoOperacions.Remove(tipoOperacion);
            db.SaveChanges();

            return Ok(tipoOperacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoOperacionExists(int id)
        {
            return db.TipoOperacions.Count(e => e.Id == id) > 0;
        }
    }
}