using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades
{
    public class Respuesta
    {
        public int Id { get; set; }
        public int IdPregunta { get; set; }
        public int IdOrigenRespuesta { get; set; }
        public bool Solucion { get; set; }
        public DateTime Fecha { get; set; }

        [StringLength(250)]
        public string Comentario { get; set; }
        public bool Activo { get; set; }

        public Pregunta Pregunta { get; set; }
        public OrigenRespuesta OrigenRespuesta { get; set; }
        public ICollection<PreguntaTurno> PreguntasTurno { get; set; }
    }
}