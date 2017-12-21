using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Alias
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }

        public Persona PersonaQueDioDeAlta { get; set; }

    }
}
