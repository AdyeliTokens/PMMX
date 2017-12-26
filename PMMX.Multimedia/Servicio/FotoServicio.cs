using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Multimedia.Servicio
{
    public class FotoServicio
    {
        private PMMXContext _context;

        public FotoServicio(PMMXContext context)
        {
            _context = context;
        }

        public FotoServicio()
        {
            _context = new PMMXContext();
        }

        public IRespuestaServicio<string> SubirFoto() {
            IRespuestaServicio<string> respuesta = new RespuestaServicio<string>();

            return respuesta;
        } 

    }
}
