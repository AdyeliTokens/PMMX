using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class ObjetivoCRR
    {
        public int Id { get; set; }
        [Required]
        public int IdWorkCenter { get; set; }
        [Required]
        public Double Objetivo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }


        public WorkCenter WorkCenter { get; set; }
    }
}
