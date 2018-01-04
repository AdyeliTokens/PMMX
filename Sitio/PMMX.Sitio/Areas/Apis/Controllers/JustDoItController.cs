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
using PMMX.Modelo.Vistas;
using PMMX.Seguridad.Servicios;
using Sitio.Helpers;
using PMMX.Modelo.Entidades.Operaciones;


namespace Sitio.Areas.Apis.Controllers
{
    public class JustDoItController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/JustDoIt
        public IQueryable<JustDoIt> GetJustDoIt()
        {
            return db.JustDoIt;
        }

        // GET: api/JustDoIt/5
        [ResponseType(typeof(JustDoItView))]
        public IHttpActionResult GetJustDoIt(int id)
        {
            var justDoIt = db.JustDoIt
               .Where(d => (d.Id == id))
               .Select(d => new JustDoItView
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
                       Path = f.Path,
                   }).ToList()
               }).FirstOrDefault();

            if (justDoIt == null)
            {
                return NotFound();
            }

            return Ok(justDoIt);
        }

        [ResponseType(typeof(IList<JustDoItView>))]
        public IHttpActionResult getJustDoItByResponsable(int idResponsable, Boolean activo, int diasDesde, int diasHasta)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(-diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);
            
            IList<JustDoItView> justDoIt = db.JustDoIt
                .Where(d => (d.Activo == activo) && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && (d.IdResponsable == idResponsable))
                .Select(d => new JustDoItView
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

            return Ok(justDoIt);
        }

        [ResponseType(typeof(IList<JustDoItView>))]
        public IHttpActionResult GetJustDoItByReportador(int idReportador, Boolean activo, int diasDesde, int diasHasta)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(-diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);

            IList<JustDoItView> justDoIt = db.JustDoIt
                .Where(d => (d.Activo == activo) && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && (d.IdResponsable == idReportador))
                .Select(d => new JustDoItView
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

            return Ok(justDoIt);
        }

        [ResponseType(typeof(IList<JustDoItView>))]
        public IHttpActionResult GetJustDoItByArea(int idArea, Boolean activo, int diasDesde, int diasHasta)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(-diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);

            IList<JustDoItView> justDoIt = db.JustDoIt
                .Where(d => (d.Activo == activo) && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && (d.Origen.WorkCenter.BussinesUnit.IdArea == idArea))
                .Select(d => new JustDoItView
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

            return Ok(justDoIt);
        }
        
        // PUT: api/JustDoIt/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJustDoIt(int id, JustDoIt justDoIt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != justDoIt.Id)
            {
                return BadRequest();
            }

            db.Entry(justDoIt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JustDoItExists(id))
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
        [ResponseType(typeof(JustDoItView))]
        public IHttpActionResult JustDoItByActivo(int id, Boolean activo)
        {
            JustDoIt justDoIt = db.JustDoIt.Where(x => x.Id == id).FirstOrDefault();

            if (justDoIt == null)
            {
                return NotFound();
            }
            
            justDoIt.Activo = activo;
            db.Entry(justDoIt).State = EntityState.Modified;
            db.SaveChanges();

            var justDoItView = db.JustDoIt
                .Where(d => (d.Id == id))
                .Select(d => new JustDoItView
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

            if (justDoItView == null)
            {
                return NotFound();
            }

            return Ok(justDoItView);
        }

        [HttpPut]
        [ResponseType(typeof(JustDoItView))]
        public IHttpActionResult PutJustDoItByFechaEstimada(int id, int dia, int mes, int year)
        {
            JustDoIt justDoIt = db.JustDoIt.Where(x => x.Id == id).FirstOrDefault();

            if (justDoIt == null)
            {
                return NotFound();
            }
            DateTime fechaEstimada = new DateTime(year, mes, dia);

            justDoIt.FechaEstimada = fechaEstimada;
            db.Entry(justDoIt).State = EntityState.Modified;
            db.SaveChanges();

            var justDoItView = db.JustDoIt
                .Where(d => (d.Id == id))
                .Select(d => new JustDoItView
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

            if (justDoItView == null)
            {
                return NotFound();
            }

            return Ok(justDoItView);
        }

        [HttpPut]
        [ResponseType(typeof(JustDoItView))]
        public IHttpActionResult PutJustDoItByResponsable(int id, int idResponsable)
        {
            JustDoIt justDoIt = db.JustDoIt.Where(x => x.Id == id).FirstOrDefault();

            if (justDoIt == null)
            {
                return NotFound();
            }

            justDoIt.IdResponsable = idResponsable;
            db.SaveChanges();

            var justDoItView = db.JustDoIt
                .Where(d => (d.Id == id))
                .Select(d => new DefectoView
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

            return Ok(justDoItView);
        }

        // POST: api/JustDoIt
        [ResponseType(typeof(JustDoItView))]
        public IHttpActionResult PostJustDoIt(JustDoIt justDoIt)
        {
            justDoIt.Activo = true;
            justDoIt.FechaReporte = DateTime.Now;
            justDoIt.Fotos = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JustDoIt.Add(justDoIt);
            db.SaveChanges();
            
            var justDoItView = db.JustDoIt
                .Where(d => d.Id == justDoIt.Id)
                .Select(d => new JustDoItView
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

            List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByJDI(justDoIt.Id);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
                notify.SendPushNotification(notificacion, "Se le ha asignado el JustDoIt " + justDoItView.Descripcion + ".", "Nuevo Just Do It reportado en " + justDoItView.Origen.WorkCenter.BussinesUnit.Area.Nombre + ".");
            }

            return Ok(justDoItView);
        }

        // DELETE: api/JustDoIt/5
        [ResponseType(typeof(JustDoIt))]
        public IHttpActionResult DeleteJustDoIt(int id)
        {
            JustDoIt justDoIt = db.JustDoIt.Find(id);
            if (justDoIt == null)
            {
                return NotFound();
            }

            db.JustDoIt.Remove(justDoIt);
            db.SaveChanges();

            return Ok(justDoIt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JustDoItExists(int id)
        {
            return db.JustDoIt.Count(e => e.Id == id) > 0;
        }
    }
}