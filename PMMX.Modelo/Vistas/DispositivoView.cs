using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class DispositivoView
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Llave { get; set; }
        public int IdPersona { get; set; }
        public bool Activo { get; set; }

        public PersonaView Propietario { get; set; }
    }
}
