using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using PMMX.Infraestructura.Helpers;
using System.Collections.Generic;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Maquinaria;

namespace PMMX.Operaciones.Servicios
{
    public class DefectoServicio
    {
        private PMMXContext _context;

        public DefectoServicio(PMMXContext context)
        {
            _context = context;
        }

        public DefectoServicio()
        {
            _context = new PMMXContext();
        }

        #region Gets

        public RespuestaServicio<IList<DefectoView>> GetDefectos()
        {
            RespuestaServicio<IList<DefectoView>> respuesta = new RespuestaServicio<IList<DefectoView>>();
            respuesta.Respuesta = _context.Defectos.Select(d => new DefectoView
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

            }).ToList();
            if (respuesta.EjecucionCorrecta == true)
            {
                foreach (DefectoView df in respuesta.Respuesta)
                {
                    if (df.IdResponsable > 0)
                    {
                        df.Responsable = _context.Personas.Where(d => d.Id == df.IdResponsable).Select(d => new PersonaView
                        {
                            Id = d.Id,
                            Nombre = d.Nombre,
                            Apellido1 = d.Apellido1,
                            Apellido2 = d.Apellido2

                        }).FirstOrDefault();
                    }
                }
            }
            return respuesta;
        }

        public RespuestaServicio<DefectoView> GetDefecto(int id)
        {
            RespuestaServicio<DefectoView> respuesta = new RespuestaServicio<DefectoView>();

            DefectoView defecto = _context.Defectos
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
                    }
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

            Defecto defecto = _context.Defectos.Find(id);

            if (defecto == null)
            {
                return respuesta;
            }

            defecto.NotificacionSAP = NotificacionSAP;
            _context.Entry(defecto).State = EntityState.Modified;

            try
            {

                _context.SaveChanges();
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
        public RespuestaServicio<DefectoView> PostDefecto(Defecto defecto)
        {
            RespuestaServicio<DefectoView> respuesta = new RespuestaServicio<DefectoView>();
            if (defecto == null)
            {
                respuesta.Mensaje = "El defecto no puede ser null al momento de guardar";
                return respuesta;
            }
            try
            {
                _context.Defectos.Add(defecto);
                _context.SaveChanges();

                var respuesta_defecto = GetDefecto(defecto.Id);
                if (respuesta_defecto.EjecucionCorrecta)
                {
                    respuesta.Respuesta = respuesta_defecto.Respuesta;
                }
                else {
                    respuesta.Mensaje = respuesta_defecto.Mensaje;
                    return respuesta;
                }
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                respuesta.Mensaje = ex.ToString();
                return respuesta;

            }
            return respuesta;
        }



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
            _context.Dispose();
        }

        private bool DefectoExists(int id)
        {
            return _context.Defectos.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
