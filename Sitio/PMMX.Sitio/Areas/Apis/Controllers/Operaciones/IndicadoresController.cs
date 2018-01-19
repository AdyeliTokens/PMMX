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

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class IndicadoresController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Indicadores
        public IQueryable<Indicador> GetIndicadores()
        {
            return db.Indicadores;
        }

        // GET: api/Indicadores/5
        [ResponseType(typeof(Indicador))]
        public IHttpActionResult GetIndicador(int id)
        {
            Indicador indicador = db.Indicadores.Find(id);
            if (indicador == null)
            {
                return NotFound();
            }

            return Ok(indicador);
        }

        // PUT: api/Indicadores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIndicador(int id, Indicador indicador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != indicador.Id)
            {
                return BadRequest();
            }

            db.Entry(indicador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicadorExists(id))
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

        // POST: api/Indicadores
        [ResponseType(typeof(Indicador))]
        public IHttpActionResult PostIndicador(Indicador indicador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Indicadores.Add(indicador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = indicador.Id }, indicador);
        }

        // DELETE: api/Indicadores/5
        [ResponseType(typeof(Indicador))]
        public IHttpActionResult DeleteIndicador(int id)
        {
            Indicador indicador = db.Indicadores.Find(id);
            if (indicador == null)
            {
                return NotFound();
            }

            db.Indicadores.Remove(indicador);
            db.SaveChanges();

            return Ok(indicador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IndicadorExists(int id)
        {
            return db.Indicadores.Count(e => e.Id == id) > 0;
        }
    }
}