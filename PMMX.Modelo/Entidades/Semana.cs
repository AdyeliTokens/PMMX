using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Semana
    {
        public int numero { get; set; }
        public IList<RespuestaView> Respuestas { get; set; }
    }
}
