using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class UserView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? IdPersona { get; set; }
        public PersonaView Persona { get; set; }
        public int IdEntorno { get; set; }
        public Boolean Reset_password { get; set; }
        public int Key_password { get; set; }
        public Boolean Activo { get; set; }
        public EntornoView Entorno { get; set; }

        public IList<EntornoView> Entornos { get; set; }
    }
}