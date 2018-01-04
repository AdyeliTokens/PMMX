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

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class ModuloSeccionesController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/ModuloSecciones
        public IQueryable<ModuloSeccion> GetModuloSeccion()
        {
            return db.ModuloSeccion;
        }

        // GET: api/ModuloSecciones/5
        [ResponseType(typeof(ModuloSeccion))]
        public IHttpActionResult GetModuloSeccion(int id)
        {
            ModuloSeccion moduloSeccion = db.ModuloSeccion.Find(id);
            if (moduloSeccion == null)
            {
                return NotFound();
            }

            return Ok(moduloSeccion);
        }

        // PUT: api/ModuloSecciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModuloSeccion(int id, ModuloSeccion moduloSeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moduloSeccion.Id)
            {
                return BadRequest();
            }

            db.Entry(moduloSeccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuloSeccionExists(id))
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

        // POST: api/ModuloSecciones
        [ResponseType(typeof(ModuloSeccion))]
        public IHttpActionResult PostModuloSeccion(ModuloSeccion moduloSeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ModuloSeccion.Add(moduloSeccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = moduloSeccion.Id }, moduloSeccion);
        }

        // DELETE: api/ModuloSecciones/5
        [ResponseType(typeof(ModuloSeccion))]
        public IHttpActionResult DeleteModuloSeccion(int id)
        {
            ModuloSeccion moduloSeccion = db.ModuloSeccion.Find(id);
            if (moduloSeccion == null)
            {
                return NotFound();
            }

            db.ModuloSeccion.Remove(moduloSeccion);
            db.SaveChanges();

            return Ok(moduloSeccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModuloSeccionExists(int id)
        {
            return db.ModuloSeccion.Count(e => e.Id == id) > 0;
        }
    }
}