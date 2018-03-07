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
    public class GembaWalkController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/GembaWalk
        public IQueryable<GembaWalk> GetGembaWalk()
        {
            return db.GembaWalk;
        }

        // GET: api/GembaWalk/5
        [ResponseType(typeof(GembaWalkView))]
        public IHttpActionResult GetGembaWalk(int id)
        {
            var GembaWalk = db.GembaWalk
               .Where(d => (d.Id == id))
               .Select(d => new GembaWalkView
               {
                   Id = d.Id,
                   IdOrigen = d.IdOrigen,
                   IdReportador = d.IdReportador,
                   IdResponsable = d.IdResponsable,
                   IdSubCategoria = d.IdSubCategoria,
                   IdEvento = d.IdEvento,
                   Descripcion = d.Descripcion,
                   Activo = d.Activo,
                   FechaReporte = d.FechaReporte,
                   FechaEstimada = d.FechaEstimada,
                   Prioridad = d.Prioridad,
                   Tipo = d.Tipo,
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

            if (GembaWalk == null)
            {
                return NotFound();
            }

            return Ok(GembaWalk);
        }

        // GET: api/GembaWalk/5
        [ResponseType(typeof(GembaWalkView))]
        public IHttpActionResult GetGembaWalkbyTipo(int Tipo, int IdResponsable)
        {
            var GembaWalk = db.GembaWalk
               .Where(d => (d.Tipo == Tipo) && (d.IdResponsable == IdResponsable))
               .Select(d => new GembaWalkView
               {
                   Id = d.Id,
                   IdOrigen = d.IdOrigen,
                   IdReportador = d.IdReportador,
                   IdResponsable = d.IdResponsable,
                   IdSubCategoria = d.IdSubCategoria,
                   IdEvento = d.IdEvento,
                   Descripcion = d.Descripcion,
                   Activo = d.Activo,
                   FechaReporte = d.FechaReporte,
                   FechaEstimada = d.FechaEstimada,
                   Prioridad = d.Prioridad,
                   Tipo = d.Tipo
               }).ToList();

            if (GembaWalk == null)
            {
                return NotFound();
            }

            return Ok(GembaWalk);
        }

        // GET: api/GembaWalk/5
        [ResponseType(typeof(GembaWalkView))]
        public IHttpActionResult GetGembaWalkporStatus(int IdStatus, int IdResponsable)
        {
            var GembaWalk = db.BitacoraGembaWalks
               .Where(b => (b.IdStatus == IdStatus) && (b.GembaWalk.IdResponsable == IdResponsable))
               .Select(d => new GembaWalkView
               {
                   Id = d.GembaWalk.Id,
                   IdOrigen = d.GembaWalk.IdOrigen,
                   IdReportador = d.GembaWalk.IdReportador,
                   IdResponsable = d.GembaWalk.IdResponsable,
                   IdSubCategoria = d.GembaWalk.IdSubCategoria,
                   IdEvento = d.GembaWalk.IdEvento,
                   Descripcion = d.GembaWalk.Descripcion,
                   Activo = d.GembaWalk.Activo,
                   FechaReporte = d.GembaWalk.FechaReporte,
                   FechaEstimada = d.GembaWalk.FechaEstimada,
                   Prioridad = d.GembaWalk.Prioridad,
                   Tipo = d.GembaWalk.Tipo,
               }).ToList();

            if (GembaWalk == null)
            {
                return NotFound();
            }

            return Ok(GembaWalk);
        }

        // GET: api/Ventana/5
        [ResponseType(typeof(GembaWalk))]
        public IHttpActionResult GetGembaWalkbyEvento(int idEvento)
        {
            var gemaWalk = db.GembaWalk
                          .Where(ve => (ve.IdEvento == idEvento))
                          .Select(g => new GembaWalkView
                          {
                            Id = g.Id,
                            ReportadorNombre = db.Personas.Where(o => o.Id == g.IdReportador).Select(o => o.Nombre + " "+ o.Apellido1 +" "+ o.Apellido2).FirstOrDefault(),
                            OrigenNombre = db.Origens.Where(o => o.Id == g.IdOrigen).Select(o => o.WorkCenter.NombreCorto + " " + o.Modulo.NombreCorto).FirstOrDefault(),
                            Descripcion = g.Descripcion,
                            Activo = g.Activo,
                            FechaReporte = g.FechaReporte,
                            FechaEstimada = g.FechaEstimada,
                            Prioridad = g.Prioridad,
                            ResponsableNombre = db.Personas.Where(o => o.Id == g.IdResponsable).Select(o => o.Nombre + " " + o.Apellido1 + " " + o.Apellido2).FirstOrDefault(),
                            SubCategoriaNombre = db.SubCategoria.Where(o => o.Id == g.IdSubCategoria).Select(o => o.Nombre ).FirstOrDefault(),
                            TipoNombre = (g.Tipo == 1) ? "JustDoIt" : "CAPAIA",
                            StatusNombre = db.BitacoraGembaWalks.Where(s => s.IdGembaWalk == g.Id).Select(s => s.Estatus.Nombre).FirstOrDefault()
                          }).ToList();

            if (gemaWalk == null)
            {
                return NotFound();
            }

            return Ok(gemaWalk);
        }


        // PUT: api/GembaWalk/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGembaWalk(int id, GembaWalk GembaWalk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != GembaWalk.Id)
            {
                return BadRequest();
            }

            db.Entry(GembaWalk).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GembaWalkExists(id))
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
        [ResponseType(typeof(GembaWalkView))]
        public IHttpActionResult GembaWalkByActivo(int id, Boolean activo)
        {
            GembaWalk GembaWalk = db.GembaWalk.Where(x => x.Id == id).FirstOrDefault();

            if (GembaWalk == null)
            {
                return NotFound();
            }
            
            GembaWalk.Activo = activo;
            db.Entry(GembaWalk).State = EntityState.Modified;
            db.SaveChanges();

            var GembaWalkView = db.GembaWalk
                .Where(d => (d.Id == id))
                .Select(d => new GembaWalkView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    IdResponsable = d.IdResponsable,
                    IdEvento = d.IdEvento,
                    IdSubCategoria = d.IdSubCategoria,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Prioridad = d.Prioridad,
                    Tipo = d.Tipo
                }).FirstOrDefault();

            if (GembaWalkView == null)
            {
                return NotFound();
            }

            return Ok(GembaWalkView);
        }

        [HttpPut]
        [ResponseType(typeof(GembaWalkView))]
        public IHttpActionResult PutGembaWalkByFechaEstimada(int id, int dia, int mes, int year)
        {
            GembaWalk GembaWalk = db.GembaWalk.Where(x => x.Id == id).FirstOrDefault();

            if (GembaWalk == null)
            {
                return NotFound();
            }
            DateTime fechaEstimada = new DateTime(year, mes, dia);

            GembaWalk.FechaEstimada = fechaEstimada;
            db.Entry(GembaWalk).State = EntityState.Modified;
            db.SaveChanges();

            var GembaWalkView = db.GembaWalk
                .Where(d => (d.Id == id))
                .Select(d => new GembaWalkView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    IdResponsable = d.IdResponsable,
                    IdEvento = d.IdEvento,
                    IdSubCategoria = d.IdSubCategoria,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Prioridad = d.Prioridad,
                    Tipo = d.Tipo
                }).FirstOrDefault();

            if (GembaWalkView == null)
            {
                return NotFound();
            }

            return Ok(GembaWalkView);
        }

        [HttpPut]
        [ResponseType(typeof(GembaWalkView))]
        public IHttpActionResult PutGembaWalkByResponsable(int id, int idResponsable)
        {
            GembaWalk GembaWalk = db.GembaWalk.Where(x => x.Id == id).FirstOrDefault();

            if (GembaWalk == null)
            {
                return NotFound();
            }

            GembaWalk.IdResponsable = idResponsable;
            db.SaveChanges();

            var GembaWalkView = db.GembaWalk
                .Where(d => (d.Id == id))
                .Select(d => new GembaWalkView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    IdResponsable = d.IdResponsable,
                    IdEvento = d.IdEvento,
                    IdSubCategoria = d.IdSubCategoria,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Prioridad = d.Prioridad,
                    Tipo = d.Tipo
                }).FirstOrDefault();
            
            return Ok(GembaWalkView);
        }

        // POST: api/GembaWalk
        [ResponseType(typeof(GembaWalkView))]
        public IHttpActionResult PostGembaWalk(GembaWalk GembaWalk)
        {
            GembaWalk.Activo = true;
            GembaWalk.FechaReporte = DateTime.Now;
            GembaWalk.Fotos = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GembaWalk.Add(GembaWalk);
            db.SaveChanges();
            
            var GembaWalkView = db.GembaWalk
                .Where(d => d.Id == GembaWalk.Id)
                .Select(d => new GembaWalkView
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

            List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByJDI(GembaWalk.Id);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
                notify.SendPushNotification(notificacion, "Se le ha asignado el GembaWalk " + GembaWalkView.Descripcion + ".", "Nuevo GembaWalk reportado en " + GembaWalkView.Origen.WorkCenter.BussinesUnit.Area.Nombre + ".");
            }

            return Ok(GembaWalkView);
        }

        // DELETE: api/GembaWalk/5
        [ResponseType(typeof(GembaWalk))]
        public IHttpActionResult DeleteGembaWalk(int id)
        {
            GembaWalk GembaWalk = db.GembaWalk.Find(id);
            if (GembaWalk == null)
            {
                return NotFound();
            }

            db.GembaWalk.Remove(GembaWalk);
            db.SaveChanges();

            return Ok(GembaWalk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GembaWalkExists(int id)
        {
            return db.GembaWalk.Count(e => e.Id == id) > 0;
        }
    }
}