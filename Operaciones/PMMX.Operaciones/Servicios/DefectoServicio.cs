using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Defectos;
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
    public class DefectoServicio
    {
        #region Contexto

        private PMMXContext db = new PMMXContext();

        #endregion

        #region Gets

        public RespuestaServicio<IQueryable<DefectoView>> GetDefectos()
        {
            RespuestaServicio<IQueryable<DefectoView>> respuesta = new RespuestaServicio<IQueryable<DefectoView>>();
            respuesta.Respuesta = db.Defectos.Select(d => new DefectoView
            {
                Id = d.Id,
                IdOrigen = d.IdOrigen,
                IdReportador = d.IdReportador,
                Activo = d.Activo,
                Prioridad = d.Prioridad,
                FechaReporte = d.FechaReporte,
                FechaEstimada = d.FechaEstimada,
                Descripcion = d.Descripcion,
                NotificacionSAP = d.NotificacionSAP,
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

        public RespuestaServicio<DefectoView> GetDefecto(int id)
        {
            RespuestaServicio<DefectoView> respuesta = new RespuestaServicio<DefectoView>();

            DefectoView defecto = db.Defectos
                .Where(d => (d.Id == id))
                .Select(d => new DefectoView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReportador = d.IdReportador,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    Prioridad = d.Prioridad,
                    FechaReporte = d.FechaReporte,
                    FechaEstimada = d.FechaEstimada,
                    NotificacionSAP = d.NotificacionSAP,
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
                    Comentarios = d.Comentarios.Select(f => new ComentarioView
                    {
                        Id = f.Id,
                        Fecha = f.Fecha,
                        IdComentador = f.IdComentador,
                        Opinion = f.Opinion,
                        IdDefecto = f.IdDefecto
                    }).ToList()
                }).FirstOrDefault();
            if (defecto != null)
            {
                respuesta.Respuesta = defecto;
            }
            else
            {
                respuesta.Mensaje = "Defecto inexistente";
            }

            return respuesta;
        }

        #endregion

        #region Puts

        public RespuestaServicio<DefectoView> PutDefecto(int id, string NotificacionSAP)
        {
            RespuestaServicio<DefectoView> respuesta = new RespuestaServicio<DefectoView>();

            Defecto defecto = db.Defectos.Find(id);

            if (defecto == null)
            {
               return respuesta;
            }

            defecto.NotificacionSAP = NotificacionSAP;
            db.Entry(defecto).State = EntityState.Modified;

            try
            {
                
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                respuesta.Mensaje = ex.ToString();
                return respuesta;

            }

            respuesta = GetDefecto(defecto.Id);

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

        private bool DefectoExists(int id)
        {
            return db.Defectos.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
