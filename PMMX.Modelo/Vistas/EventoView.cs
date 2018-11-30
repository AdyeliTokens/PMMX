
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class EventoView
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public int IdAsignador { get; set; }
        public int IdCategoria { get; set; }
        public int IdSubCategoria { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaInicio { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaFin { get; set; }

        [StringLength(250)]
        public string Nota { get; set; }
        [StringLength(20)]
        public string Color { get; set; }
        [StringLength(20)]
        public string Clasificacion { get; set; }
        public bool Activo { get; set; }

        public PersonaView Asignador { get; set; }
        public CategoriaView Categoria { get; set; }
        public SubCategoriaView SubCategoria { get; set; }
        public List<GembaWalkView> GembaWalk { get; set; }
        public List<VentanaView> Ventanas { get; set; }
        public List<EventoOrigenView> EventoOrigen { get; set; }
        public List<EventoResponsableView> EventoResponsable { get; set; }
    }
}
