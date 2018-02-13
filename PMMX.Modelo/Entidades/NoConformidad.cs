using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Entidades
{
    public class NoConformidad
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Code { get; set; }
        public string CodeDescription { get; set; }
        public int Calificacion_VQI { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }
        public int IdWorkCenter { get; set; }
        public int IdSeccion { get; set; }
        

        public Persona Persona { get; set; }
        public ModuloSeccion Seccion { get; set; }
        public WorkCenter WorkCenter { get; set; }

    }
}