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
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class BussinesUnitController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/BussinesUnit/5
        [ResponseType(typeof(IList<BussinesUnitView>))]
        public IHttpActionResult GetParosyDefectosforBussinessUnits()
        {

            var bussinesUnit = db.BussinesUnits
                .Select(b => new BussinesUnitView
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    NombreCorto = b.NombreCorto,
                    IdArea = b.IdArea,
                    IdResponsable = b.IdResponsable,
                    Activo = b.Activo,
                    DefectosActivos = db.Defectos.Where(d => (d.Origen.WorkCenter.BussinesUnit.Id == b.Id) && (d.Activo == true)).Count(),
                    ParosActivos = db.Paros.Where(p => (p.Origen.WorkCenter.BussinesUnit.Id == b.Id) && (p.Activo == true)).Count()
                }).ToList();

            if (bussinesUnit == null)
            {
                return NotFound();
            }

            return Ok(bussinesUnit);
        }

        // GET: api/BussinesUnit/5
        [ResponseType(typeof(BussinesUnit))]
        public IHttpActionResult GetBussinesUnit(int id)
        {
            BussinesUnit bussinesUnit = db.BussinesUnits.Find(id);
            if (bussinesUnit == null)
            {
                return NotFound();
            }

            return Ok(bussinesUnit);
        }
        
        // PUT: api/BussinesUnit/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBussinesUnit(int id, BussinesUnit bussinesUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bussinesUnit.Id)
            {
                return BadRequest();
            }

            db.Entry(bussinesUnit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BussinesUnitExists(id))
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

        // POST: api/BussinesUnit
        [ResponseType(typeof(BussinesUnit))]
        public IHttpActionResult PostBussinesUnit(BussinesUnit bussinesUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BussinesUnits.Add(bussinesUnit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bussinesUnit.Id }, bussinesUnit);
        }

        // DELETE: api/BussinesUnit/5
        [ResponseType(typeof(BussinesUnit))]
        public IHttpActionResult DeleteBussinesUnit(int id)
        {
            BussinesUnit bussinesUnit = db.BussinesUnits.Find(id);
            if (bussinesUnit == null)
            {
                return NotFound();
            }

            db.BussinesUnits.Remove(bussinesUnit);
            db.SaveChanges();

            return Ok(bussinesUnit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BussinesUnitExists(int id)
        {
            return db.BussinesUnits.Count(e => e.Id == id) > 0;
        }
    }
}