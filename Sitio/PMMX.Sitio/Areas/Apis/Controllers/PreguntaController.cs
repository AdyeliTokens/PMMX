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
    public class PreguntaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Pregunta
        public IQueryable<Pregunta> GetPreguntas()
        {
            return db.Preguntas;
        }

        // GET: api/Pregunta/5
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult GetPregunta(int id)
        {
            Pregunta pregunta = db.Preguntas.Find(id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return Ok(pregunta);
        }

        // GET: api/Pregunta/5
        [ResponseType(typeof(IList<PreguntaTurnoView>))]
        public IList<PreguntaTurnoView> GetPreguntasByWC(int idWC)
        {
            int diaSemana = (int)DateTime.Now.DayOfWeek;
            int hora = (int)DateTime.Now.TimeOfDay.Hours;

            var preguntas = db.PreguntaTurno.Where(x => (x.Origen.IdWorkCenter == idWC) && (x.IdDia.Equals(diaSemana)))
                .Select(y => new PreguntaTurnoView()
                    { Id = y.Id,
                      IdOrigen = y.IdOrigen,
                      IdDia = y.IdDia,
                      IdTurno = y.IdTurno,
                      Pregunta = new PreguntaView
                      {
                          Id = y.Pregunta.Id,
                          IdGrupo = y.Pregunta.IdGrupo,
                          Interrogante = y.Pregunta.Interrogante,
                          EnParo = y.Pregunta.EnParo,
                          EPP = y.Pregunta.EPP,
                          Herramientas = y.Pregunta.Herramientas,
                          GrupoPreguntas = new GrupoPreguntasView
                          {
                              Id = y.Pregunta.GrupoPreguntas.Id,
                              Nombre = y.Pregunta.GrupoPreguntas.Nombre,
                              DDS =  y.Pregunta.GrupoPreguntas.DDS,
                              Activo = y.Pregunta.GrupoPreguntas.Activo
                          },
                          Activo = y.Pregunta.Activo,
                          Dias = y.Pregunta.Dias,
                          Turnos = y.Pregunta.Turnos,
                          TiempoEstimado = y.Pregunta.TiempoEstimado
                      },
                      Origen = new OrigenView
                      {
                          Id =  y.Origen.Id,
                          IdModulo = y.Origen.IdModulo,
                          IdWorkCenter = y.Origen.IdWorkCenter,
                          Modulo = new ModuloView
                          {
                              Id = y.Origen.Modulo.Id,
                              Nombre = y.Origen.Modulo.Nombre,
                              NombreCorto = y.Origen.Modulo.NombreCorto,
                              Activo = y.Origen.Modulo.Activo
                          }
                      },
                      IdPregunta =y.IdPregunta
                    }
                ).ToList();

            return preguntas;
        }
       
        // GET: api/Pregunta/5
        [ResponseType(typeof(IList<PreguntaTurnoView>))]
        public IList<PreguntaTurnoView> GetPreguntasByWCDDS(int idWC, int idDDS)
        {
            int diaSemana = (int)DateTime.Now.DayOfWeek;
            int hora = (int)DateTime.Now.TimeOfDay.Hours;
            bool dds = (idDDS == 1) ? true : false;

            var preguntas = db.PreguntaTurno.Where(x => (x.Origen.IdWorkCenter == idWC) && (x.IdDia.Equals(diaSemana))  && (x.Pregunta.GrupoPreguntas.DDS.Equals(dds) ))
                .Select(y => new PreguntaTurnoView()
                {
                    Id = y.Id,
                    IdOrigen = y.IdOrigen,
                    IdDia = y.IdDia,
                    IdTurno = y.IdTurno,
                    Pregunta = new PreguntaView
                    {
                        Id = y.Pregunta.Id,
                        IdGrupo = y.Pregunta.IdGrupo,
                        Interrogante = y.Pregunta.Interrogante,
                        EnParo = y.Pregunta.EnParo,
                        EPP = y.Pregunta.EPP,
                        Herramientas = y.Pregunta.Herramientas,
                        GrupoPreguntas = new GrupoPreguntasView
                        {
                            Id = y.Pregunta.GrupoPreguntas.Id,
                            Nombre = y.Pregunta.GrupoPreguntas.Nombre,
                            DDS = y.Pregunta.GrupoPreguntas.DDS
                        },
                        //Dias = y.Pregunta.Dias,
                        //Turnos = y.Pregunta.Turnos,
                        TiempoEstimado = y.Pregunta.TiempoEstimado
                    },
                    Origen = new OrigenView
                    {
                        Id = y.Origen.Id,
                        IdModulo = y.Origen.IdModulo,
                        IdWorkCenter = y.Origen.IdWorkCenter,
                        Modulo = new ModuloView
                        {
                            Id = y.Origen.Modulo.Id,
                            Nombre = y.Origen.Modulo.Nombre,
                            NombreCorto = y.Origen.Modulo.NombreCorto
                        }
                    },
                    IdPregunta = y.IdPregunta
                }
                ).ToList();

            return preguntas;
        }

        [ResponseType(typeof(IList<PreguntaView>))]
        public IList<PreguntaView> GetPreguntasByGrupo(int IdGrupo)
        {
            int diaSemana = (int)DateTime.Now.DayOfWeek;
            int hora = (int)DateTime.Now.TimeOfDay.Hours;

            // var preguntas = db.PreguntaTurno.Where(x => (x.Origen.IdWorkCenter == idWC) && (x.IdDia.Equals(diaSemana)) && (x.Pregunta.GrupoPreguntas.Id.Equals(idHC)))  
            var preguntas = db.Preguntas.Where(x => (x.IdGrupo.Equals(IdGrupo)))
                 .Select(y => new PreguntaView()
                {
                   Id = y.Id,
                   IdGrupo = y.IdGrupo,
                   Interrogante = y.Interrogante,
                   EnParo = y.EnParo,
                   EPP = y.EPP,
                   Herramientas = y.Herramientas,
                   TiempoEstimado = y.TiempoEstimado
                }
                ).ToList();

            return preguntas;
        }


        // PUT: api/Pregunta/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPregunta(int id, Pregunta Pregunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Pregunta.Id)
            {
                return BadRequest();
            }

            db.Entry(Pregunta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntaExists(id))
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
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult PostPregunta(Pregunta Pregunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Preguntas.Add(Pregunta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Pregunta.Id }, Pregunta);
        }

        // DELETE: api/Pregunta/5
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult DeletePregunta(int id)
        {
            Pregunta Pregunta = db.Preguntas.Find(id);
            if (Pregunta == null)
            {
                return NotFound();
            }

            db.Preguntas.Remove(Pregunta);
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

        private bool PreguntaExists(int id)
        {
            return db.Preguntas.Count(e => e.Id == id) > 0;
        }
    }
}