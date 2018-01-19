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
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class ActividadEnParoController : ApiController
    {
        private PMMXContext db;
        private ActividadEnParoServicio _servicio;

        ActividadEnParoController() {
            db = new PMMXContext();
            _servicio = new ActividadEnParoServicio(db);
        }


        public RespuestaServicio<IQueryable<ActividadEnParo>> GetActividadEnParo()
        {
            return _servicio.GetActividadesEnParo();
        }

        [ResponseType(typeof(RespuestaServicio<ActividadEnParo>))]
        public IHttpActionResult GetActividadEnParo(int id)
        {
            var respuesta = _servicio.GetActividadEnParo(id);
            
            return Ok(respuesta);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutActividadEnParo(int id, ActividadEnParo actividadEnParo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actividadEnParo.Id)
            {
                return BadRequest();
            }

            db.Entry(actividadEnParo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadEnParoExists(id))
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

        [ResponseType(typeof(ActividadEnParo))]
        public IHttpActionResult PostActividadEnParo(ActividadEnParo actividadEnParo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActividadEnParos.Add(actividadEnParo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actividadEnParo.Id }, actividadEnParo);
        }

        [ResponseType(typeof(ActividadEnParo))]
        public IHttpActionResult DeleteActividadEnParo(int id)
        {
            ActividadEnParo actividadEnParo = db.ActividadEnParos.Find(id);
            if (actividadEnParo == null)
            {
                return NotFound();
            }

            db.ActividadEnParos.Remove(actividadEnParo);
            db.SaveChanges();

            return Ok(actividadEnParo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActividadEnParoExists(int id)
        {
            return db.ActividadEnParos.Count(e => e.Id == id) > 0;
        }
    }
}