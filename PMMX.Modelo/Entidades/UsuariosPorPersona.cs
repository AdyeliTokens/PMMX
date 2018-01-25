using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class UsuariosPorPersona
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string IdAspNetUser { get; set; }
        public int IdPersona { get; set; }

        public AspNetUser Usuario { get; set; }
        public Persona Persona { get; set; }
    }
}
