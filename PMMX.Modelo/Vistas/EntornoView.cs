using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class EntornoView
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}