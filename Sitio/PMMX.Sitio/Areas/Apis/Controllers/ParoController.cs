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
using PMMX.Modelo.Entidades.Paros;
using PMMX.Modelo.Vistas;
using PMMX.Modelo;
using PMMX.Modelo.Entidades;
using Maya.Helpers;
using PMMX.Seguridad.Servicios;

namespace Sitio.Areas.Apis.Controllers
{
    public class ParoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Paro
        public IQueryable<Paro> GetParo()
        {

            return db.Paros;
        }

        // GET: api/Paro/5
        [ResponseType(typeof(Paro))]
        public IHttpActionResult GetParo(int id)
        {
            var paro = db.Paros.Where(x => x.Id == id).Select(x => new ParoView
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
                FechaReporte = x.FechaReporte,
                IdMecanico = x.IdMecanico,
                IdOrigen = x.IdOrigen,
                IdReportador = x.IdReportador,
                Motivo = x.Motivo,
                TiempoDeParos = x.TiemposDeParo.Where(t => t.IdParo == id).Select(t => new TiempoDeParoView
                {
                    Id = t.Id,
                    IdParo = id,
                    Inicio = t.Inicio,
                    Fin = t.Fin
                }).ToList(),
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

            }).FirstOrDefault();

            if (paro == null)
            {
                return NotFound();
            }

