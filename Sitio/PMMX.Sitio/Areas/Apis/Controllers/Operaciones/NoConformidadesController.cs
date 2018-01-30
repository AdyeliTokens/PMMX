using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Vistas;
using System.Collections;
using System.Collections.Generic;
using System;
using PMMX.Modelo.Entidades.Maquinaria;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class NoConformidadesController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/VQI
        public IQueryable<NoConformidad> GetNoConformidades()
        {
            return db.NoConformidades;
        }

        // GET: api/VQI/5
        [ResponseType(typeof(NoConformidad))]
        public IHttpActionResult GetNoConformidad(int id)
        {
            NoConformidad noConformidad = db.NoConformidades.Find(id);
            if (noConformidad == null)
            {
                return NotFound();
            }

            return Ok(noConformidad);
        }
        
        // GET: api/VQI/5
        [ResponseType(typeof(ModuloSeccionView))]
        public IHttpActionResult GetNoConformidadesPorFechaAndWorkCenter(DateTime fecha, int IdWorkCenter)
        {
            DateTime hoy = fecha.Date;
            DateTime mañana = hoy.AddDays(1);
            var secciones = db.ModuloSeccion.Select(x => new ModuloSeccionView
            {
                Id = x.Id,
                Nombre = x.Nombre,
                NoConformidades = x.NoConformidades.Where(n => (n.IdWorkCenter == IdWorkCenter) && (n.Fecha >= hoy && n.Fecha <= mañana)).Select(n => new NoConformidadView
                {
                    Id = n.Id,
                    Calificacion_VQI = n.Calificacion_VQI,
                    Fecha = n.Fecha
                }).ToList()
            }
            ).ToList();


            //var noConformidades = db.NoConformidades.Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= hoy && x.Fecha <= mañana) ).ToList();
            return Ok(secciones);
        }

        // PUT: api/VQI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNoConformidad(int id, NoConformidad noConformidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noConformidad.Id)
            {
                return BadRequest();
            }

            db.Entry(noConformidad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoConformidadExists(id))
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

        // POST: api/VQI
        [ResponseType(typeof(NoConformidad))]
        public IHttpActionResult PostNoConformidad(NoConformidad noConformidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NoConformidades.Add(noConformidad);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = noConformidad.Id }, noConformidad);
        }

        // DELETE: api/VQI/5
        [ResponseType(typeof(NoConformidad))]
        public IHttpActionResult DeleteNoConformidad(int id)
        {
            NoConformidad noConformidad = db.NoConformidades.Find(id);
            if (noConformidad == null)
            {
                return NotFound();
            }

            db.NoConformidades.Remove(noConformidad);
            db.SaveChanges();

            return Ok(noConformidad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoConformidadExists(int id)
        {
            return db.NoConformidades.Count(e => e.Id == id) > 0;
        }
    }
}
