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
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class ComentariosController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Comentarios
        public IQueryable<Comentario> GetComentarios()
        {
            return db.Comentarios;
        }

        // GET: api/Comentarios/5
        [ResponseType(typeof(Comentario))]
        public IHttpActionResult GetComentario(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }



        [ResponseType(typeof(IList<ComentarioView>))]
        public IHttpActionResult GetComentarioByDefect(int idDefecto)
        {
            IList<ComentarioView> comentarios = db.Comentarios
                            .Where(x => x.IdDefecto == idDefecto)
                            .Select(x => new ComentarioView
                            {
                                Id = x.Id,
                                IdComentador = x.IdComentador,
                                IdDefecto = x.IdDefecto,
                                Opinion = x.Opinion,
                                Fecha = x.Fecha,
                                Comentador = new PersonaView
                                {
                                    Id = x.Comentador.Id,
                                    Nombre = x.Comentador.Nombre,
                                    Apellido1 = x.Comentador.Apellido1,
                                    Apellido2 = x.Comentador.Apellido2
                                }
                            }).ToList();
            
            return Ok(comentarios);
        }
        


        [ResponseType(typeof(void))]
        public IHttpActionResult PutComentario(int id, Comentario comentario)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comentario.Id)
            {
                return BadRequest();
            }

            db.Entry(comentario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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
        
        [ResponseType(typeof(IList<ComentarioView>))]
        public IHttpActionResult PostComentario(Comentario comentario)
        {
            comentario.Fecha = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comentarios.Add(comentario);
            db.SaveChanges();

            IList<ComentarioView> comentarios = db.Comentarios
                            .Where(x => x.IdDefecto == comentario.IdDefecto)
                            .Select(x => new ComentarioView
                            {
                                Id = x.Id,
                                IdComentador = x.IdComentador,
                                IdDefecto = x.IdDefecto,
                                Opinion = x.Opinion,
                                Fecha = x.Fecha,
                                Comentador = new PersonaView
                                {
                                    Id = x.Comentador.Id,
                                    Nombre = x.Comentador.Nombre,
                                    Apellido1 = x.Comentador.Apellido1,
                                    Apellido2 = x.Comentador.Apellido2
                                }
                            }).ToList();

            return Ok(comentarios);
        }

        // DELETE: api/Comentarios/5
        [ResponseType(typeof(Comentario))]
        public IHttpActionResult DeleteComentario(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return NotFound();
            }

            db.Comentarios.Remove(comentario);
            db.SaveChanges();

            return Ok(comentario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComentarioExists(int id)
        {
            return db.Comentarios.Count(e => e.Id == id) > 0;
        }
    }
}