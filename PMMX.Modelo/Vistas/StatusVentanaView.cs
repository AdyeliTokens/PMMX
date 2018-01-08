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
    public class StatusVentanaView
    {
        public int Id { get; set; }
        public string IdVentana { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public int Fecha { get; set; }


        public VentanaView Ventana { get; set; }
        public StatusView Status { get; set; }
        public PersonaView Responsable { get; set; }
    }
}
