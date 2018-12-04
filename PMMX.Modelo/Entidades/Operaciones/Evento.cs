
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
    public class Evento
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
        public bool Activo { get; set; }

        public Persona Asignador { get; set; }
        public Categoria Categoria { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public List<Ventana> Ventanas { get; set; }
        public List<EventoOrigen> EventoOrigen { get; set; }
        public List<EventoResponsable> EventoResponsable { get; set; }

    }
}
