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
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    public class SubCategoriaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/SubCategoria
        public IQueryable<SubCategoria> GetSubCategoria()
        {
            return db.SubCategoria;
        }

        // GET: api/SubCategoria/5
        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult GetSubCategoria(int id)
        {
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            return Ok(subCategoria);
        }

        [ResponseType(typeof(SubCategoriaView))]
        public IHttpActionResult GetSubCategoriaByCategoria(int idCategoria)
        {
            var subCategoria = db.SubCategoria.Where(s => (s.IdCategoria == idCategoria))
            .Select(s => new SubCategoriaView
            {
                Id = s.Id,
                Nombre = s.Nombre,
                NombreCorto = s.NombreCorto,
                IdCategoria = s.IdCategoria,
                IdResponsable = s.IdResponsable,
                IdGrupo = s.Categoria.GrupoPreguntas.Where(g => (g.IdCategoria == idCategoria) && (s.NombreCorto.Equals(g.Nombre))).Select(g => g.Id).FirstOrDefault(),
                Activo = s.Activo                
            }).ToList();

            if (subCategoria == null)
            {
                return NotFound();
            }

            return Ok(subCategoria);
        }

        // PUT: api/SubCategoria/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubCategoria(int id, SubCategoria subCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subCategoria.Id)
            {
                return BadRequest();
            }

            db.Entry(subCategoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoriaExists(id))
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

        // POST: api/SubCategoria
        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult PostSubCategoria(SubCategoria subCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubCategoria.Add(subCategoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subCategoria.Id }, subCategoria);
        }

        // DELETE: api/SubCategoria/5
        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult DeleteSubCategoria(int id)
        {
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            db.SubCategoria.Remove(subCategoria);
            db.SaveChanges();

            return Ok(subCategoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubCategoriaExists(int id)
        {
            return db.SubCategoria.Count(e => e.Id == id) > 0;
        }
    }
}