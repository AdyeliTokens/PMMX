using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades
{
    public class Entorno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }

        public ICollection<User> Usuarios { get; set; }
    }
}