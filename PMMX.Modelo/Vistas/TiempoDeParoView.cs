using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class TiempoDeParoView
    {
        public int Id { get; set; }
        public int IdParo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }



        public ParoView Paro { get; set; }
    }
}
