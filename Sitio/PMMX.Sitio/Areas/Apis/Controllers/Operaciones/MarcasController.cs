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
using PMMX.Operaciones.Servicios;

namespace Sitio.Areas.Apis.Controllers.Operarios
{
    public class MarcasController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        public IQueryable<Marca> GetMarcas()
        {

            return db.Marcas;
        }

        [ResponseType(typeof(Marca))]
        public IHttpActionResult GetMarca(int id)
        {
            PesadorServicio servicio = new PesadorServicio();
            var workcenters = servicio.GetWorkCentersByPesador(id);
            return Ok(workcenters.Respuesta);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutPesador(int id, Marca marcas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marcas.Id)
            {
                return BadRequest();
            }

            db.Entry(marcas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaExists(id))
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

        [ResponseType(typeof(Marca))]
        public IHttpActionResult PostMarca(Marca marca )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Marcas.Add(marca);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = marca.Id }, marca);
        }

        [ResponseType(typeof(Marca))]
        public IHttpActionResult DeleteMarca(int id)
        {
            Marca marca = db.Marcas.Find(id);
            if (marca == null)
            {
                return NotFound();
            }

            db.Marcas.Remove(marca);
            db.SaveChanges();

            return Ok(marca);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarcaExists(int id)
        {
            return db.Marcas.Count(e => e.Id == id) > 0;
        }
    }
}