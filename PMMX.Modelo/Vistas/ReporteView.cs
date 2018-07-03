using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades;
using System.ComponentModel.DataAnnotations;
using PMMX.Modelo.Entidades.Maquinaria;

namespace PMMX.Modelo.Vistas
{
    public class ReporteView
    {
        public WorkCenter WorkCenter { get; set; }
        public IEnumerable<DesperdicioView> CRR { get; set; }

        
    }
}
