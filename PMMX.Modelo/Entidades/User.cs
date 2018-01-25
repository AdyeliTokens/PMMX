using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades
{
    public class User
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Password { get; set; }
        public Boolean Reset_password { get; set; }
        public int Key_password { get; set; }
        public Boolean Activo { get; set; }

        public Persona Persona { get; set; }
        public ICollection<Entorno> Entornos { get; set; }
    }
}