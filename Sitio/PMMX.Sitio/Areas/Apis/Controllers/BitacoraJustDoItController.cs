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

namespace Sitio.Areas.Apis.Controllers
{
    public class BitacoraJustDoItController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/BitacoraJustDoIt
        public IQueryable<BitacoraJustDoIt> GetBitacoraJustDoIts()
        {
            return db.BitacoraJustDoIts;
        }

        // GET: api/BitacoraJustDoIt/5
        [ResponseType(typeof(BitacoraJustDoIt))]
        public IHttpActionResult GetBitacoraJustDoIt(int id)
        {
            BitacoraJustDoIt bitacoraJustDoIt = db.BitacoraJustDoIts.Find(id);
            if (bitacoraJustDoIt == null)
            {
                return NotFound();
            }

            return Ok(bitacoraJustDoIt);
        }

        // PUT: api/BitacoraJustDoIt/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBitacoraJustDoIt(int id, BitacoraJustDoIt bitacoraJustDoIt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bitacoraJustDoIt.Id)
            {
                return BadRequest();
            }

            db.Entry(bitacoraJustDoIt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraJustDoItExists(id))
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

        // POST: api/BitacoraJustDoIt
        [ResponseType(typeof(BitacoraJustDoIt))]
        public IHttpActionResult PostBitacoraJustDoIt(BitacoraJustDoIt bitacoraJustDoIt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BitacoraJustDoIts.Add(bitacoraJustDoIt);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bitacoraJustDoIt.Id }, bitacoraJustDoIt);
        }

        // DELETE: api/BitacoraJustDoIt/5
        [ResponseType(typeof(BitacoraJustDoIt))]
        public IHttpActionResult DeleteBitacoraJustDoIt(int id)
        {
            BitacoraJustDoIt bitacoraJustDoIt = db.BitacoraJustDoIts.Find(id);
            if (bitacoraJustDoIt == null)
            {
                return NotFound();
            }

            db.BitacoraJustDoIts.Remove(bitacoraJustDoIt);
            db.SaveChanges();

            return Ok(bitacoraJustDoIt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BitacoraJustDoItExists(int id)
        {
            return db.BitacoraJustDoIts.Count(e => e.Id == id) > 0;
        }
    }
}