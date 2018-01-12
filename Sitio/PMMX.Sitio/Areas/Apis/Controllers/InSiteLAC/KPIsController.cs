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
using PMMX.Modelo.Entidades.InsiteLAC;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers.InSiteLAC
{
    public class KPIsController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/KPIs
        public IQueryable<KPIView> GetKPIs()
        {
            var list = db.KPIs.Select(x => new KPIView { Description=  x.Description });
            return list.Distinct();
        }

        // GET: api/KPIs/5
        [ResponseType(typeof(KPI))]
        public IHttpActionResult GetKPI(int id)
        {
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return NotFound();
            }

            return Ok(kPI);
        }

        // PUT: api/KPIs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKPI(int id, KPI kPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kPI.Id)
            {
                return BadRequest();
            }

            db.Entry(kPI).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KPIExists(id))
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

        // POST: api/KPIs
        [ResponseType(typeof(KPI))]
        public IHttpActionResult PostKPI(KPI kPI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KPIs.Add(kPI);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kPI.Id }, kPI);
        }

        // DELETE: api/KPIs/5
        [ResponseType(typeof(KPI))]
        public IHttpActionResult DeleteKPI(int id)
        {
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return NotFound();
            }

            db.KPIs.Remove(kPI);
            db.SaveChanges();

            return Ok(kPI);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KPIExists(int id)
        {
            return db.KPIs.Count(e => e.Id == id) > 0;
        }
    }
}