            return Ok(paro);
        }

        // GET: api/Defectos/5
        [ResponseType(typeof(IList<ParoView>))]
        public IHttpActionResult getParosByOrigen(int idOrigen, Boolean activo, int cantidad)
        {
            var paros = db.Paros
                .Where(x => (x.Activo == activo && x.Origen.Id == idOrigen))
                .OrderByDescending(x => x.FechaReporte)
                .Take(cantidad)
                .Select(x => new ParoView
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    FechaReporte = x.FechaReporte,
                    Activo = x.Activo,
                    IdReportador = x.IdReportador,
                    Reportador = new PersonaView
                    {
                        Id = x.Reportador.Id,
                        Nombre = x.Reportador.Nombre,
                        Apellido1 = x.Reportador.Apellido1,
                        Apellido2 = x.Reportador.Apellido2,
                        Activo = x.Reportador.Activo,

                    },
                    IdMecanico = x.IdMecanico,
                    IdOrigen = x.IdOrigen,
                    Origen = new OrigenView
                    {
                        Id = x.Origen.Id,
                        IdModulo = x.Origen.IdModulo,
                        IdWorkCenter = x.Origen.IdWorkCenter,
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

                }).ToList();




            if (paros == null)
            {
                return NotFound();
            }

            return Ok(paros);
        }

        // GET: api/Defectos/5
        [ResponseType(typeof(IList<ParoView>))]
        public IHttpActionResult getParosByBussinesUnit(int idBussinesUnit, Boolean activo, int cantidad)
        {
            var paros = db.Paros
                .Where(x => (x.Activo == activo && x.Origen.WorkCenter.IdBussinesUnit == idBussinesUnit))
                .OrderByDescending(x => x.FechaReporte)
                .Take(cantidad)
                .Select(x => new ParoView
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    FechaReporte = x.FechaReporte,
                    Activo = x.Activo,
                    IdReportador = x.IdReportador,
                    Reportador = new PersonaView
                    {
                        Id = x.Reportador.Id,
                        Nombre = x.Reportador.Nombre,
                        Apellido1 = x.Reportador.Apellido1,
                        Apellido2 = x.Reportador.Apellido2,
                        Activo = x.Reportador.Activo,

                    },
                    IdMecanico = x.IdMecanico,
                    IdOrigen = x.IdOrigen,
                    Origen = new OrigenView
                    {
                        Id = x.Origen.Id,
                        IdModulo = x.Origen.IdModulo,
                        IdWorkCenter = x.Origen.IdWorkCenter,
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

                }).ToList();




            if (paros == null)
            {
                return NotFound();
            }

            return Ok(paros);
        }

        [ResponseType(typeof(IList<ParoView>))]
        public IHttpActionResult getParosByWorkCenter(int idWorkCenter, Boolean activo, int cantidad)
        {
            var paros = db.Paros
                .Where(x => (x.Activo == activo && x.Origen.IdWorkCenter == idWorkCenter))
                .OrderByDescending(x => x.FechaReporte)
                .Take(cantidad)
                .Select(x => new ParoView
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    FechaReporte = x.FechaReporte,
                    Activo = x.Activo,
                    IdReportador = x.IdReportador,
                    Reportador = new PersonaView
                    {
                        Id = x.Reportador.Id,
                        Nombre = x.Reportador.Nombre,
                        Apellido1 = x.Reportador.Apellido1,
                        Apellido2 = x.Reportador.Apellido2,
                        Activo = x.Reportador.Activo,

                    },
                    IdMecanico = x.IdMecanico,
                    IdOrigen = x.IdOrigen,
                    Origen = new OrigenView
                    {
                        Id = x.Origen.Id,
                        IdModulo = x.Origen.IdModulo,
                        IdWorkCenter = x.Origen.IdWorkCenter,
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

                }).ToList();




            if (paros == null)
            {
                return NotFound();
            }

            return Ok(paros);
        }

        // PUT: api/Paro/5
        [ResponseType(typeof(Paro))]
        [HttpPut]
        public IHttpActionResult PutParo(int id, Paro paro)
        {



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paro.Id)
            {
                return BadRequest();
            }

            db.Entry(paro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParoExists(id))
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
        [ResponseType(typeof(ParoView))]
        public IHttpActionResult PutParoByAsignacion(int id, int idMecanico)
        {
            Paro paro = db.Paros.Find(id);
            if (paro == null)
            {
                return NotFound();
            }

            paro.IdMecanico = idMecanico;
            ActividadEnParo actividad = new ActividadEnParo() { IdParo = id, Fecha = DateTime.Now, IdPersona = idMecanico, Descripcion = "Asignacion de paro" };
            if (paro.ActividadesEnParo == null)
            {
                paro.ActividadesEnParo = new List<ActividadEnParo>() { actividad };
            }
            else
            {
                paro.ActividadesEnParo.Add(actividad);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(paro).State = EntityState.Modified;


            db.SaveChanges();

            var paroreturn = db.Paros.Where(x => x.Id == paro.Id).Select(x => new ParoView
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
                FechaReporte = x.FechaReporte,
                IdMecanico = x.IdMecanico,
                IdOrigen = x.IdOrigen,
                IdReportador = x.IdReportador,
                Motivo = x.Motivo,
                TiempoDeParos = x.TiemposDeParo.Where(t => t.IdParo == id).Select(t => new TiempoDeParoView
                {
                    Id = t.Id,
                    IdParo = id,
                    Inicio = t.Inicio,
                    Fin = t.Fin
                }).ToList(),
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

            }).FirstOrDefault();

            if (paroreturn == null)
            {
                return NotFound();
            }

            return Ok(paroreturn);
        }

        [ResponseType(typeof(Paro))]
        public IHttpActionResult PutMotivoParo(int id, int idMecanico, string motivo)
        {
            Paro paro = db.Paros.Find(id);
            if (paro == null)
            {
                return NotFound();
            }

            paro.Motivo = motivo;

            ActividadEnParo actividad = new ActividadEnParo() { IdParo = id, Fecha = DateTime.Now, IdPersona = idMecanico, Descripcion = "Motivo de Paro: " + motivo };
            if (paro.ActividadesEnParo == null)
            {
                paro.ActividadesEnParo = new List<ActividadEnParo>() { actividad };
            }
            else
            {
                paro.ActividadesEnParo.Add(actividad);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(paro).State = EntityState.Modified;


            db.SaveChanges();

            var paroreturn = db.Paros.Where(x => x.Id == paro.Id).Select(x => new ParoView
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
                FechaReporte = x.FechaReporte,
                IdMecanico = x.IdMecanico,
                IdOrigen = x.IdOrigen,
                IdReportador = x.IdReportador,
                Motivo = x.Motivo,
                TiempoDeParos = x.TiemposDeParo.Where(t => t.IdParo == id).Select(t => new TiempoDeParoView
                {
                    Id = t.Id,
                    IdParo = id,
                    Inicio = t.Inicio,
                    Fin = t.Fin
                }).ToList(),
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

            }).FirstOrDefault();

            if (paroreturn == null)
            {
                return NotFound();
            }

            return Ok(paroreturn);
        }

        // PUT: api/Paro/5
        [ResponseType(typeof(Paro))]
        public IHttpActionResult PutParoCambiarActivo(int id, int idPersona, bool activo)
        {
            Paro paro = db.Paros.Where(x => x.Id == id).FirstOrDefault();

            if (paro == null)
            {
                return NotFound();
            }
            TiempoDeParo tiempo;
            if (activo)
            {

                tiempo = new TiempoDeParo { Inicio = DateTime.Now };
                if (paro.TiemposDeParo == null)
                {
                    List<TiempoDeParo> tiempos = new List<TiempoDeParo>() { tiempo };
                    paro.TiemposDeParo = tiempos;
                }
                else
                {
                    paro.TiemposDeParo.Add(tiempo);
                }

            }
            else
            {
                tiempo = db.TiemposDeParo.Where(x => x.IdParo == id).OrderByDescending(x => x.Id).FirstOrDefault();
                if (tiempo == null)
                {
                    tiempo = new TiempoDeParo();
                    tiempo.Inicio = paro.FechaReporte;
                }
                tiempo.Fin = DateTime.Now;
                List<TiempoDeParo> tiempos = new List<TiempoDeParo>() { tiempo };
                paro.TiemposDeParo = tiempos;
            }

            paro.Activo = activo;
            ActividadEnParo actividad = new ActividadEnParo() { IdParo = id, Fecha = DateTime.Now, IdPersona = idPersona, Descripcion = "Cambio de Activo en paro" };
            if (paro.ActividadesEnParo == null)
            {
                paro.ActividadesEnParo = new List<ActividadEnParo>() { actividad };
            }
            else
            {
                paro.ActividadesEnParo.Add(actividad);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(paro).State = EntityState.Modified;


            db.SaveChanges();


            var paroreturn = db.Paros.Where(x => x.Id == paro.Id).Select(x => new ParoView
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
                FechaReporte = x.FechaReporte,
                IdMecanico = x.IdMecanico,
                IdOrigen = x.IdOrigen,
                IdReportador = x.IdReportador,
                Motivo = x.Motivo,
                TiempoDeParos = x.TiemposDeParo.Where(t => t.IdParo == id).Select(t => new TiempoDeParoView
                {
                    Id = t.Id,
                    IdParo = id,
                    Inicio = t.Inicio,
                    Fin = t.Fin
                }).ToList(),
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
                        Activo = x.Origen.WorkCenter.Activo,
                         IdBussinesUnit= x.Origen.WorkCenter.IdBussinesUnit

                    }

                }

            }).FirstOrDefault();
            if (activo) {
                NotificationService notify = new NotificationService();
                UsuarioServicio usuarioServicio = new UsuarioServicio();
                List<DispositivoView> dispositivos = usuarioServicio.GetDispositivosDeMecanicosByBussinesUnit(paroreturn.Origen.WorkCenter.IdBussinesUnit);
                List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                foreach (string notificacion in llaves)
                {
                    notify.SendPushNotification(notificacion, "Necesitan de tu ayuda en " + paroreturn.Origen.WorkCenter.NombreCorto, "Falla reabierta reportada");
                }
            }

            if (paroreturn == null)
            {
                return NotFound();
            }

            return Ok(paroreturn);
        }

        // POST: api/Paro
        
        [ResponseType(typeof(ParoView))]
        [HttpPost]
        public IHttpActionResult PostParo(Paro paro)
        {
            paro.FechaReporte = DateTime.Now;
            paro.Activo = true;
            paro.Descripcion = "";
            paro.Motivo = "";
            
            List<ActividadEnParo> actividades = new List<ActividadEnParo>();
            actividades.Add(new ActividadEnParo { Fecha = DateTime.Now, Descripcion = "Reporte de Paro", IdPersona = paro.IdReportador });
            List<TiempoDeParo> tiempoDeParo = new List<TiempoDeParo>();
            tiempoDeParo.Add(new TiempoDeParo { Inicio = DateTime.Now });
            paro.TiemposDeParo = tiempoDeParo;



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paros.Add(paro);
            db.SaveChanges();



            var paroAdded = db.Paros.Where(x => x.Id == paro.Id).Select(x => new ParoView
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Activo = x.Activo,
                FechaReporte = x.FechaReporte,
                IdMecanico = x.IdMecanico,
                IdOrigen = x.IdOrigen,
                IdReportador = x.IdReportador,
                TiempoDeParos = x.TiemposDeParo.Where(t => t.IdParo == paro.Id).Select(t => new TiempoDeParoView
                {
                    Id = t.Id,
                    IdParo = paro.Id,
                    Inicio = t.Inicio,
                    Fin = t.Fin
                }).ToList(),
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
                        Activo = x.Origen.WorkCenter.Activo,
                         IdBussinesUnit = x.Origen.WorkCenter.IdBussinesUnit

                    }

                }

            }).FirstOrDefault();


            NotificationService notify = new NotificationService();
            UsuarioServicio usuarioServicio = new UsuarioServicio();

            List<DispositivoView> dispositivos = usuarioServicio.GetMecanicosPorOrigen(paroAdded.IdOrigen);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
                notify.SendPushNotification(notificacion, "El modulo " + paroAdded.Origen.Modulo.NombreCorto + " necesita de tu ayuda urgentemente.", "Nueva Falla reportada en " + paroAdded.Origen.WorkCenter.NombreCorto + " por favor ve lo mas pronto posible a ayudarlos.");
            }

            return Ok(paroAdded);
        }

        // POST: api/Paro
        [ResponseType(typeof(Paro))]
        public IHttpActionResult PostParoSerialize(int idOrigen, int idReporteador)
        {
            Paro paro = new Paro();


            paro.Descripcion = "pero que pex  u.u";
            paro.FechaReporte = DateTime.Now;
            paro.IdOrigen = idOrigen;
            paro.IdReportador = idReporteador;
            paro.Activo = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paros.Add(paro);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = paro.Id }, paro);

            //return paro.Id;

        }

        // DELETE: api/Paro/5
        [ResponseType(typeof(Paro))]
        public IHttpActionResult DeleteParo(int id)
        {
            Paro paro = db.Paros.Find(id);
            if (paro == null)
            {
                return NotFound();
            }

            db.Paros.Remove(paro);
            db.SaveChanges();

            return Ok(paro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParoExists(int id)
        {
            return db.Paros.Count(e => e.Id == id) > 0;
        }
    }
}