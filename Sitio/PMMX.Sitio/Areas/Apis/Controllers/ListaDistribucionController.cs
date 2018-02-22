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
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades;

namespace Sitio.Areas.Apis.Controllers
{
    public class ListaDistribucionController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/ListaDistribucion
        public IQueryable<ListaDistribucion> GetListaDistribucion()
        {
            return db.ListaDistribucion;
        }

        // GET: api/ListaDistribucion/5
        [ResponseType(typeof(ListaDistribucion))]
        public IHttpActionResult GetListaDistribucion(int id)
        {
            ListaDistribucion listaDistribucion = db.ListaDistribucion.Find(id);
            if (listaDistribucion == null)
            {
                return NotFound();
            }

            return Ok(listaDistribucion);
        }

        // PUT: api/ListaDistribucion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutListaDistribucion(int id, ListaDistribucion listaDistribucion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listaDistribucion.Id)
            {
                return BadRequest();
            }

            db.Entry(listaDistribucion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaDistribucionExists(id))
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

        // POST: api/ListaDistribucion
        [ResponseType(typeof(ListaDistribucion))]
        public IHttpActionResult PostListaDistribucion(ListaDistribucion listaDistribucion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ListaDistribucion.Add(listaDistribucion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = listaDistribucion.Id }, listaDistribucion);
        }

        // DELETE: api/ListaDistribucion/5
        [ResponseType(typeof(ListaDistribucion))]
        public IHttpActionResult DeleteListaDistribucion(int id)
        {
            ListaDistribucion listaDistribucion = db.ListaDistribucion.Find(id);
            if (listaDistribucion == null)
            {
                return NotFound();
            }

            db.ListaDistribucion.Remove(listaDistribucion);
            db.SaveChanges();

            return Ok(listaDistribucion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ListaDistribucionExists(int id)
        {
            return db.ListaDistribucion.Count(e => e.Id == id) > 0;
        }
    }
}