using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class OrigenView
    {
        public int Id { get; set; }
        public int? IdModulo { get; set; }
        public int IdWorkCenter { get; set; }
        public int? Orden { get; set; }
        public int ParosActivos { get; set; }
        public int DefectosActivos { get; set; }
        public string NombreOrigen { get; set; }

        public ModuloView Modulo { get; set; }
        public WorkCenterView WorkCenter { get; set; }
        public ICollection<PreguntaTurnoView> PreguntaTurno { get; set; }
        public ICollection<ParoView> Paros { get; set; }
        public ICollection<DefectoView> Defectos { get; set; }
        //public ICollection<PreguntaView> Preguntas { get; set; }
        public ICollection<OrigenRespuestaView> OrigenRespuestas { get; set; }
    }
}