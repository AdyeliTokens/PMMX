using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class FeedView
    {
        public DateTime Fecha { get; set; }
        public ActividadEnParo actividadEnParo { get; set; }
        public ActividadEnDefecto actividadEnDefecto { get; set; }

    }
}
