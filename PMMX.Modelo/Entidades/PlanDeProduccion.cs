using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class PlanDeProduccion
    {
        
        public String Codigo { get; set; }

        [Required]
        public int IdWorkCenter { get; set; }

        [Required]
        public String Code_FA { get; set; }

        [Required]
        public Double Cantidad { get; set; }
        public String ClaseDeOrden { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Inicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fin { get; set; }

        [Required]
        public DateTime FechaSubida { get; set; }

        [Required]
        public int IdUploader { get; set; }

        [Required]
        public Boolean Activo { get; set; }

        public Marca Marca_FA { get; set; }
        public Persona Uploader { get; set; }
        public WorkCenter WorkCenterEfectivo { get; set; }



    }
}
