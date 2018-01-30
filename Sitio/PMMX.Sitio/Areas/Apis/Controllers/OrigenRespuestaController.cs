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
using PMMX.Modelo.Entidades.Paros;
using PMMX.Modelo.Vistas;
using PMMX.Modelo;
using PMMX.Modelo.Entidades;

namespace Sitio.Areas
{
    public class OrigenRespuestaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Pregunta
        public IQueryable<OrigenRespuesta> GetOrigenRespuesta()
        {
            return db.OrigenRespuesta;
        }

        // GET: api/Pregunta/5
        [ResponseType(typeof(OrigenRespuesta))]
        public IHttpActionResult GetOrigenRespuesta(int id)
        {
            OrigenRespuesta MWCR = db.OrigenRespuesta.Find(id);
            if (MWCR == null)
            {
                return NotFound();
            }

            return Ok(MWCR);
        }
        
        // PUT: api/Pregunta/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrigenRespuesta(int id, OrigenRespuesta MWCR)
        {
            MWCR.Fecha = DateTime.Now;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != MWCR.Id)
            {
                return BadRequest();
            }

            db.Entry(MWCR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrigenRespuestaExists(id))
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

        // POST: api/Pregunta
        [ResponseType(typeof(OrigenRespuesta))]
        public IHttpActionResult PostOrigenRespuesta(OrigenRespuesta MWCR)
        {
            MWCR.Fecha = DateTime.Now;
            MWCR.Activo = true;
            MWCR.IdSupervisor = db.Origens.Where(x => (x.Id == MWCR.IdOrigen)).Select(y => y.WorkCenter.BussinesUnit.IdResponsable).FirstOrDefault();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrigenRespuesta.Add(MWCR);
            db.SaveChanges();
            var MWCRAdded = db.OrigenRespuesta.Where(x => x.Id == MWCR.Id).Select(x => new OrigenRespuestaView
            {
                Id = x.Id,
                IdOrigen = x.IdOrigen,
                IdEntrevistado = x.IdEntrevistado,
                IdSupervisor  = x.IdSupervisor,
                Fecha = x.Fecha,
                Activo = x.Activo}).FirstOrDefault();

            return Ok(MWCRAdded);
            //return CreatedAtRoute("DefaultApi", new { id = MWCR.Id }, MWCR);
        }

        // DELETE: api/Pregunta/5
        [ResponseType(typeof(OrigenRespuesta))]
        public IHttpActionResult DeleteOrigenRespuesta(int id)
        {
            OrigenRespuesta MWCR = db.OrigenRespuesta.Find(id);
            if (MWCR == null)
            {
                return NotFound();
            }

            db.OrigenRespuesta.Remove(MWCR);
            db.SaveChanges();

            return Ok(MWCR);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrigenRespuestaExists(int id)
        {
            return db.OrigenRespuesta.Count(e => e.Id == id) > 0;
        }
    }
}