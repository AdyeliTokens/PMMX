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
    public class BitacoraVentanaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/BitacoraVentana
        public IQueryable<BitacoraVentana> GetBitacoraVentana()
        {
            return db.BitacoraVentana;
        }

        // GET: api/BitacoraVentana/5
        [ResponseType(typeof(BitacoraVentana))]
        public IHttpActionResult GetBitacoraVentana(int id)
        {
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return NotFound();
            }

            return Ok(bitacoraVentana);
        }

        // PUT: api/BitacoraVentana/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBitacoraVentana(int id, BitacoraVentana bitacoraVentana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bitacoraVentana.Id)
            {
                return BadRequest();
            }

            db.Entry(bitacoraVentana).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraVentanaExists(id))
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

        // POST: api/BitacoraVentana
        [ResponseType(typeof(BitacoraVentana))]
        public IHttpActionResult PostBitacoraVentana(BitacoraVentana bitacoraVentana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BitacoraVentana.Add(bitacoraVentana);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bitacoraVentana.Id }, bitacoraVentana);
        }

        // DELETE: api/BitacoraVentana/5
        [ResponseType(typeof(BitacoraVentana))]
        public IHttpActionResult DeleteBitacoraVentana(int id)
        {
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return NotFound();
            }

            db.BitacoraVentana.Remove(bitacoraVentana);
            db.SaveChanges();

            return Ok(bitacoraVentana);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BitacoraVentanaExists(int id)
        {
            return db.BitacoraVentana.Count(e => e.Id == id) > 0;
        }
    }
}