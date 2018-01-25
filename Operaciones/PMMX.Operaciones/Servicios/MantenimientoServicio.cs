using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios
{
    public class MantenimientoServicio
    {
        #region Contexto

        private PMMXContext db = new PMMXContext();

        #endregion

        #region Gets

        public RespuestaServicio<IQueryable<MantenimientoView>> GetMantenimiento()
        {
            RespuestaServicio<IQueryable<MantenimientoView>> respuesta = new RespuestaServicio<IQueryable<MantenimientoView>>();
            respuesta.Respuesta = db.Mantenimiento.Select(d => new MantenimientoView
            {
                Id = d.Id,
                IdOrigen = d.IdOrigen,
                IdReportador = d.IdReportador,
                Activo = d.Activo,
                Prioridad = d.Prioridad,
                FechaReporte = d.FechaReporte,
                FechaEstimada = d.FechaEstimada,
                Descripcion = d.Descripcion,
                IdResponsable = d.IdResponsable,
                Reportador = new PersonaView
                {
                    Id = d.Reportador.Id,
                    Nombre = d.Reportador.Nombre,
                    Apellido1 = d.Reportador.Apellido1,
                    Apellido2 = d.Reportador.Apellido2,
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
                        NombreCorto = d.Origen.Modulo.NombreCorto
                    },
                    WorkCenter = new WorkCenterView
                    {
                        Id = d.Origen.WorkCenter.Id,
                        Nombre = d.Origen.WorkCenter.Nombre,
                        NombreCorto = d.Origen.WorkCenter.NombreCorto,
                        Activo = d.Origen.WorkCenter.Activo,
                        IdBussinesUnit = d.Origen.WorkCenter.IdBussinesUnit,
                        BussinesUnit = new BussinesUnitView
                        {
                            Id = d.Origen.WorkCenter.BussinesUnit.Id,
                            Nombre = d.Origen.WorkCenter.BussinesUnit.Nombre,
                            NombreCorto = d.Origen.WorkCenter.BussinesUnit.NombreCorto,
                            Activo = d.Origen.WorkCenter.BussinesUnit.Activo,
                            IdResponsable = d.Origen.WorkCenter.BussinesUnit.IdResponsable
                        }
                    }
                }

            });
            return respuesta;
        }

        public RespuestaServicio<MantenimientoView> GetMantenimiento(int id)
        {
            RespuestaServicio<MantenimientoView> respuesta = new RespuestaServicio<MantenimientoView>();

            MantenimientoView Mantenimiento = db.Mantenimiento
                .Where(d => (d.Id == id))
                .Select(d => new MantenimientoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    Prioridad = d.Prioridad,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    Reportador = new PersonaView
                    {
                        Id = d.Reportador.Id,
                        Nombre = d.Reportador.Nombre,
                        Apellido1 = d.Reportador.Apellido1,
                        Apellido2 = d.Reportador.Apellido2,
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
                            Activo = d.Origen.WorkCenter.Activo,
                            IdBussinesUnit = d.Origen.WorkCenter.IdBussinesUnit,
                            BussinesUnit = new BussinesUnitView
                            {
                                Id = d.Origen.WorkCenter.BussinesUnit.Id,
                                Nombre = d.Origen.WorkCenter.BussinesUnit.Nombre,
                                NombreCorto = d.Origen.WorkCenter.BussinesUnit.NombreCorto,
                                Activo = d.Origen.WorkCenter.BussinesUnit.Activo

                            }

                        }

                    },
                    Fotos = d.Fotos.Select(f => new FotoView
                    {
                        Id = f.Id,
                        Nombre = f.Nombre,
                        Path = f.Path
                        
                    }).ToList()
                }).FirstOrDefault();
            if (Mantenimiento != null)
            {
                respuesta.Respuesta = Mantenimiento;
            }
            else
            {
                respuesta.Mensaje = "Mantenimiento inexistente";
            }

            return respuesta;
        }

        #endregion

        #region Puts

        public RespuestaServicio<MantenimientoView> PutMantenimiento(int id, string NotificacionSAP)
        {
            RespuestaServicio<MantenimientoView> respuesta = new RespuestaServicio<MantenimientoView>();

            Mantenimiento Mantenimiento = db.Mantenimiento.Find(id);

            if (Mantenimiento == null)
            {
                return respuesta;
            }

            db.Entry(Mantenimiento).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                respuesta.Mensaje = ex.ToString();
                return respuesta;

            }

            respuesta = GetMantenimiento(Mantenimiento.Id);

            return respuesta;
        }

        #endregion

        #region Posts
        #endregion

        #region Deletes

        //public RespuestaServicio<bool> DeleteWorkCenter(int id)
        //{
        //    RespuestaServicio<bool> respuesta = new RespuestaServicio<bool>();

        //    WorkCenter workCenter = db.WorkCenters.Find(id);
        //    if (workCenter == null)
        //    {
        //        respuesta.Mensaje = "LU no encontrada, no se pudo Eliminar";
        //    }
        //    else
        //    {
        //        db.WorkCenters.Remove(workCenter);
        //        db.SaveChanges();
        //        respuesta.Respuesta = true;
        //    }

        //    return respuesta;
        //}

        #endregion

        #region Helpers

        public void Dispose()
        {
            db.Dispose();
        }

        private bool MantenimientoExists(int id)
        {
            return db.Mantenimiento.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
