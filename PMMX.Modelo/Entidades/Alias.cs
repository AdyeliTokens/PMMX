using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Alias
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }

        public Persona PersonaQueDioDeAlta { get; set; }
        public ICollection<WorkCenter> WorkCenters { get; set; }

    }
}
