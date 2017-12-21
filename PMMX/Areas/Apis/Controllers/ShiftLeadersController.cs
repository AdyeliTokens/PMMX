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
using PMMX.Operaciones.Servicios;

namespace Sitio.Areas.Apis.Controllers
{
    public class ShiftLeadersController : ApiController
    {
        private PMMXContext db = new PMMXContext();


        public IQueryable<ShiftLeaders> GetShiftLeaders()
        {
            return db.ShiftLeaders;
        }


        [ResponseType(typeof(List<WorkCenterView>))]
        public IHttpActionResult GetShiftLeaders(int idShiftLeaders)
        {
            WorkCenterServicio servicio = new WorkCenterServicio(db);
            var respuesta = servicio.GetWorkCentersByShiftLeaders(idShiftLeaders);
            return Ok(respuesta.Respuesta);
        }


        [ResponseType(typeof(void))]
        public IHttpActionResult PutShiftLeaders(int id, ShiftLeaders shiftLeaders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shiftLeaders.Id)
            {
                return BadRequest();
            }

            db.Entry(shiftLeaders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftLeadersExists(id))
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


        [ResponseType(typeof(ShiftLeaders))]
        public IHttpActionResult PostShiftLeaders(ShiftLeaders shiftLeaders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShiftLeaders.Add(shiftLeaders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shiftLeaders.Id }, shiftLeaders);
        }


        [ResponseType(typeof(ShiftLeaders))]
        public IHttpActionResult DeleteShiftLeaders(int id)
        {
            ShiftLeaders shiftLeaders = db.ShiftLeaders.Find(id);
            if (shiftLeaders == null)
            {
                return NotFound();
            }

            db.ShiftLeaders.Remove(shiftLeaders);
            db.SaveChanges();

            return Ok(shiftLeaders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShiftLeadersExists(int id)
        {
            return db.ShiftLeaders.Count(e => e.Id == id) > 0;
        }
    }
}