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
using PMMX.Modelo.RespuestaGenerica;

namespace Sitio.Areas.Apis.Controllers.Operarios
{
    public class MarcasController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        public IQueryable<Marca> GetMarcas()
        {

            return db.Marcas;
        }

        [ResponseType(typeof(RespuestaServicio<Marca>))]
        public IHttpActionResult GetMarca(string Code_FA)
        {
            MarcaServicio servicio = new MarcaServicio(db);
            var respuesta = servicio.GetMarca(Code_FA);
            return Ok(respuesta);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarca(string code_FA, Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (code_FA != marca.Code_FA)
            {
                return BadRequest();
            }

            db.Entry(marca).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaExists(code_FA))
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

            return CreatedAtRoute("DefaultApi", new { id = marca.Code_FA }, marca);
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

        private bool MarcaExists(string code_FA)
        {
            return db.Marcas.Count(e => e.Code_FA == code_FA) > 0;
        }
    }
}