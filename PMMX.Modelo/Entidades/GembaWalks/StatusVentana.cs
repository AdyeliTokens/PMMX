using PMMX.Modelo.Entidades.GembaWalks;
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime Fecha { get; set; }
        [StringLength(250)]
        public string Comentarios { get; set; }

        public Ventana Ventana { get; set; }
        public Estatus Status { get; set; }
        public Persona Responsable { get; set; }
    }
}
