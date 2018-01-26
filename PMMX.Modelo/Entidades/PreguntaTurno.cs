using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades
{
    public class PreguntaTurno
    {
        public int Id { get; set; }
        public int IdPregunta { get; set; }
        public int IdOrigen { get; set; }
        public bool Activo { get; set; }
        public int IdDia { get; set; }
        public int IdTurno { get; set; }
        public int IdGrupo { get; set; }

        public Pregunta Pregunta { get; set; }
        public Origen Origen { get; set; }
        public GrupoPreguntas HealthCheck { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }
        public Dia Dia { get; set; }
        public Turno Turno { get; set; }

        public static implicit operator PreguntaTurno(List<PreguntaTurno> v)
        {
            throw new NotImplementedException();
        }
    }
}
