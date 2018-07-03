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
using PMMX.Modelo.Entidades;
using PMMX.Seguridad.Servicios;
using PMMX.Operaciones.Servicios;

namespace PMMX.Operaciones.Servicios
{
    public class WorkFlowServicio
    {
        #region Contexto

        private PMMXContext db = new PMMXContext();

        #endregion

        #region Gets

        public RespuestaServicio<IQueryable<WorkFlowView>> GetWorkFlow()
        {
            RespuestaServicio<IQueryable<WorkFlowView>> respuesta = new RespuestaServicio<IQueryable<WorkFlowView>>();
            respuesta.Respuesta = db.WorkFlows
            .Select(d => new WorkFlowView
            {
                Id = d.Id,
                IdSubCategoria = d.IdSubCategoria,
                Inicial = d.Inicial,
                Anterior = d.Anterior,
                Siguiente = d.Siguiente,
                Cancelado = d.Cancelado,
                Activo = d.Activo,
                EstatusInicial = new EstatusView
                {
                    Id = d.EstatusInicial.Id,
                    Nombre = d.EstatusInicial.Nombre,
                    IdCategoria = d.EstatusInicial.IdCategoria,
                    Activo = d.EstatusInicial.Activo
                },
                EstatusSiguiente = new EstatusView
                {
                    Id = d.EstatusSiguiente.Id,
                    Nombre = d.EstatusSiguiente.Nombre,
                    IdCategoria = d.EstatusSiguiente.IdCategoria,
                    Activo = d.EstatusSiguiente.Activo
                },
                EstatusAnterior = new EstatusView
                {
                    Id = d.EstatusAnterior.Id,
                    Nombre = d.EstatusAnterior.Nombre,
                    IdCategoria = d.EstatusAnterior.IdCategoria,
                    Activo = d.EstatusAnterior.Activo
                }
            });
            return respuesta;
        }

        public RespuestaServicio<WorkFlowView> GetWorkFlow(int id)
        {
            RespuestaServicio<WorkFlowView> respuesta = new RespuestaServicio<WorkFlowView>();

            var WorkFlow = db.WorkFlows
                .Where(d => (d.Id == id))
                .Select(d => new WorkFlowView
                {
                    Id = d.Id,
                    IdSubCategoria = d.IdSubCategoria,
                    Inicial = d.Inicial,
                    Anterior = d.Anterior,
                    Siguiente = d.Siguiente,
                    Cancelado = d.Cancelado,
                    Activo = d.Activo,
                    EstatusInicial = new EstatusView
                    {
                        Id = d.EstatusInicial.Id,
                        Nombre = d.EstatusInicial.Nombre,
                        IdCategoria = d.EstatusInicial.IdCategoria,
                        Activo = d.EstatusInicial.Activo
                    },
                    EstatusSiguiente = new EstatusView
                    {
                        Id = d.EstatusSiguiente.Id,
                        Nombre = d.EstatusSiguiente.Nombre,
                        IdCategoria = d.EstatusSiguiente.IdCategoria,
                        Activo = d.EstatusSiguiente.Activo
                    },
                    EstatusAnterior = new EstatusView
                    {
                        Id = d.EstatusAnterior.Id,
                        Nombre = d.EstatusAnterior.Nombre,
                        IdCategoria = d.EstatusAnterior.IdCategoria,
                        Activo = d.EstatusAnterior.Activo
                    }
                }).FirstOrDefault();

            if (WorkFlow != null)
            {
                respuesta.Respuesta = WorkFlow;
            }
            else
            {
                respuesta.Mensaje = "WorkFlow inexistente";
            }

            return respuesta;
        }
        
        public RespuestaServicio<WorkFlowView> nextEstatus(int IdSubcategoria, int IdEstatusActual, bool Cancelado)
        {
            RespuestaServicio<WorkFlowView> respuesta = new RespuestaServicio<WorkFlowView>();

            Estatus estatus = db.Estatus.Find(IdEstatusActual);
            var _next = new WorkFlowView();

            if (estatus == null)
            {
                _next = db.WorkFlows
                      .Where(w => (w.IdSubCategoria == IdSubcategoria))
                      .OrderBy(w => w.Id)
                      .Select(w => new WorkFlowView
                      {
                          Id = w.Id,
                          IdSubCategoria = w.IdSubCategoria,
                          Inicial = w.Inicial,
                          Siguiente = w.Siguiente,
                          Anterior = w.Anterior,
                          Cancelado = w.Cancelado,
                          EstatusInicial = new EstatusView
                          {
                              Id = w.EstatusInicial.Id,
                              Nombre = w.EstatusInicial.Nombre
                          },
                          EstatusAnterior = new EstatusView
                          {
                              Id = w.EstatusAnterior.Id,
                              Nombre = w.EstatusAnterior.Nombre
                          },
                          EstatusSiguiente = new EstatusView
                          {
                              Id = w.EstatusSiguiente.Id,
                              Nombre = w.EstatusSiguiente.Nombre
                          }
                      })
                      .FirstOrDefault();                
            }
            else
            {
                _next = db.WorkFlows
                    .Where(w => (w.IdSubCategoria == IdSubcategoria) && (w.EstatusInicial.Id == IdEstatusActual) && (w.Cancelado == Cancelado))
                    .Select(w => new WorkFlowView {
                        Id = w.Id,
                        IdSubCategoria = w.IdSubCategoria,
                        Inicial = w.Inicial,
                        Siguiente = w.Siguiente,
                        Anterior = w.Anterior,
                        Cancelado = w.Cancelado,
                        EstatusInicial = new EstatusView
                        {
                            Id = w.EstatusInicial.Id,
                            Nombre = w.EstatusInicial.Nombre
                        },
                        EstatusAnterior = new EstatusView
                        {
                            Id = w.EstatusAnterior.Id,
                            Nombre = w.EstatusAnterior.Nombre
                        },
                        EstatusSiguiente = new EstatusView
                        {
                            Id = w.EstatusSiguiente.Id,
                            Nombre = w.EstatusSiguiente.Nombre
                        }
                    })
                    .FirstOrDefault();
            }
            
            if (_next != null)
            {
                respuesta.Respuesta = _next;
            }
            else
            {
                respuesta.Mensaje = "WorkFlow inexistente";
            }

            return respuesta;
        }

        #endregion

        #region Puts

        public RespuestaServicio<WorkFlowView> PutWorkFlow(int id, string NotificacionSAP)
        {
            RespuestaServicio<WorkFlowView> respuesta = new RespuestaServicio<WorkFlowView>();

            WorkFlow workFlow = db.WorkFlows.Find(id);

            if (workFlow == null)
            {
                return respuesta;
            }

            db.Entry(workFlow).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                respuesta.Mensaje = ex.ToString();
                return respuesta;

            }

            respuesta = GetWorkFlow(workFlow.Id);

            return respuesta;
        }

        #endregion

        #region Posts
        #endregion

        #region Deletes

        public RespuestaServicio<bool> DeleteWorkFlow(int id)
        {
            RespuestaServicio<bool> respuesta = new RespuestaServicio<bool>();

            WorkFlow WorkFlow = db.WorkFlows.Find(id);
            if (WorkFlow == null)
            {
                respuesta.Mensaje = "WorkFlow no encontrado, no se pudo Eliminar";
            }
            else
            {
                db.WorkFlows.Remove(WorkFlow);
                db.SaveChanges();
                respuesta.Respuesta = true;
            }

            return respuesta;
        }

        #endregion

        #region Helpers

        public void Dispose()
        {
            db.Dispose();
        }

        private bool WorkFlowExists(int id)
        {
            return db.WorkFlows.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
