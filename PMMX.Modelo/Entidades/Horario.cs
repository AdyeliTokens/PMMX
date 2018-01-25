using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Horario
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Desccripcion { get; set; }
        public DateTime Hora_Inicial { get; set; }
        public DateTime Hora_Final { get; set; }
        public Boolean Activo { get; set; }


        


    }
}
