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
    public class EventoServicio
    {
        #region Contexto

        private PMMXContext db = new PMMXContext();

        #endregion

        #region Gets

        public RespuestaServicio<IQueryable<EventoView>> GetEvento()
        {
            RespuestaServicio<IQueryable<EventoView>> respuesta = new RespuestaServicio<IQueryable<EventoView>>();
            respuesta.Respuesta = db.Evento
            .Select(d => new EventoView
            {
                Id = d.Id,
                Descripcion = d.Descripcion,
                IdAsignador = d.IdAsignador,
                FechaInicio = d.FechaInicio,
                FechaFin = d.FechaFin,
                Nota = d.Nota,
                EsRecurrente = d.EsRecurrente,
                Activo = d.Activo,
                Asignador = new PersonaView
                {
                    Id = d.Asignador.Id,
                    Nombre = d.Asignador.Nombre,
                    Apellido1 = d.Asignador.Apellido1,
                    Apellido2 = d.Asignador.Apellido2,
                    IdPuesto = d.Asignador.IdPuesto
                },
                GembaWalk = d.GembaWalk.Where(j => (j.IdEvento == d.Id))
                .Select(j => new GembaWalkView
                {
                    Id = j.Id,
                    IdEvento = j.IdEvento,
                    IdOrigen = j.IdOrigen,
                    IdReportador = j.IdReportador,
                    IdResponsable = j.IdResponsable,
                    Descripcion = j.Descripcion,
                    FechaEstimada = j.FechaEstimada,
                    FechaReporte = j.FechaReporte,
                    Prioridad = j.Prioridad
                }).ToList()
            });
            return respuesta;
        }

        public RespuestaServicio<EventoView> GetEvento(int id)
        {
            RespuestaServicio<EventoView> respuesta = new RespuestaServicio<EventoView>();

            var Evento = db.Evento
                .Where(d => (d.Id == id))
                .Select(d => new EventoView
                {
                    Id = d.Id,
                    Descripcion = d.Descripcion,
                    IdAsignador = d.IdAsignador,
                    FechaInicio = d.FechaInicio,
                    FechaFin = d.FechaFin,
                    Nota = d.Nota,
                    EsRecurrente = d.EsRecurrente,
                    Activo = d.Activo,
                    Asignador = new PersonaView
                    {
                        Id = d.Asignador.Id,
                        Nombre = d.Asignador.Nombre,
                        Apellido1 = d.Asignador.Apellido1,
                        Apellido2 = d.Asignador.Apellido2,
                        IdPuesto = d.Asignador.IdPuesto
                    },
                    GembaWalk = d.GembaWalk.Where(j => (j.IdEvento == d.Id))
                    .Select(j => new GembaWalkView
                    {
                        Id = j.Id,
                        IdEvento = j.IdEvento,
                        IdOrigen = j.IdOrigen,
                        IdReportador = j.IdReportador,
                        IdResponsable = j.IdResponsable,
                        Descripcion = j.Descripcion,
                        FechaEstimada = j.FechaEstimada,
                        FechaReporte = j.FechaReporte,
                        Prioridad = j.Prioridad
                    }).ToList()
             }).FirstOrDefault();

            if (Evento != null)
            {
                respuesta.Respuesta = Evento;
            }
            else
            {
                respuesta.Mensaje = "Evento inexistente";
            }

            return respuesta;
        }

        #endregion

        #region Puts

        public RespuestaServicio<EventoView> PutEvento(int id, string NotificacionSAP)
        {
            RespuestaServicio<EventoView> respuesta = new RespuestaServicio<EventoView>();

            Evento Evento = db.Evento.Find(id);

            if (Evento == null)
            {
                return respuesta;
            }

            db.Entry(Evento).State = EntityState.Modified;

            try
            {

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                respuesta.Mensaje = ex.ToString();
                return respuesta;

            }

            respuesta = GetEvento(Evento.Id);

            return respuesta;
        }

        #endregion

        #region Posts
        #endregion

        #region Deletes

        public RespuestaServicio<bool> DeleteEvento(int id)
        {
            RespuestaServicio<bool> respuesta = new RespuestaServicio<bool>();

            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                respuesta.Mensaje = "Evento no encontrado, no se pudo Eliminar";
            }
            else
            {
                db.Evento.Remove(evento);
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

        private bool EventoExists(int id)
        {
            return db.Evento.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
