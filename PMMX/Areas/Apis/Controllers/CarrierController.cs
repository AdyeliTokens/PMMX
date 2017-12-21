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
    public class CarrierController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Carrier
        public IQueryable<Carrier> GetCarrier()
        {
            return db.Carrier;
        }

        // GET: api/Carrier/5
        [ResponseType(typeof(Carrier))]
        public IHttpActionResult GetCarrier(int id)
        {
            Carrier carrier = db.Carrier.Find(id);
            if (carrier == null)
            {
                return NotFound();
            }

            return Ok(carrier);
        }

        // PUT: api/Carrier/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarrier(int id, Carrier carrier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carrier.Id)
            {
                return BadRequest();
            }

            db.Entry(carrier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrierExists(id))
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

        // POST: api/Carrier
        [ResponseType(typeof(Carrier))]
        public IHttpActionResult PostCarrier(Carrier carrier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carrier.Add(carrier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carrier.Id }, carrier);
        }

        // DELETE: api/Carrier/5
        [ResponseType(typeof(Carrier))]
        public IHttpActionResult DeleteCarrier(int id)
        {
            Carrier carrier = db.Carrier.Find(id);
            if (carrier == null)
            {
                return NotFound();
            }

            db.Carrier.Remove(carrier);
            db.SaveChanges();

            return Ok(carrier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarrierExists(int id)
        {
            return db.Carrier.Count(e => e.Id == id) > 0;
        }
    }
}