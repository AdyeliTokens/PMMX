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
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    public class MecanicoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Mecanico
        public IQueryable<Persona> GetPersonas()
        {
            return db.Personas;
        }

        // GET: api/Mecanico/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetMecanico(int id)
        {
            PersonaView mecanico = new PersonaView();

            mecanico.ParosAsignados = db.Personas.Where(y => y.Id == id).Select(y => y.ParosAsignados.Where(p => p.Activo == true).Select(x => new ParoView
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
                FechaReporte = x.FechaReporte,
                IdMecanico = x.IdMecanico,
                IdOrigen = x.IdOrigen,
                IdReportador = x.IdReportador,
                Motivo = x.Motivo,
                Reportador = new PersonaView
                {
                    Id = x.Reportador.Id,
                    Nombre = x.Reportador.Nombre,
                    Apellido1 = x.Reportador.Apellido1,
                    Apellido2 = x.Reportador.Apellido2,
                    Puesto = new PuestoView
                    {
                        Id = x.Reportador.Puesto.Id,
                        Nombre = x.Reportador.Puesto.Nombre
                    }
                },
                Origen = new OrigenView
                {
                    Id = x.IdOrigen,
                    Modulo = new ModuloView
                    {
                        Id = x.Origen.Modulo.Id,
                        Nombre = x.Origen.Modulo.Nombre,
                        NombreCorto = x.Origen.Modulo.NombreCorto,
                        Activo = x.Origen.Modulo.Activo

                    },
                    WorkCenter = new WorkCenterView
                    {
                        Id = x.Origen.WorkCenter.Id,
                        Nombre = x.Origen.WorkCenter.Nombre,
                        NombreCorto = x.Origen.WorkCenter.NombreCorto,
                        Activo = x.Origen.WorkCenter.Activo

                    }

                }

            }).ToList()).FirstOrDefault();
            mecanico.DefectosAsignados = db.Personas.Where(y=> y.Id ==id ).Select(y => y.DefectosAsignados.Where(d => d.Activo == true).Select(d => new DefectoView
            {
                Id = d.Id,
                IdOrigen = d.IdOrigen,
                IdReportador = d.IdReportador,
                IdResponsable = d.IdResponsable,
                Descripcion = d.Descripcion,
                Activo = d.Activo,
                FechaReporte = d.FechaReporte,
                FechaEstimada = d.FechaEstimada,
                Prioridad = d.Prioridad,
                Fotos = d.Fotos.Select(f => new FotoView
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Path = f.Path
                }).ToList()
            }).ToList()).FirstOrDefault();


               


            return Ok(mecanico);
        }

        // PUT: api/Mecanico/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersona(int id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }

            db.Entry(persona).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // POST: api/Mecanico
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personas.Add(persona);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = persona.Id }, persona);
        }

        // DELETE: api/Mecanico/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(int id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.Personas.Remove(persona);
            db.SaveChanges();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(int id)
        {
            return db.Personas.Count(e => e.Id == id) > 0;
        }
    }
}