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
    public class GembaWalkServicio
    {
        #region Contexto

        private PMMXContext db = new PMMXContext();

        #endregion

        #region Gets

        public RespuestaServicio<IQueryable<GembaWalkView>> GetGembaWalk()
        {
            RespuestaServicio<IQueryable<GembaWalkView>> respuesta = new RespuestaServicio<IQueryable<GembaWalkView>>();
            respuesta.Respuesta = db.GembaWalk.Select(d => new GembaWalkView
            {
                Id = d.Id,
                IdOrigen = d.IdOrigen,
                IdReporta = d.IdReporta,
                Activo = d.Activo,
                Prioridad = d.Prioridad,
                FechaReporte = d.FechaReporte,
                Descripcion = d.Descripcion,
                IdSubCategoria = d.IdSubCategoria,
                Tipo = d.Tipo,
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
                },
                SubCategoria = new SubCategoriaView
                {
                    Id = d.SubCategoria.Id,
                    Nombre = d.SubCategoria.Nombre,
                    NombreCorto = d.SubCategoria.NombreCorto,
                    IdResponsable = d.SubCategoria.IdResponsable,
                    Activo = d.SubCategoria.Activo
                }
                });
            return respuesta;
        }

        public RespuestaServicio<GembaWalkView> GetGembaWalk(int id)
        {
            RespuestaServicio<GembaWalkView> respuesta = new RespuestaServicio<GembaWalkView>();

            GembaWalkView GembaWalk = db.GembaWalk
                .Where(d => (d.Id == id))
                .Select(d => new GembaWalkView
                {
                    Id = d.Id,
                    IdOrigen = d.IdOrigen,
                    IdReporta = d.IdReporta,
                    Descripcion = d.Descripcion,
                    Activo = d.Activo,
                    Prioridad = d.Prioridad,
                    FechaReporte = d.FechaReporte,
                    IdSubCategoria = d.IdSubCategoria,
                    Tipo = d.Tipo,
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
                    SubCategoria = new SubCategoriaView
                    {
                        Id = d.SubCategoria.Id,
                        Nombre = d.SubCategoria.Nombre,
                        NombreCorto = d.SubCategoria.NombreCorto,
                        IdResponsable = d.SubCategoria.IdResponsable,
                        Activo = d.SubCategoria.Activo
                    }                    
                }).FirstOrDefault();
            if (GembaWalk != null)
            {
                respuesta.Respuesta = GembaWalk;
            }
            else
            {
                respuesta.Mensaje = "GembaWalk inexistente";
            }

            return respuesta;
        }

        #endregion

        #region Puts

        public RespuestaServicio<GembaWalkView> PutGembaWalk(int id, string NotificacionSAP)
        {
            RespuestaServicio<GembaWalkView> respuesta = new RespuestaServicio<GembaWalkView>();

            GembaWalk GembaWalk = db.GembaWalk.Find(id);

            if (GembaWalk == null)
            {
                return respuesta;
            }
            
            db.Entry(GembaWalk).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                respuesta.Mensaje = ex.ToString();
                return respuesta;

            }

            respuesta = GetGembaWalk(GembaWalk.Id);

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

        private bool GembaWalkExists(int id)
        {
            return db.GembaWalk.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
