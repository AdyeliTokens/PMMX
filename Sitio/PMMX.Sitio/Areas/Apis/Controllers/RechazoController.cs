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
    public class RechazoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Rechazo
        public IQueryable<Rechazo> GetRechazo()
        {
            return db.Rechazo;
        }

        // GET: api/Rechazo/5
        [ResponseType(typeof(Rechazo))]
        public IHttpActionResult GetRechazo(int id)
        {
            Rechazo Rechazo = db.Rechazo.Find(id);
            if (Rechazo == null)
            {
                return NotFound();
            }

            return Ok(Rechazo);
        }

        // PUT: api/Rechazo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRechazo(int id, Rechazo Rechazo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Rechazo.Id)
            {
                return BadRequest();
            }

            db.Entry(Rechazo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RechazoExists(id))
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

        // POST: api/Rechazo
        [ResponseType(typeof(Rechazo))]
        public IHttpActionResult PostRechazo(Rechazo Rechazo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rechazo.Add(Rechazo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Rechazo.Id }, Rechazo);
        }

        // DELETE: api/Rechazo/5
        [ResponseType(typeof(Rechazo))]
        public IHttpActionResult DeleteRechazo(int id)
        {
            Rechazo Rechazo = db.Rechazo.Find(id);
            if (Rechazo == null)
            {
                return NotFound();
            }

            db.Rechazo.Remove(Rechazo);
            db.SaveChanges();

            return Ok(Rechazo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RechazoExists(int id)
        {
            return db.Rechazo.Count(e => e.Id == id) > 0;
        }
    }
}