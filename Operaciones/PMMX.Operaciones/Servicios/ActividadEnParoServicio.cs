using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Paros;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Collections.Generic;
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
    }
}
