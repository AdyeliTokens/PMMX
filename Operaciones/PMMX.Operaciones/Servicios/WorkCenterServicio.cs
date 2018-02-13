using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios
{
    public class WorkCenterServicio
    {
        private PMMXContext _context;

        public WorkCenterServicio(PMMXContext context)
        {
            _context = context;
        }

        public WorkCenterServicio()
        {
            _context = new PMMXContext();
        }

        #region Gets

        public RespuestaServicio<IQueryable<WorkCenter>> GetWorkCenters()
        {
            RespuestaServicio<IQueryable<WorkCenter>> respuesta = new RespuestaServicio<IQueryable<WorkCenter>>();
            respuesta.Respuesta = _context.WorkCenters;
            return respuesta;
        }

        public RespuestaServicio<WorkCenterView> GetWorkCenter(int id)
        {
            RespuestaServicio<WorkCenterView> respuesta = new RespuestaServicio<WorkCenterView>();
            WorkCenterView workCenter = _context.WorkCenters.Where(x => x.Id == id).Select(x => new WorkCenterView
            {
                Id = x.Id,
                Nombre = x.Nombre,
                NombreCorto = x.NombreCorto,
                Activo = x.Activo,
                IdBussinesUnit = x.IdBussinesUnit,
                ParosActivos = x.Origenes.Select(w => w.Paros.Where(p => p.Activo == true)).Count(),
                DefectosActivos = x.Origenes.Select(w => w.Defectos.Where(p => p.Activo == true)).Count(),
                BussinesUnit = new BussinesUnitView
                {
                    Id = x.BussinesUnit.Id,
                    Nombre = x.BussinesUnit.Nombre,
                    NombreCorto = x.BussinesUnit.NombreCorto,
                    Activo = x.BussinesUnit.Activo,
                    IdResponsable = x.BussinesUnit.IdResponsable,
                    Responsable = new PersonaView
                    {
                        Id = x.BussinesUnit.Responsable.Id,
                        Nombre = x.BussinesUnit.Responsable.Nombre,
                        Apellido1 = x.BussinesUnit.Responsable.Apellido1,
                        Apellido2 = x.BussinesUnit.Responsable.Apellido2
                    }

                },
                Modulos = x.Origenes.Where(o => (o.IdModulo > 0 && o.IdModulo != null)).Select(o => new OrigenView
                {
                    Id = o.Id,
                    IdModulo = o.IdModulo,
                    IdWorkCenter = o.IdWorkCenter,
                    Orden = o.Orden,
                    DefectosActivos = o.Defectos.Where(d => d.Activo == true).Count(),
                    ParosActivos = o.Paros.Where(p => p.Activo == true).Count(),
                    Modulo = new ModuloView
                    {
                        Id = o.Modulo.Id,
                        Nombre = o.Modulo.Nombre,
                        NombreCorto = o.Modulo.NombreCorto,
                        Seccion = new ModuloSeccionView
                        {

                        }

                    }
                }).ToList()
            }).FirstOrDefault();
            if (workCenter != null)
            {
                respuesta.Respuesta = workCenter;
            }
            else
            {
                respuesta.Mensaje = "LU inexistente";
            }

            return respuesta;
        }

        public RespuestaServicio<List<WorkCenterView>> GetWorkCentersByPersona(int idPersona)
        {
            RespuestaServicio<List<WorkCenterView>> respuesta = new RespuestaServicio<List<WorkCenterView>>();

            List<WorkCenterView> workCenters = _context.WorkCenters
                .Where(b => b.Operadores.Any(u => u.Id == idPersona))
                .Select(x => new WorkCenterView
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    NombreCorto = x.NombreCorto,
                    Activo = x.Activo,
                    IdBussinesUnit = x.IdBussinesUnit,
                    BussinesUnit = new BussinesUnitView
                    {
                        Id = x.BussinesUnit.Id,
                        Nombre = x.BussinesUnit.Nombre,
                        NombreCorto = x.BussinesUnit.NombreCorto,
                        Activo = x.BussinesUnit.Activo,
                        IdResponsable = x.BussinesUnit.IdResponsable,
                        Responsable = new PersonaView
                        {
                            Id = x.BussinesUnit.Responsable.Id,
                            Nombre = x.BussinesUnit.Responsable.Nombre,
                            Apellido1 = x.BussinesUnit.Responsable.Apellido1,
                            Apellido2 = x.BussinesUnit.Responsable.Apellido2
                        }

                    },
                    Modulos = x.Origenes.Where(o => (o.IdModulo > 0 && o.IdModulo != null)).Select(o => new OrigenView
                    {
                        Id = o.Id,
                        IdModulo = o.IdModulo,
                        IdWorkCenter = o.IdWorkCenter,
                        Orden = o.Orden,
                        DefectosActivos = o.Defectos.Where(d => d.Activo == true).Count(),
                        ParosActivos = o.Paros.Where(p => p.Activo == true).Count(),
                        Modulo = new ModuloView
                        {
                            Id = o.Modulo.Id,
                            Nombre = o.Modulo.Nombre,
                            NombreCorto = o.Modulo.NombreCorto,
                            Seccion = new ModuloSeccionView
                            {

                            }

                        }
                    }).ToList()
                }
            ).ToList();
            if (workCenters != null)
            {
                respuesta.Respuesta = workCenters;
            }
            else
            {
                respuesta.Mensaje = "LUs inexistentes";
            }

            return respuesta;
        }

        public RespuestaServicio<List<WorkCenterView>> GetWorkCentersByShiftLeaders(int idShiftLeaders)
        {
            RespuestaServicio<List<WorkCenterView>> respuesta = new RespuestaServicio<List<WorkCenterView>>();

            List<WorkCenterView> workCenter = _context.ShiftLeaders
                .Where(y => y.IdShiftLeader == idShiftLeaders)
                .SelectMany(s => s.BussinesUnit.WorkCenters.Select(x => new WorkCenterView
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    NombreCorto = x.NombreCorto,
                    Activo = x.Activo,
                    IdBussinesUnit = x.IdBussinesUnit,
                    ParosActivos = x.Origenes.Select(w => w.Paros.Where(p => p.Activo == true)).Count(),
                    DefectosActivos = x.Origenes.Select(w => w.Defectos.Where(p => p.Activo == true)).Count(),
                    IdResponsable = x.IdResponsable,
                    Responsable = new PersonaView
                    {
                        Id = x.Responsable.Id,
                        IdPuesto = x.Responsable.IdPuesto,
                        Nombre = x.Responsable.Nombre,
                        Apellido1 = x.Responsable.Apellido1,
                        Apellido2 = x.Responsable.Apellido2,
                        Activo = x.Responsable.Activo,

                    },
                    Modulos = x.Origenes.Where(o => (o.IdModulo > 0 && o.IdModulo != null)).Select(o => new OrigenView
                    {
                        Id = o.Id,
                        IdModulo = o.IdModulo,
                        IdWorkCenter = o.IdWorkCenter,
                        Orden = o.Orden,
                        DefectosActivos = o.Defectos.Where(d => d.Activo == true).Count(),
                        ParosActivos = o.Paros.Where(p => p.Activo == true).Count(),
                        Modulo = new ModuloView
                        {
                            Id = o.Modulo.Id,
                            Nombre = o.Modulo.Nombre,
                            NombreCorto = o.Modulo.NombreCorto

                        }
                    }).ToList()


                })).ToList();





            if (workCenter != null)
            {
                respuesta.Respuesta = workCenter;
            }
            else
            {
                respuesta.Mensaje = "LU inexistentes";
            }

            return respuesta;
        }

        public RespuestaServicio<List<WorkCenter>> GetWorkCentersByLineLeader(int idLineLeader)
        {
            RespuestaServicio<List<WorkCenter>> respuesta = new RespuestaServicio<List<WorkCenter>>();
            List<WorkCenter> workCenter = _context.WorkCenters.Where(y => y.IdResponsable == idLineLeader).ToList();

            if (workCenter != null && workCenter.Count() > 0)
            {
                respuesta.Respuesta = workCenter;
            }
            else
            {
                respuesta.Mensaje = "WorkCenters inexistentes";
            }

            return respuesta;
        }

        public RespuestaServicio<WorkCenterView> GetWorkCenterByOperador(int idOperador)
        {
            RespuestaServicio<WorkCenterView> respuesta = new RespuestaServicio<WorkCenterView>();

            WorkCenterView workCenter = _context.Operadores
                .Where(y => y.IdOperador == idOperador)
                .Select(x => new WorkCenterView
                {
                    Id = x.WorkCenter.Id,
                    Nombre = x.WorkCenter.Nombre,
                    NombreCorto = x.WorkCenter.NombreCorto,
                    Activo = x.WorkCenter.Activo,
                    IdBussinesUnit = x.WorkCenter.IdBussinesUnit,
                    ParosActivos = x.WorkCenter.Origenes.Select(w => w.Paros.Where(p => p.Activo == true)).Count(),
                    DefectosActivos = x.WorkCenter.Origenes.Select(w => w.Defectos.Where(p => p.Activo == true)).Count(),
                    IdResponsable = x.WorkCenter.IdResponsable,
                    Responsable = new PersonaView
                    {
                        Id = x.WorkCenter.Responsable.Id,
                        IdPuesto = x.WorkCenter.Responsable.IdPuesto,
                        Nombre = x.WorkCenter.Responsable.Nombre,
                        Apellido1 = x.WorkCenter.Responsable.Apellido1,
                        Apellido2 = x.WorkCenter.Responsable.Apellido2,
                        Activo = x.WorkCenter.Responsable.Activo,

                    },
                    Modulos = x.WorkCenter.Origenes.Where(o => (o.IdModulo > 0 && o.IdModulo != null)).Select(o => new OrigenView
                    {
                        Id = o.Id,
                        IdModulo = o.IdModulo,
                        IdWorkCenter = o.IdWorkCenter,
                        Orden = o.Orden,
                        DefectosActivos = o.Defectos.Where(d => d.Activo == true).Count(),
                        ParosActivos = o.Paros.Where(p => p.Activo == true).Count(),
                        Modulo = new ModuloView
                        {
                            Id = o.Modulo.Id,
                            Nombre = o.Modulo.Nombre,
                            NombreCorto = o.Modulo.NombreCorto

                        }
                    }).ToList()
                }).FirstOrDefault();

            if (workCenter != null)
            {
                respuesta.Respuesta = workCenter;
            }
            else
            {
                respuesta.Mensaje = "LU inexistentes";
            }

            return respuesta;
        }

        public RespuestaServicio<List<WorkCenterView>> GetWorkCentersByBussinesUnit(int idBussinesUnit)
        {
            RespuestaServicio<List<WorkCenterView>> respuesta = new RespuestaServicio<List<WorkCenterView>>();

            List<WorkCenterView> workCenters = _context.WorkCenters.Select(x => new WorkCenterView
            {
                Id = x.Id,
                Nombre = x.Nombre,
                NombreCorto = x.NombreCorto,
                Activo = x.Activo,
                IdBussinesUnit = x.IdBussinesUnit,
                BussinesUnit = new BussinesUnitView
                {
                    Id = x.BussinesUnit.Id,
                    Nombre = x.BussinesUnit.Nombre,
                    NombreCorto = x.BussinesUnit.NombreCorto,
                    Activo = x.BussinesUnit.Activo,
                    IdResponsable = x.BussinesUnit.IdResponsable,
                    Responsable = new PersonaView
                    {
                        Id = x.BussinesUnit.Responsable.Id,
                        Nombre = x.BussinesUnit.Responsable.Nombre,
                        Apellido1 = x.BussinesUnit.Responsable.Apellido1,
                        Apellido2 = x.BussinesUnit.Responsable.Apellido2
                    }

                },
                Modulos = _context.Origens.Where(o => o.IdWorkCenter == x.Id && (o.IdModulo > 0 && o.IdModulo != null)).Select(o => new OrigenView
                {
                    Id = o.Id,
                    IdModulo = o.IdModulo,
                    IdWorkCenter = o.IdWorkCenter,
                    Orden = o.Orden,
                    DefectosActivos = _context.Defectos.Where(d => (d.Origen.Modulo.Id == o.IdModulo && d.Activo == true && d.Origen.IdWorkCenter == x.Id)).Count(),
                    ParosActivos = _context.Paros.Where(p => (p.Origen.Modulo.Id == o.IdModulo && p.Activo == true && p.Origen.IdWorkCenter == x.Id)).Count(),
                    Modulo = new ModuloView
                    {
                        Id = o.Modulo.Id,
                        Nombre = o.Modulo.Nombre,
                        NombreCorto = o.Modulo.NombreCorto,
                        Activo = o.Modulo.Activo,
                        Seccion = new ModuloSeccionView
                        {
                            Id = o.Modulo.Seccion.Id,
                            Nombre = o.Modulo.Seccion.Nombre
                        }

                    }
                }).ToList()
            }).Where(w => w.IdBussinesUnit == idBussinesUnit).ToList();
            if (workCenters != null)
            {
                respuesta.Respuesta = workCenters;
            }
            else
            {
                respuesta.Mensaje = "LUs inexistentes";
            }

            return respuesta;
        }

        #endregion

        #region Puts

        public RespuestaServicio<WorkCenterView> ActualizarWorkCenter(int id, WorkCenter workCenter)
        {
            RespuestaServicio<WorkCenterView> respuesta = new RespuestaServicio<WorkCenterView>();

            _context.Entry(workCenter).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                respuesta.Mensaje = ex.ToString();
                return respuesta;

            }

            respuesta = GetWorkCenter(id);

            return respuesta;
        }

        #endregion

        #region Posts

        public RespuestaServicio<WorkCenterView> PostWorkCenter(WorkCenter workCenter)
        {
            RespuestaServicio<WorkCenterView> respuesta = new RespuestaServicio<WorkCenterView>();

            _context.WorkCenters.Add(workCenter);
            _context.SaveChanges();
            respuesta = GetWorkCenter(workCenter.Id);
            return respuesta;
        }

        #endregion

        #region Deletes
        public RespuestaServicio<bool> DeleteWorkCenter(int id)
        {
            RespuestaServicio<bool> respuesta = new RespuestaServicio<bool>();

            WorkCenter workCenter = _context.WorkCenters.Find(id);
            if (workCenter == null)
            {
                respuesta.Mensaje = "LU no encontrada, no se pudo Eliminar";
            }
            else
            {
                _context.WorkCenters.Remove(workCenter);
                _context.SaveChanges();
                respuesta.Respuesta = true;
            }

            return respuesta;
        }
        #endregion

        #region Helpers

        public void Dispose()
        {
            _context.Dispose();
        }

        private bool WorkCenterExists(int id)
        {
            return _context.WorkCenters.Count(e => e.Id == id) > 0;
        }

        #endregion

    }
}
