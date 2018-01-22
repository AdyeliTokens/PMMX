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
    public class BitacoraGembaWalkController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/BitacoraGembaWalk
        public IQueryable<BitacoraGembaWalk> GetBitacoraJustDoIts()
        {
            return db.BitacoraGembaWalks;
        }

        // GET: api/BitacoraGembaWalk/5
        [ResponseType(typeof(BitacoraGembaWalk))]
        public IHttpActionResult GetBitacoraJustDoIt(int id)
        {
            BitacoraGembaWalk BitacoraGembaWalk = db.BitacoraGembaWalks.Find(id);
            if (BitacoraGembaWalk == null)
            {
                return NotFound();
            }

            return Ok(BitacoraGembaWalk);
        }

        // PUT: api/BitacoraGembaWalk/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBitacoraJustDoIt(int id, BitacoraGembaWalk BitacoraGembaWalk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != BitacoraGembaWalk.Id)
            {
                return BadRequest();
            }

            db.Entry(BitacoraGembaWalk).State = EntityState.Modified;

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

        // POST: api/BitacoraGembaWalk
        [ResponseType(typeof(BitacoraGembaWalk))]
        public IHttpActionResult PostBitacoraJustDoIt(BitacoraGembaWalk BitacoraGembaWalk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BitacoraGembaWalks.Add(BitacoraGembaWalk);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = BitacoraGembaWalk.Id }, BitacoraGembaWalk);
        }

        // DELETE: api/BitacoraGembaWalk/5
        [ResponseType(typeof(BitacoraGembaWalk))]
        public IHttpActionResult DeleteBitacoraJustDoIt(int id)
        {
            BitacoraGembaWalk BitacoraGembaWalk = db.BitacoraGembaWalks.Find(id);
            if (BitacoraGembaWalk == null)
            {
                return NotFound();
            }

            db.BitacoraGembaWalks.Remove(BitacoraGembaWalk);
            db.SaveChanges();

            return Ok(BitacoraGembaWalk);
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
            return db.BitacoraGembaWalks.Count(e => e.Id == id) > 0;
        }
    }
}