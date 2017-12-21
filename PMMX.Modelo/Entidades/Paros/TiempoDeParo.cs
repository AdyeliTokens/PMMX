using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Paros
{
    public class TiempoDeParo
    {
        public int Id { get; set; }
        public int IdParo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fin { get; set; }



        public Paro Paro { get; set; }
    }
}
