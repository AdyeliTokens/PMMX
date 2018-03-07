using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Vistas;
using Sitio.Helpers;
using PMMX.Seguridad.Servicios;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class DefectoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        public IQueryable<Defecto> GetDefecto()
        {
            return db.Defectos;
        }

        [ResponseType(typeof(DefectoView))]
        public IHttpActionResult GetDefecto(int id)
        {

            var defecto = db.Defectos
                .Where(d => (d.Id == id))
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Prioridad = d.Prioridad,
                    //NotificacionSAP = d.NotificacionSAP,
                    ComentariosCount = d.Comentarios.Count(),
                    ActividadesCount = d.Actividades.Count(),
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

                    }
                }).FirstOrDefault();

            if (defecto == null)
            {
                return NotFound();
            }

            return Ok(defecto);
        }

        [ResponseType(typeof(IList<DefectoView>))]
        public IHttpActionResult getDefectosByOrigen(int idOrigen, Boolean activo, int diasDesde, int diasHasta, int cantidad)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);



            IList<DefectoView> defectos = db.Defectos
                .Where(d => d.Activo == activo && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && d.Origen.Id == idOrigen)
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
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

                    }
                }).Take(cantidad).ToList();



            if (defectos == null)
            {
                return NotFound();
            }

            return Ok(defectos);
        }

        [ResponseType(typeof(IList<DefectoView>))]
        public IHttpActionResult getDefectosByBussinesUnit(int idBussinesUnit, Boolean activo, int diasDesde, int diasHasta, int cantidad)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);



            IList<DefectoView> defectos = db.Defectos
                .Where(d => d.Activo == activo && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && d.Origen.WorkCenter.IdBussinesUnit == idBussinesUnit)
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
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

                    }
                }).Take(cantidad).ToList();

            return Ok(defectos);
        }

        [ResponseType(typeof(IList<DefectoView>))]
        public IHttpActionResult getDefectosByWorkCenter(int idWorkCenter, Boolean activo, int diasDesde, int diasHasta, int cantidad)
        {
            DateTime fechaInicial = DateTime.Now.AddDays(diasDesde);
            DateTime fechaFinal = DateTime.Now.AddDays(diasHasta);



            IList<DefectoView> defectos = db.Defectos
                .Where(d => d.Activo == activo && (d.FechaReporte >= fechaInicial && d.FechaReporte <= fechaFinal) && d.Origen.IdWorkCenter == idWorkCenter)
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
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

                    }
                }).Take(cantidad).ToList();

            return Ok(defectos);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutDefecto(int id, Defecto defecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != defecto.Id)
            {
                return BadRequest();
            }

            db.Entry(defecto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DefectoExists(id))
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
        [ResponseType(typeof(DefectoView))]
        public IHttpActionResult PutDefectoByActivo(int id, Boolean activo)
        {
            Defecto defecto = db.Defectos.Where(x => x.Id == id).FirstOrDefault();

            if (defecto == null)
            {
                return NotFound();
            }

            ActividadEnDefecto actividad = new ActividadEnDefecto();
            if (activo)
            {
                actividad.Descripcion = "Defecto Abierto nuevamente!!";
            }
            else {
                actividad.Descripcion = "Defecto Cerrado!!";
            }
            actividad.IdEjecutante = defecto.IdReportador;
            actividad.Fecha = DateTime.Now;
            defecto.Actividades = new List<ActividadEnDefecto>();
            defecto.Actividades.Add(actividad);

            defecto.Activo = activo;
            db.Entry(defecto).State = EntityState.Modified;
            db.SaveChanges();

            var defectoView = db.Defectos
                .Where(d => (d.Id == id))
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    //NotificacionSAP = d.NotificacionSAP,
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

                    }
                }).FirstOrDefault();

            if (defectoView == null)
            {
                return NotFound();
            }

            return Ok(defectoView);


        }

        [HttpPut]
        [ResponseType(typeof(DefectoView))]
        public IHttpActionResult PutDefectoByFechaEstimada(int id, int dia, int mes, int year)
        {
            Defecto defecto = db.Defectos.Where(x => x.Id == id).FirstOrDefault();

            if (defecto == null)
            {
                return NotFound();
            }
            DateTime fechaEstimada = new DateTime(year, mes, dia);


            ActividadEnDefecto actividad = new ActividadEnDefecto();
            actividad.Descripcion = "Nueva Fecha Estimada";
            actividad.IdEjecutante = defecto.IdReportador;
            actividad.Fecha = DateTime.Now;
            defecto.Actividades = new List<ActividadEnDefecto>();
            defecto.Actividades.Add(actividad);


            defecto.FechaEstimada = fechaEstimada;
            db.Entry(defecto).State = EntityState.Modified;
            db.SaveChanges();

            var defectoView = db.Defectos
                .Where(d => (d.Id == id))
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    //NotificacionSAP = d.NotificacionSAP,
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

                    }
                }).FirstOrDefault();

            if (defectoView == null)
            {
                return NotFound();
            }

            return Ok(defectoView);


        }

        [HttpPut]
        [ResponseType(typeof(DefectoView))]
        public IHttpActionResult PutDefectoByResponsable(int id, int idResponsable)
        {
            Defecto defecto = db.Defectos.Where(x => x.Id == id).FirstOrDefault();

            if (defecto == null)
            {
                return NotFound();
            }

            ActividadEnDefecto actividad = new ActividadEnDefecto();
            actividad.Descripcion = "Nueva Asignacion a Defecto";
            actividad.IdEjecutante = defecto.IdReportador;
            actividad.Fecha = DateTime.Now;
            defecto.Actividades = new List<ActividadEnDefecto>();
            defecto.Actividades.Add(actividad);
            
            db.SaveChanges();

            var defectoView = db.Defectos
                .Where(d => (d.Id == id))
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    //NotificacionSAP = d.NotificacionSAP,
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

                    }
                }).FirstOrDefault();



            return Ok(defectoView);
        }

        [HttpPut]
        [ResponseType(typeof(DefectoView))]
        public IHttpActionResult PutDefectoBySAP(int id, String sap)
        {
            Defecto defecto = db.Defectos.Where(x => x.Id == id).FirstOrDefault();

            if (defecto == null)
            {
                return NotFound();
            }

            defecto.NotificacionSAP = sap;
            db.SaveChanges();

            var defectoView = db.Defectos
                .Where(d => (d.Id == id))
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    //NotificacionSAP = d.NotificacionSAP,
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

                    }
                }).FirstOrDefault();



            return Ok(defectoView);
        }

        [HttpPost]
        [ResponseType(typeof(RespuestaServicio<DefectoView>))]
        public IHttpActionResult PostDefecto(Defecto defecto)
        {
            RespuestaServicio<DefectoView> respuesta = new RespuestaServicio<DefectoView>();
            if (defecto != null)
            {
                defecto.Activo = true;
                defecto.FechaReporte = DateTime.Now;

                DefectoServicio servicio = new DefectoServicio(db);
                respuesta = servicio.PostDefecto(defecto);
                if (respuesta.EjecucionCorrecta)
                {
                    NotificationService notify = new NotificationService();
                    UsuarioServicio usuarioServicio = new UsuarioServicio();

                    List<DispositivoView> dispositivos = usuarioServicio.GetMecanicosPorOrigen(defecto.IdOrigen);
                    List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                    foreach (string notificacion in llaves)
                    {
                        notify.SendPushNotification(notificacion, "El modulo " + respuesta.Respuesta.Origen.Modulo.NombreCorto + " no parece estar funcionando muy bien.", "Nuevo defecto reportado en " + respuesta.Respuesta.Origen.WorkCenter.NombreCorto + ".");
                    }
                }
                else
                {

                }
            }
            else
            {
                respuesta.Mensaje = "El defecto no se puede agregar porque llego nulo :(";
            }
            
            return Ok(respuesta);

        }

        [ResponseType(typeof(Defecto))]
        public IHttpActionResult DeleteDefecto(int id)
        {
            Defecto defecto = db.Defectos.Find(id);
            if (defecto == null)
            {
                return NotFound();
            }

            db.Defectos.Remove(defecto);
            db.SaveChanges();

            return Ok(defecto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DefectoExists(int id)
        {
            return db.Defectos.Count(e => e.Id == id) > 0;
        }
    }
}