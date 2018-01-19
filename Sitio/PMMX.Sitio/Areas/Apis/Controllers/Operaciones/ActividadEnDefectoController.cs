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
using PMMX.Modelo.Entidades.Defectos;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class ActividadEnDefectoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/ActividadEnDefectos
        public IQueryable<ActividadEnDefecto> GetActividadEnDefecto()
        {
            return db.ActividadEnDefectos;
        }

        // GET: api/ActividadEnDefectos/5
        [ResponseType(typeof(ActividadEnDefecto))]
        public IHttpActionResult GetActividadEnDefecto(int id)
        {
            ActividadEnDefecto actividadEnDefecto = db.ActividadEnDefectos.Find(id);
            if (actividadEnDefecto == null)
            {
                return NotFound();
            }

            return Ok(actividadEnDefecto);
        }

        // PUT: api/ActividadEnDefectos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActividadEnDefecto(int id, ActividadEnDefecto actividadEnDefecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actividadEnDefecto.Id)
            {
                return BadRequest();
            }

            db.Entry(actividadEnDefecto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadEnDefectoExists(id))
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

        // POST: api/ActividadEnDefectos
        [ResponseType(typeof(ActividadEnDefecto))]
        public IHttpActionResult PostActividadEnDefecto(ActividadEnDefecto actividadEnDefecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActividadEnDefectos.Add(actividadEnDefecto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actividadEnDefecto.Id }, actividadEnDefecto);
        }

        // DELETE: api/ActividadEnDefectos/5
        [ResponseType(typeof(ActividadEnDefecto))]
        public IHttpActionResult DeleteActividadEnDefecto(int id)
        {
            ActividadEnDefecto actividadEnDefecto = db.ActividadEnDefectos.Find(id);
            if (actividadEnDefecto == null)
            {
                return NotFound();
            }

            db.ActividadEnDefectos.Remove(actividadEnDefecto);
            db.SaveChanges();

            return Ok(actividadEnDefecto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActividadEnDefectoExists(int id)
        {
            return db.ActividadEnDefectos.Count(e => e.Id == id) > 0;
        }
    }
}