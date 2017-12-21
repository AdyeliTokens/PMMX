using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class PreguntaView
    {
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string Interrogante { get; set; }
        public bool EnParo { get; set; }
        public string Herramientas { get; set; }
        public string EPP { get; set; }
        public DateTime? TiempoEstimado { get; set; }
        public Boolean Activo { get; set; }

        public GrupoPreguntasView GrupoPreguntas { get; set; }
        public ICollection<Dia> Dias { get; set; }
        public ICollection<Turno> Turnos { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }
    }
}