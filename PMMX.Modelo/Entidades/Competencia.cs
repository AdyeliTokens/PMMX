using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades
{
    public class Competencia
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string NombreCorto { get; set; }
        public Boolean Activo { get; set; }
    }
}