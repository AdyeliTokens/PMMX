using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class PreguntaTurnoView
    {
        public int Id { get; set; }
        public int IdPregunta { get; set; }
        public int IdOrigen { get; set; }
        public bool Activo { get; set; }
        public int IdDia { get; set; }
        public int IdTurno { get; set; }
        public int IdGrupo { get; set; }
        

        public PreguntaView Pregunta { get; set; }
        public OrigenView Origen { get; set; }
        public GrupoPreguntas GrupoPreguntas { get; set; }
        //public ICollection<RespuestaView> Respuestas { get; set; }
        public Dia Dia { get; set; }
        public Turno Turno { get; set; }
    }
}