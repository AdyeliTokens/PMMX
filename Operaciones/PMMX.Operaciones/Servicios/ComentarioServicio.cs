using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios
{
    public class ComentarioServicio
    {
        #region Contexto
            private PMMXContext db = new PMMXContext();
        #endregion


        #region Gets
        public RespuestaServicio<IQueryable<ComentarioView>> GetComentarios()
        {
            RespuestaServicio<IQueryable<ComentarioView>> respuesta = new RespuestaServicio<IQueryable<ComentarioView>>();
            respuesta.Respuesta = db.Comentarios.Select(d => new ComentarioView
            {
                Id = d.Id,
                IdComentador = d.IdComentador,
                IdDefecto = d.IdDefecto,
                Opinion = d.Opinion,
                Fecha = d.Fecha,
                Comentador = new PersonaView
                {
                    Id = d.Comentador.Id,
                    Nombre = d.Comentador.Nombre,
                    Apellido1 = d.Comentador.Apellido1,
                    Apellido2 = d.Comentador.Apellido2
                },
                Defecto = new DefectoView
                {
                    Id = d.Defecto.Id,
                    Descripcion = d.Defecto.Descripcion,
                    FechaEstimada = d.Defecto.FechaEstimada,
                    FechaReporte = d.Defecto.FechaReporte
                }
            });
            return respuesta;
        }

        public RespuestaServicio<ComentarioView> GetComentario(int id)
        {
            RespuestaServicio<ComentarioView> respuesta = new RespuestaServicio<ComentarioView>();

            ComentarioView comentario = db.Comentarios
                .Where(d => (d.Id == id))
                .Select(d => new ComentarioView
                {
                    Id = d.Id,
                    IdComentador = d.IdComentador,
                    IdDefecto = d.IdDefecto,
                    Opinion = d.Opinion,
                    Fecha = d.Fecha,
                    Comentador = new PersonaView
                    {
                        Id = d.Comentador.Id,
                        Nombre = d.Comentador.Nombre,
                        Apellido1 = d.Comentador.Apellido1,
                        Apellido2 = d.Comentador.Apellido2
                    },
                    Defecto = new DefectoView
                    {
                        Id = d.Defecto.Id,
                        Descripcion = d.Defecto.Descripcion,
                        FechaEstimada = d.Defecto.FechaEstimada,
                        FechaReporte = d.Defecto.FechaReporte
                    }
                }).FirstOrDefault(); 

            if ( comentario != null)
            {
                respuesta.Respuesta = comentario;
            }
            else
            {
                respuesta.Mensaje = "Comentario inexistente";
            }

            return respuesta;
        }

        #endregion

        #region Puts
        #endregion

        #region Posts
        public RespuestaServicio<ComentarioView> PostComentario(Comentario comentario)
        {
            RespuestaServicio<ComentarioView> respuesta = new RespuestaServicio<ComentarioView>();
            comentario.Fecha = DateTime.Now;
            db.Comentarios.Add(comentario);
            db.SaveChanges();
            respuesta = GetComentario(comentario.Id);
            return respuesta;
        }
        #endregion

        #region Deletes
        #endregion

        #region Helpers
        public void Dispose()
        {
            db.Dispose();
        }

        private bool ComentarioExists(int id)
        {
            return db.Comentarios.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
