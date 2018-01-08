using PMMX.Modelo.Entidades.JustDoIts;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class StatusVentana
    {
        public int Id { get; set; }
        public int IdVentana { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public int Fecha { get; set; }


        public Ventana Ventana { get; set; }
        public Status Status { get; set; }
        public Persona Responsable { get; set; }
    }
}
