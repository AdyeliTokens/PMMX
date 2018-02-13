using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.InsiteLAC
{
    public class KPI
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
        public Double YTD { get; set; }
        public int Mes_Efectivo { get; set; }
        public int Anio_Efectivo { get; set; }
    }
}
