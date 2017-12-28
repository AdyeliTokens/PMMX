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
using Maya.Helpers;
using PMMX.Seguridad.Servicios;

namespace Sitio.Areas.Apis.Controllers
{
    public class MantenimientoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Mantenimiento
        public IQueryable<Mantenimiento> GetMantenimiento()
        {
            return db.Mantenimiento;
        }

        // GET: api/Mantenimiento/5
        [ResponseType(typeof(MantenimientoView))]
        public IHttpActionResult GetMantenimiento(int id)
        {
            var mantenimiento = db.Mantenimiento
               .Where(d => (d.Id == id))
               .Select(d => new MantenimientoView
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
                   Reportador = new PersonaView
                   {
                       Id = d.Reportador.Id,
                       Apellido1 = d.Reportador.Apellido1,
                       Apellido2 = d.Reportador.Apellido2,
                       Nombre = d.Reportador.Nombre,

                       Puesto = new PuestoView
                       {
                           Id = d.Reportador.Puesto.Id,
                           Nombre = d.Reportador.Puesto.Nombre
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
                           NombreCorto = d.Origen.Modulo.NombreCorto,
                           Activo = d.Origen.Modulo.Activo
                       },
                       WorkCenter = new WorkCenterView
                       {
                           Id = d.Origen.WorkCenter.Id,
                           Nombre = d.Origen.WorkCenter.Nombre,
                           NombreCorto = d.Origen.WorkCenter.NombreCorto,
                           Activo = d.Origen.WorkCenter.Activo
                       }
                   },
                   Fotos = d.Fotos.Select(f => new FotoView
                   {
                       Id = f.Id,
                       Nombre = f.Nombre,
                       Path = f.Path
                   }).ToList()
               }).FirstOrDefault();

            if (mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(mantenimiento);
        }

        [ResponseType(typeof(IList<MantenimientoView>))]
        public IHttpActionResult getMantenimientoByResponsable(int idResponsable, Boolean activo, int diasDesde, int diasHasta)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(-diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);

