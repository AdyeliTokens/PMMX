using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Vistas
{
    public class ReporteView
    {
        public int IdWorkCenter { get; set; }
        public string NombreCorto { get; set; }
        public string Nombre { get; set; }
        public Double? DesperdiciosTotal { get; set; }
        public IEnumerable<Code_FA_View> Codes_FA { get; set; }

        
    }
}
