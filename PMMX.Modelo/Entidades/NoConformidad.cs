using PMMX.Modelo.Entidades.Maquinaria;
using System;

namespace PMMX.Modelo.Entidades
{
    public class NoConformidad
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Calificacion_Low { get; set; }
        public int Calificacion_High { get; set; }
        public int Calificacion_VQI { get; set; }
        public int Calificacion_CSVQI { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }
        public int IdWorkCenter { get; set; }
        public int IdSeccion { get; set; }
        public int IdAuditoria { get; set; }


        public Persona Persona { get; set; }
        public ModuloSeccion Seccion { get; set; }
        public WorkCenter WorkCenter { get; set; }

    }
}