using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Paros;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios
{
    public class ActividadEnParoServicio
    {
        private PMMXContext _context;


        public ActividadEnParoServicio()
        {
            _context = new PMMXContext();
        }

        public ActividadEnParoServicio(PMMXContext _context)
        {
            this._context = _context;
        }


        #region Get

        public RespuestaServicio<IQueryable<ActividadEnParo>> GetActividadesEnParo()
        {
            RespuestaServicio<IQueryable<ActividadEnParo>> respuesta = new RespuestaServicio<IQueryable<ActividadEnParo>>();

            try
            {
                var query = _context.ActividadEnParos;
                respuesta.Respuesta = query;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }

        public RespuestaServicio<ActividadEnParo> GetActividadEnParo(int id)
        {
            RespuestaServicio<ActividadEnParo> respuesta = new RespuestaServicio<ActividadEnParo>();
            ActividadEnParo actividadEnParo = _context.ActividadEnParos.Find(id);

            respuesta.Respuesta = actividadEnParo;

            return respuesta;
        }

        public bool ActividadEnParoExists(int id)
        {
            return _context.ActividadEnParos.Count(e => e.Id == id) > 0;
        }

        #endregion

        #region Put

        public RespuestaServicio<ActividadEnParo> PutActividadEnParo(int id, ActividadEnParo actividadEnParo)
        {
            RespuestaServicio<ActividadEnParo> respuesta = new RespuestaServicio<ActividadEnParo>();
            if (id != actividadEnParo.Id)
            {
                respuesta.Mensaje = "El identificador mandado no coincide con el de la Actividad en paro.";
            }

            _context.Entry(actividadEnParo).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                respuesta.Respuesta = actividadEnParo;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadEnParoExists(id))
                {
                    respuesta.Mensaje = "La Actividad en el paro no Existe";
                }
            }

            return respuesta;
        }
        #endregion

        #region Post
        public RespuestaServicio<ActividadEnParo> PostActividadEnParo(ActividadEnParo actividadEnParo)
        {
            RespuestaServicio<ActividadEnParo> respuesta = new RespuestaServicio<ActividadEnParo>();
            try
            {
                _context.ActividadEnParos.Add(actividadEnParo);
                _context.SaveChanges();

                respuesta.Respuesta = actividadEnParo;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = e.Message;
            }


            return respuesta;
        }
        #endregion

        #region Delete
        public RespuestaServicio<ActividadEnParo> DeleteActividadEnParo(int id)
        {
            RespuestaServicio<ActividadEnParo> respuesta = new RespuestaServicio<ActividadEnParo>();
            ActividadEnParo actividadEnParo = _context.ActividadEnParos.Find(id);
            if (actividadEnParo == null)
            {
                respuesta.Mensaje = "La Actividad No puede ser Borrada porque no existe";
            }
            try
            {
                _context.ActividadEnParos.Remove(actividadEnParo);
                _context.SaveChanges();
                respuesta.Respuesta = actividadEnParo;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.Message;
            }


            return respuesta;
        }
        #endregion

    }
}
