using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class VQIView
    {

        public int VQI_Total { get; set; }

        public int Objetivo { get; set; }

        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
    }
}
