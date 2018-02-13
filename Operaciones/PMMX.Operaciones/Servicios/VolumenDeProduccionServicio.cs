using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios
{
    public class VolumenDeProduccionServicio
    {
        private PMMXContext _context;

        public VolumenDeProduccionServicio(PMMXContext context)
        {
            _context = context;
        }

        public VolumenDeProduccionServicio()
        {
            _context = new PMMXContext();
        }

        #region GET
        #endregion

        #region PUT
        public RespuestaServicio<VolumenDeProduccion> PostVolumenDeProduccion(VolumenDeProduccion volumenDeProduccion)
        {
            RespuestaServicio<VolumenDeProduccion> respuesta = new RespuestaServicio<VolumenDeProduccion>();
            try
            {
                _context.VolumenesDeProduccion.Add(volumenDeProduccion);
                _context.SaveChanges();
                respuesta.Respuesta = volumenDeProduccion;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = e.Message;
            }
            
            
            return respuesta;
        }
        #endregion

        #region POST
        #endregion

        #region DELETE
        #endregion

    }
}
