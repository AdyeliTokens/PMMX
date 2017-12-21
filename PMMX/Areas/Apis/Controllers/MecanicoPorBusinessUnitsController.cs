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
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    public class MecanicoPorBusinessUnitsController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/MecanicoPorBusinessUnits
        public IQueryable<Mecanicos> GetMecanicos()
        {
            return db.Mecanicos;
        }

        // GET: api/MecanicoPorBusinessUnits/5
        [ResponseType(typeof(Mecanicos))]
        public IHttpActionResult GetMecanicoPorBusinessUnit(int id)
        {
            Mecanicos mecanicoPorBusinessUnit = db.Mecanicos.Find(id);
            if (mecanicoPorBusinessUnit == null)
            {
                return NotFound();
            }

            return Ok(mecanicoPorBusinessUnit);
        }

        // GET: api/MecanicoPorBusinessUnits/5
        [ResponseType(typeof(Mecanicos))]
        public IHttpActionResult GetBusinessUnitByMecanico(int idMecanico)
        {
            var mecanicoPorBusinessUnit = db.Mecanicos.Where(x => x.IdMecanico == idMecanico).Select(x => new BussinesUnitView
            {
                Id = x.BusinessUnit.Id,
                Nombre = x.BusinessUnit.Nombre,
                NombreCorto = x.BusinessUnit.NombreCorto,
                Activo = x.BusinessUnit.Activo,
                WorkCenters = x.BusinessUnit.WorkCenters.Select(w => new WorkCenterView
                {
                    Id = w.Id,
                    Activo = w.Activo,
                    IdBussinesUnit = w.IdBussinesUnit,
                    Nombre = w.Nombre,
                    NombreCorto = w.NombreCorto,
                    DefectosActivos = db.Defectos.Where(p => (p.Origen.IdWorkCenter == w.Id && p.Activo == true)).Count(),
                    ParosActivos = db.Paros.Where(p => (p.Origen.IdWorkCenter == w.Id && p.Activo == true)).Count()
                }).ToList()
            }).FirstOrDefault();


            return Ok(mecanicoPorBusinessUnit);
        }

        // PUT: api/MecanicoPorBusinessUnits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMecanicoPorBusinessUnit(int id, Mecanicos mecanicoPorBusinessUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mecanicoPorBusinessUnit.Id)
            {
                return BadRequest();
            }

            db.Entry(mecanicoPorBusinessUnit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MecanicoPorBusinessUnitExists(id))
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

        // POST: api/MecanicoPorBusinessUnits
        [ResponseType(typeof(Mecanicos))]
        public IHttpActionResult PostMecanicoPorBusinessUnit(Mecanicos mecanicoPorBusinessUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mecanicos.Add(mecanicoPorBusinessUnit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mecanicoPorBusinessUnit.Id }, mecanicoPorBusinessUnit);
        }

        // DELETE: api/MecanicoPorBusinessUnits/5
        [ResponseType(typeof(Mecanicos))]
        public IHttpActionResult DeleteMecanicoPorBusinessUnit(int id)
        {
            Mecanicos mecanicoPorBusinessUnit = db.Mecanicos.Find(id);
            if (mecanicoPorBusinessUnit == null)
            {
                return NotFound();
            }

            db.Mecanicos.Remove(mecanicoPorBusinessUnit);
            db.SaveChanges();

            return Ok(mecanicoPorBusinessUnit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MecanicoPorBusinessUnitExists(int id)
        {
            return db.Mecanicos.Count(e => e.Id == id) > 0;
        }
    }
}