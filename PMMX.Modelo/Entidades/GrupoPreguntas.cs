using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PMMX.Modelo.Entidades
{
    public class GrupoPreguntas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool DDS { get; set; }
        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        

        public Categoria Categoria { get; set; }
        
        public ICollection<Pregunta> Preguntas { get; set; }
        public ICollection<WorkCenter> WorkCenters { get; set; }
        public ICollection<PreguntaTurno> PreguntaTurno { get; set; }
        public ICollection<Remitentes> Remitentes { get; set; }
        
    }
}