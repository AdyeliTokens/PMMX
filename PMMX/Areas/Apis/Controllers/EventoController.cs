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
using Maya.Helpers;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    public class EventoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Evento
        public IQueryable<Evento> GetEvento()
        {
            return db.Evento;
        }

        // GET: api/JustDoIt/5
        [ResponseType(typeof(EventoView))]
        
        public IHttpActionResult GetEventobyResponsableandFecha(int idResponsable, int dias, bool activo)
        {
            var today = DateTime.Now.Date;
            DateTime dateInit = today.AddDays(dias);

            var evento = db.Evento
               .Where(d => (d.IdResponsable == idResponsable) && (d.FechaInicio >= dateInit) && (d.Activo == activo))
               .Select(d => new EventoView
               {
                   Id = d.Id,
                   IdOrigen = d.IdOrigen,
                   IdResponsable = d.IdResponsable,
                   IdCategoria = d.IdCategoria,
                   Descripcion = d.Descripcion,
                   FechaInicio = d.FechaInicio,
                   FechaFin = d.FechaFin,
                   Activo = d.Activo,
                   Responsable = new PersonaView
                   {
                       Id = d.Responsable.Id,
                       Apellido1 = d.Responsable.Apellido1,
                       Apellido2 = d.Responsable.Apellido2,
                       Nombre = d.Responsable.Nombre,

                       Puesto = new PuestoView
                       {
                           Id = d.Responsable.Puesto.Id,
                           Nombre = d.Responsable.Puesto.Nombre
                       }
                   },
                   Origen = new OrigenView
                   {
                       Id = d.Origen.Id,
                       IdModulo = d.Origen.IdModulo,
                       IdWorkCenter = d.Origen.IdWorkCenter,
                       Modulo = new ModuloView
                       {
                           Id = d.Origen.Modulo.Id,
                           Nombre = d.Origen.Modulo.Nombre,
                           NombreCorto = d.Origen.Modulo.NombreCorto
                       },
                       WorkCenter = new WorkCenterView
                       {
                           Id = d.Origen.WorkCenter.Id,
                           Nombre = d.Origen.WorkCenter.Nombre,
                           NombreCorto = d.Origen.WorkCenter.NombreCorto,
                           Activo = d.Origen.WorkCenter.Activo
                       }
                   }
               }).ToList();

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [ResponseType(typeof(ModuloSeccionView))]
        public IHttpActionResult GetEventobyResponsableandFecha(int idResponsable, DateTime fecha, bool activo)
        {
            DateTime hoy = fecha.Date;
            DateTime mañana = hoy.AddDays(1);

            var evento = db.Evento
               .Where(d => (d.IdResponsable == idResponsable) && (d.FechaInicio >= hoy && d.FechaInicio <= mañana) && (d.Activo == activo))
               .Select(d => new EventoView
               {
                   Id = d.Id,
                   IdOrigen = d.IdOrigen,
                   IdResponsable = d.IdResponsable,
                   IdCategoria = d.IdCategoria,
                   Descripcion = d.Descripcion,
                   FechaInicio = d.FechaInicio,
                   FechaFin = d.FechaFin,
                   Activo = d.Activo,
                   Responsable = new PersonaView
                   {
                       Id = d.Responsable.Id,
                       Apellido1 = d.Responsable.Apellido1,
                       Apellido2 = d.Responsable.Apellido2,
                       Nombre = d.Responsable.Nombre,

                       Puesto = new PuestoView
                       {
                           Id = d.Responsable.Puesto.Id,
                           Nombre = d.Responsable.Puesto.Nombre
                       }
                   },
                   Origen = new OrigenView
                   {
                       Id = d.Origen.Id,
                       IdModulo = d.Origen.IdModulo,
                       IdWorkCenter = d.Origen.IdWorkCenter,
                       Modulo = new ModuloView
                       {
                           Id = d.Origen.Modulo.Id,
                           Nombre = d.Origen.Modulo.Nombre,
                           NombreCorto = d.Origen.Modulo.NombreCorto
                       },
                       WorkCenter = new WorkCenterView
                       {
                           Id = d.Origen.WorkCenter.Id,
                           Nombre = d.Origen.WorkCenter.Nombre,
                           NombreCorto = d.Origen.WorkCenter.NombreCorto,
                           Activo = d.Origen.WorkCenter.Activo
                       }
                   }
               }).ToList();
            
            return Ok(evento);
        }
        
        // GET: api/Evento/5
        [ResponseType(typeof(Evento))]
        public IHttpActionResult GetEvento(int id)
        {
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        // GET: api/Evento/5
        //[ResponseType(typeof(Evento))]
        //public IHttpActionResult GetEventoByResponsable(int IdResponsable)
        //{
        //    var _evento = new db.

        //    if (evento == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(evento);
        //}

        // PUT: api/Evento/5

        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvento(int id, Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento.Id)
            {
                return BadRequest();
            }

            db.Entry(evento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Evento
        [ResponseType(typeof(Evento))]
        public IHttpActionResult PostEvento(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Evento.Add(evento);
            db.SaveChanges();

            NotificationService notify = new NotificationService();
            UsuarioServicio usuarioServicio = new UsuarioServicio();

            List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(evento.Id);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
                var _evento = db.Evento.Where(e => e.Id == evento.Id)
                    .Select(s => new EventoView
                    {
                        Id = s.Id,
                        Descripcion = s.Descripcion,
                        Categoria = new CategoriaView
                        {
                            Id = s.Categoria.Id,
                            Nombre = s.Categoria.Nombre,
                            NombreCorto = s.Categoria.NombreCorto
                        },
                        Origen = new OrigenView
                        {
                            Id = s.Origen.Id,
                            WorkCenter = new WorkCenterView
                            {
                                Id = s.Origen.WorkCenter.Id,
                                BussinesUnit = new BussinesUnitView
                                {
                                    Id = s.Origen.WorkCenter.BussinesUnit.Id,
                                    Area = new AreaView
                                    {
                                        Id = s.Origen.WorkCenter.BussinesUnit.Area.Id,
                                        Nombre = s.Origen.WorkCenter.BussinesUnit.Area.Nombre,
                                        NombreCorto = s.Origen.WorkCenter.BussinesUnit.Area.NombreCorto
                                    }
                                }
                            }
                            
                        }
                    }).FirstOrDefault();


                notify.SendPushNotification(notificacion, "Se le ha asignado un nuevo evento: " + _evento.Descripcion + ". ", _evento.Categoria.Nombre + " : originado en " + _evento.Origen.WorkCenter.BussinesUnit.Area.Nombre);
            }
            
            return CreatedAtRoute("DefaultApi", new { id = evento.Id }, evento);
        }

        // DELETE: api/Evento/5
        [ResponseType(typeof(Evento))]
        public IHttpActionResult DeleteEvento(int id)
        {
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return NotFound();
            }

            db.Evento.Remove(evento);
            db.SaveChanges();

            return Ok(evento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoExists(int id)
        {
            return db.Evento.Count(e => e.Id == id) > 0;
        }
    }
}