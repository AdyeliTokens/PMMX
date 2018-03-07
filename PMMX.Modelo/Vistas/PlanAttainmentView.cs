using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class PlanAttainmentView
    {
        public Double Plan_Attainment_Total { get; set; }
        public Double Objetivo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
    }
}