            IList<MantenimientoView> mantenimiento = db.Mantenimiento
                .Where(d => (d.Activo == activo) && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && (d.IdResponsable == idResponsable))
                .Select(d => new MantenimientoView
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
                    Reportador = new PersonaView
                    {
                        Id = d.Reportador.Id,
                        Nombre = d.Reportador.Nombre,
                        Apellido1 = d.Reportador.Apellido1,
                        Apellido2 = d.Reportador.Apellido2,
                        Activo = d.Reportador.Activo
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
                            NombreCorto = d.Origen.Modulo.NombreCorto,
                            Activo = d.Origen.Modulo.Activo
                        },
                        WorkCenter = new WorkCenterView
                        {
                            Id = d.Origen.WorkCenter.Id,
                            Nombre = d.Origen.WorkCenter.Nombre,
                            NombreCorto = d.Origen.WorkCenter.NombreCorto,
                            Activo = d.Origen.WorkCenter.Activo
                        }

                    },
                    Fotos = d.Fotos.Select(f => new FotoView
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Path = f.Path
                    }).ToList()
                }).ToList();

            return Ok(mantenimiento);
        }

        [ResponseType(typeof(IList<JustDoItView>))]
        public IHttpActionResult GetMantenimientoByReportador(int idReportador, Boolean activo, int diasDesde, int diasHasta)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(-diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);

            IList<MantenimientoView> mantenimiento = db.Mantenimiento
                .Where(d => (d.Activo == activo) && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && (d.IdResponsable == idReportador))
                .Select(d => new MantenimientoView
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
                    Reportador = new PersonaView
                    {
                        Id = d.Reportador.Id,
                        Nombre = d.Reportador.Nombre,
                        Apellido1 = d.Reportador.Apellido1,
                        Apellido2 = d.Reportador.Apellido2,
                        Activo = d.Reportador.Activo
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
                            NombreCorto = d.Origen.Modulo.NombreCorto,
                            Activo = d.Origen.Modulo.Activo
                        },
                        WorkCenter = new WorkCenterView
                        {
                            Id = d.Origen.WorkCenter.Id,
                            Nombre = d.Origen.WorkCenter.Nombre,
                            NombreCorto = d.Origen.WorkCenter.NombreCorto,
                            Activo = d.Origen.WorkCenter.Activo
                        }

                    },
                    Fotos = d.Fotos.Select(f => new FotoView
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Path = f.Path
                    }).ToList()
                }).ToList();

            return Ok(mantenimiento);
        }

        [ResponseType(typeof(IList<JustDoItView>))]
        public IHttpActionResult GetMantenimientotByArea(int idArea, Boolean activo, int diasDesde, int diasHasta)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(-diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);

            IList<MantenimientoView> mantenimiento = db.Mantenimiento
                .Where(d => (d.Activo == activo) && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && (d.Origen.WorkCenter.BussinesUnit.IdArea == idArea))
                .Select(d => new MantenimientoView
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
                    Reportador = new PersonaView
                    {
                        Id = d.Reportador.Id,
                        Nombre = d.Reportador.Nombre,
                        Apellido1 = d.Reportador.Apellido1,
                        Apellido2 = d.Reportador.Apellido2,
                        Activo = d.Reportador.Activo
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
                            NombreCorto = d.Origen.Modulo.NombreCorto,
                            Activo = d.Origen.Modulo.Activo
                        },
                        WorkCenter = new WorkCenterView
                        {
                            Id = d.Origen.WorkCenter.Id,
                            Nombre = d.Origen.WorkCenter.Nombre,
                            NombreCorto = d.Origen.WorkCenter.NombreCorto,
                            Activo = d.Origen.WorkCenter.Activo
                        }

                    },
                    Fotos = d.Fotos.Select(f => new FotoView
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Path = f.Path
                    }).ToList()
                }).ToList();

            return Ok(mantenimiento);
        }
        
        // PUT: api/Mantenimiento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMantenimiento(int id, Mantenimiento mantenimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mantenimiento.Id)
            {
                return BadRequest();
            }

            db.Entry(mantenimiento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoExists(id))
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

        [HttpPut]
        [ResponseType(typeof(MantenimientoView))]
        public IHttpActionResult PutMantenimientoByActivo(int id, Boolean activo)
        {
            Mantenimiento mantenimiento = db.Mantenimiento.Where(x => x.Id == id).FirstOrDefault();

            if (mantenimiento == null)
            {
                return NotFound();
            }

            mantenimiento.Activo = activo;
            db.Entry(mantenimiento).State = EntityState.Modified;
            db.SaveChanges();

            var mantenimientoView = db.Mantenimiento
                .Where(d => (d.Id == id))
                .Select(d => new MantenimientoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    IdResponsable = d.IdResponsable,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Reportador = new PersonaView
                    {
                        Id = d.Reportador.Id,
                        Apellido1 = d.Reportador.Apellido1,
                        Apellido2 = d.Reportador.Apellido2,
                        Nombre = d.Reportador.Nombre,

                        Puesto = new PuestoView
                        {
                            Id = d.Reportador.Puesto.Id,
                            Nombre = d.Reportador.Puesto.Nombre
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
                            NombreCorto = d.Origen.Modulo.NombreCorto,
                            Activo = d.Origen.Modulo.Activo
                        },
                        WorkCenter = new WorkCenterView
                        {
                            Id = d.Origen.WorkCenter.Id,
                            Nombre = d.Origen.WorkCenter.Nombre,
                            NombreCorto = d.Origen.WorkCenter.NombreCorto,
                            Activo = d.Origen.WorkCenter.Activo
                        }

                    },
                    Fotos = d.Fotos.Select(f => new FotoView
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Path = f.Path
                    }).ToList()
                }).FirstOrDefault();

            if (mantenimientoView == null)
            {
                return NotFound();
            }

            return Ok(mantenimientoView);
        }

        [HttpPut]
        [ResponseType(typeof(MantenimientoView))]
        public IHttpActionResult PutMantenimientoByFechaEstimada(int id, int dia, int mes, int year)
        {
            Mantenimiento mantenimiento = db.Mantenimiento.Where(x => x.Id == id).FirstOrDefault();

            if (mantenimiento == null)
            {
                return NotFound();
            }
            DateTime fechaEstimada = new DateTime(year, mes, dia);

            mantenimiento.FechaEstimada = fechaEstimada;
            db.Entry(mantenimiento).State = EntityState.Modified;
            db.SaveChanges();

            var mantenimientoView = db.Mantenimiento
                .Where(d => (d.Id == id))
                .Select(d => new MantenimientoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    IdResponsable = d.IdResponsable,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Reportador = new PersonaView
                    {
                        Id = d.Reportador.Id,
                        Apellido1 = d.Reportador.Apellido1,
                        Apellido2 = d.Reportador.Apellido2,
                        Nombre = d.Reportador.Nombre,

                        Puesto = new PuestoView
                        {
                            Id = d.Reportador.Puesto.Id,
                            Nombre = d.Reportador.Puesto.Nombre
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
                            NombreCorto = d.Origen.Modulo.NombreCorto,
                            Activo = d.Origen.Modulo.Activo
                        },
                        WorkCenter = new WorkCenterView
                        {
                            Id = d.Origen.WorkCenter.Id,
                            Nombre = d.Origen.WorkCenter.Nombre,
                            NombreCorto = d.Origen.WorkCenter.NombreCorto,
                            Activo = d.Origen.WorkCenter.Activo
                        }

                    },
                    Fotos = d.Fotos.Select(f => new FotoView
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Path = f.Path
                    }).ToList()
                }).FirstOrDefault();

            if (mantenimientoView == null)
            {
                return NotFound();
            }

            return Ok(mantenimientoView);
        }

        [HttpPut]
        [ResponseType(typeof(MantenimientoView))]
        public IHttpActionResult PutMantenimientoByResponsable(int id, int idResponsable)
        {
            Mantenimiento mantenimiento = db.Mantenimiento.Where(x => x.Id == id).FirstOrDefault();

            if (mantenimiento == null)
            {
                return NotFound();
            }

            mantenimiento.IdResponsable = idResponsable;
            db.SaveChanges();

            var mantenimientoView = db.Mantenimiento
                .Where(d => (d.Id == id))
                .Select(d => new MantenimientoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    IdResponsable = d.IdResponsable,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Reportador = new PersonaView
                    {
                        Id = d.Reportador.Id,
                        Apellido1 = d.Reportador.Apellido1,
                        Apellido2 = d.Reportador.Apellido2,
                        Nombre = d.Reportador.Nombre,

                        Puesto = new PuestoView
                        {
                            Id = d.Reportador.Puesto.Id,
                            Nombre = d.Reportador.Puesto.Nombre
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
                            NombreCorto = d.Origen.Modulo.NombreCorto,
                            Activo = d.Origen.Modulo.Activo
                        },
                        WorkCenter = new WorkCenterView
                        {
                            Id = d.Origen.WorkCenter.Id,
                            Nombre = d.Origen.WorkCenter.Nombre,
                            NombreCorto = d.Origen.WorkCenter.NombreCorto,
                            Activo = d.Origen.WorkCenter.Activo
                        }

                    },
                    Fotos = d.Fotos.Select(f => new FotoView
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Path = f.Path
                    }).ToList()
                }).FirstOrDefault();

            return Ok(mantenimientoView);
        }

        // POST: api/Mantenimiento
        [ResponseType(typeof(MantenimientoView))]
        public IHttpActionResult PostMantenimiento(Mantenimiento mantenimiento)
        {
            mantenimiento.Activo = true;
            mantenimiento.FechaReporte = DateTime.Now;
            mantenimiento.Fotos = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mantenimiento.Add(mantenimiento);
            db.SaveChanges();

            var mantenimientoView = db.Mantenimiento
                 .Where(d => d.Id == mantenimiento.Id)
                 .Select(d => new MantenimientoView
                 {
                     Id = d.Id,
                     Origen = new OrigenView
                     {
                         Id = d.Origen.Id,
                         IdModulo = d.Origen.IdModulo,
                         IdWorkCenter = d.Origen.IdWorkCenter,
                         WorkCenter = new WorkCenterView
                         {
                             Id = d.Origen.WorkCenter.Id,
                             Nombre = d.Origen.WorkCenter.Nombre,
                             NombreCorto = d.Origen.WorkCenter.NombreCorto,
                             Activo = d.Origen.WorkCenter.Activo,
                             BussinesUnit = new BussinesUnitView
                             {
                                 Id = d.Origen.WorkCenter.BussinesUnit.Id,
                                 Area = new AreaView
                                 {
                                     Id = d.Origen.WorkCenter.BussinesUnit.Area.Id,
                                     Nombre = d.Origen.WorkCenter.BussinesUnit.Area.Nombre,
                                 }
                             }
                         }
                     }
                 }).FirstOrDefault();

            NotificationService notify = new NotificationService();
            UsuarioServicio usuarioServicio = new UsuarioServicio();

            List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByMantenimiento(mantenimiento.Id);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
                notify.SendPushNotification(notificacion, "Se le ha solicitado un nuevo Mantenimiento " + mantenimiento.Descripcion + ".", "Nuevo Mantenimiento para " + mantenimiento.Origen.WorkCenter.BussinesUnit.Area.Nombre + ".");
            }

            return Ok(mantenimientoView);
        }

        // DELETE: api/Mantenimiento/5
        [ResponseType(typeof(Mantenimiento))]
        public IHttpActionResult DeleteMantenimiento(int id)
        {
            Mantenimiento mantenimiento = db.Mantenimiento.Find(id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            db.Mantenimiento.Remove(mantenimiento);
            db.SaveChanges();

            return Ok(mantenimiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MantenimientoExists(int id)
        {
            return db.Mantenimiento.Count(e => e.Id == id) > 0;
        }
    }
}