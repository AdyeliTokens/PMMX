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
    public class Evento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdAsignador { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaInicio { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaFin { get; set; }
        public string Nota { get; set; }
        public bool EsRecurrente { get; set; }
        public bool Activo { get; set; }

        public Persona Asignador { get; set; }

        public ICollection<JustDoIt> JustDoIt { get; set; }
        public ICollection<Ventana> Ventanas { get; set; }
        public ICollection<EventoOrigen> EventoOrigen { get; set; }
        public ICollection<EventoResponsable> EventoResponsable { get; set; }

    }
}
