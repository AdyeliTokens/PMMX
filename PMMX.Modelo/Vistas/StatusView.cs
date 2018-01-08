using PMMX.Modelo.Entidades.JustDoIts;
using PMMX.Modelo.Entidades.Warehouse;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class StatusView
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NombreCorto { get; set; }
        public int Activo { get; set; }


        public List<StatusVentanaView> StatusVentana { get; set; }
    }
}
