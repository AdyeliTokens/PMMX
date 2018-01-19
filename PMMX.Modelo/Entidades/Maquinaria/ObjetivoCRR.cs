using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class ObjetivoCRR
    {
        public int Id { get; set; }
        public int IdWorkCenter { get; set; }
        public Double Objetivo { get; set; }
        public DateTime FechaInicial { get; set; }


        public WorkCenter WorkCenter { get; set; }
    }
}
