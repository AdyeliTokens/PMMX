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

namespace Sitio.Areas.Apis.Controllers
{
    public class WorkFlowsController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/WorkFlows
        public IQueryable<WorkFlow> GetWorkFlows()
        {
            return db.WorkFlows;
        }

        // GET: api/WorkFlows/5
        [ResponseType(typeof(WorkFlow))]
        public IHttpActionResult GetWorkFlow(int id)
        {
            WorkFlow workFlow = db.WorkFlows.Find(id);
            if (workFlow == null)
            {
                return NotFound();
            }

            return Ok(workFlow);
        }

        // PUT: api/WorkFlows/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkFlow(int id, WorkFlow workFlow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workFlow.Id)
            {
                return BadRequest();
            }

            db.Entry(workFlow).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkFlowExists(id))
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

        // POST: api/WorkFlows
        [ResponseType(typeof(WorkFlow))]
        public IHttpActionResult PostWorkFlow(WorkFlow workFlow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkFlows.Add(workFlow);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workFlow.Id }, workFlow);
        }

        // DELETE: api/WorkFlows/5
        [ResponseType(typeof(WorkFlow))]
        public IHttpActionResult DeleteWorkFlow(int id)
        {
            WorkFlow workFlow = db.WorkFlows.Find(id);
            if (workFlow == null)
            {
                return NotFound();
            }

            db.WorkFlows.Remove(workFlow);
            db.SaveChanges();

            return Ok(workFlow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkFlowExists(int id)
        {
            return db.WorkFlows.Count(e => e.Id == id) > 0;
        }
    }
}