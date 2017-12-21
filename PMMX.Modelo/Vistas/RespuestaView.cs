using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class RespuestaView
    {
        public int Id { get; set; }
        public int IdPregunta { get; set; }
        public int IdOrigenRespuesta { get; set; }
        public bool Solucion { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public bool Activo { get; set; }
        public int TotalSolucion { get; set; }
        public int TotalSi { get; set; }
        public int TotalNo { get; set; }
        public int PorcentajeSi { get; set; }
        public int PorcentajeNo { get; set; }
        public string DescripcionPregunta { get; set; }
        public string RespuestaBy { get; set; }

        public Pregunta Pregunta { get; set; }
        public OrigenRespuestaView OrigenRespuesta { get; set; }

        public ICollection<PreguntaTurnoView> PreguntasTurno { get; set; }
    }
}