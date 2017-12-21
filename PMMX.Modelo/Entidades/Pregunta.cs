using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PMMX.Modelo.Entidades
{
    public class Pregunta
    {
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string Interrogante { get; set; }
        public bool EnParo { get; set; }
        public string Herramientas { get; set; }
        public string EPP { get; set; }
        public DateTime? TiempoEstimado { get; set; }
        public Boolean Activo { get; set; }

        public GrupoPreguntas GrupoPreguntas { get; set; }
        public ICollection<Dia> Dias { get; set; }
        public ICollection<Turno> Turnos { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }
    }
}