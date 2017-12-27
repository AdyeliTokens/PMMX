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
using PMMX.Modelo.Entidades;
using PMMX.Modelo;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas
{
    public class GrupoPreguntasController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/GrupoPreguntas
        public IQueryable<GrupoPreguntas> GetGrupoPreguntas()
        {
            return db.GrupoPreguntas;
        }
        
        [ResponseType(typeof(List<GrupoPreguntasView>))]
        public IHttpActionResult getGrupoPreguntasbyCategoria(int idCategoria)
        {
           var GrupoPreguntas = db.GrupoPreguntas.Where(x => x.IdCategoria == idCategoria)
                    .Select(x => new GrupoPreguntasView
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        DDS = x.DDS,
                        IdCategoria = x.IdCategoria,
                        Activo = x.Activo
                    }).ToList();
            
            if (GrupoPreguntas == null)
            {
                return NotFound();
            }

            return Ok(GrupoPreguntas);
        }
        
        // GET: api/GrupoPreguntas/5
        [ResponseType(typeof(GrupoPreguntas))]
        public IHttpActionResult GetGrupoPreguntas(int id)
        {
            GrupoPreguntas GrupoPreguntas = db.GrupoPreguntas.Find(id);
            if (GrupoPreguntas == null)
            {
                return NotFound();
            }

            return Ok(GrupoPreguntas);
        }

        // PUT: api/GrupoPreguntas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrupoPreguntas(int id, GrupoPreguntas GrupoPreguntas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != GrupoPreguntas.Id)
            {
                return BadRequest();
            }

            db.Entry(GrupoPreguntas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoPreguntasExists(id))
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

        // POST: api/GrupoPreguntas
        [ResponseType(typeof(GrupoPreguntas))]
        public IHttpActionResult PostGrupoPreguntas(GrupoPreguntas GrupoPreguntas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GrupoPreguntas.Add(GrupoPreguntas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = GrupoPreguntas.Id }, GrupoPreguntas);
        }

        // DELETE: api/GrupoPreguntas/5
        [ResponseType(typeof(GrupoPreguntas))]
        public IHttpActionResult DeleteGrupoPreguntas(int id)
        {
            GrupoPreguntas GrupoPreguntas = db.GrupoPreguntas.Find(id);
            if (GrupoPreguntas == null)
            {
                return NotFound();
            }

            db.GrupoPreguntas.Remove(GrupoPreguntas);
            db.SaveChanges();

            return Ok(GrupoPreguntas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GrupoPreguntasExists(int id)
        {
            return db.GrupoPreguntas.Count(e => e.Id == id) > 0;
        }
    }
}