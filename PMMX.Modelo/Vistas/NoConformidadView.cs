using System;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Vistas
{
    public class NoConformidadView
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public int Calificacion_Low { get; set; }
        public int Calificacion_High { get; set; }
        public int Calificacion_VQI { get; set; }
        public int Calificacion_CSVQI { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }
        public int IdWorkCenter { get; set; }
        public int IdSeccion { get; set; }


        public PersonaView Persona { get; set; }
        public ModuloSeccionView Seccion { get; set; }
        public WorkCenterView WorkCenter { get; set; }
    }
}