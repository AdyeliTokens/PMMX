using PMMX.Modelo.Entidades.GembaWalks;
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
        public int IdVentana { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public DateTime Fecha { get; set; }


        public VentanaView Ventana { get; set; }
        public EstatusView Status { get; set; }
        public PersonaView Responsable { get; set; }
    }
}
