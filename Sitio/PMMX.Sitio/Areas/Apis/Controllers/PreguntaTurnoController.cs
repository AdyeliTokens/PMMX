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
using PMMX.Modelo.Vistas;

namespace Sitio.Areas
{
    public class PreguntaTurnoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Pregunta
        public IQueryable<PreguntaTurno> GetPreguntaTurno()
        {
            return db.PreguntaTurno;
        }

        // GET: api/Pregunta/5
        [ResponseType(typeof(PreguntaTurno))]
        public IHttpActionResult GetPreguntaTurno(int id)
        {
            PreguntaTurno pregunta = db.PreguntaTurno.Find(id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return Ok(pregunta);
        }

        // PUT: api/Pregunta/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPreguntaTurno(int id, PreguntaTurno PreguntaTurno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != PreguntaTurno.Id)
            {
                return BadRequest();
            }

            db.Entry(PreguntaTurno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntaTurnoExists(id))
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

        // PUT: api/Paro/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPreguntaTurnoByRespuesta(int id, Respuesta respuesta)
        {
            PreguntaTurno PreguntaTurno = db.PreguntaTurno.Find(id);
            if (PreguntaTurno == null)
            {
                return NotFound();
            }
            
            if (PreguntaTurno.Respuestas == null)
            {
               PreguntaTurno.Respuestas = new List<Respuesta>() { respuesta };
            }
            else
            {
                PreguntaTurno.Respuestas.Add(respuesta);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(PreguntaTurno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntaTurnoExists(id))
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


        // POST: api/Pregunta
        [ResponseType(typeof(PreguntaTurno))]
        public IHttpActionResult PostPreguntaTurno(PreguntaTurno PreguntaTurno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PreguntaTurno.Add(PreguntaTurno);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = PreguntaTurno.Id }, PreguntaTurno);
        }

        // DELETE: api/Pregunta/5
        [ResponseType(typeof(PreguntaTurno))]
        public IHttpActionResult DeletePreguntaTurno(int id)
        {
            PreguntaTurno Pregunta = db.PreguntaTurno.Find(id);
            if (Pregunta == null)
            {
                return NotFound();
            }

            db.PreguntaTurno.Remove(Pregunta);
            db.SaveChanges();

            return Ok(Pregunta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreguntaTurnoExists(int id)
        {
            return db.PreguntaTurno.Count(e => e.Id == id) > 0;
        }
    }
}