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
    public class SubAreaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/SubArea
        public IQueryable<SubArea> GetSubArea()
        {
            return db.SubArea;
        }

        // GET: api/SubArea/5
        [ResponseType(typeof(SubArea))]
        public IHttpActionResult GetSubArea(int id)
        {
            SubArea subArea = db.SubArea.Find(id);
            if (subArea == null)
            {
                return NotFound();
            }

            return Ok(subArea);
        }

        // PUT: api/SubArea/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubArea(int id, SubArea subArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subArea.Id)
            {
                return BadRequest();
            }

            db.Entry(subArea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubAreaExists(id))
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

        // POST: api/SubArea
        [ResponseType(typeof(SubArea))]
        public IHttpActionResult PostSubArea(SubArea subArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubArea.Add(subArea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subArea.Id }, subArea);
        }

        // DELETE: api/SubArea/5
        [ResponseType(typeof(SubArea))]
        public IHttpActionResult DeleteSubArea(int id)
        {
            SubArea subArea = db.SubArea.Find(id);
            if (subArea == null)
            {
                return NotFound();
            }

            db.SubArea.Remove(subArea);
            db.SaveChanges();

            return Ok(subArea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubAreaExists(int id)
        {
            return db.SubArea.Count(e => e.Id == id) > 0;
        }
    }
}