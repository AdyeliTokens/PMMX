using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class ModuloView
    {
        public int Id { get; set; }
        public string  Nombre { get; set; }
        public string NombreCorto { get; set; }
        public bool? Activo { get; set; }
        public ModuloSeccionView Seccion { get; set; }

    }
}