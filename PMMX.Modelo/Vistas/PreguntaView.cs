using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class PreguntaView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdGrupo { get; set; }

        [StringLength(250)]
        public string Interrogante { get; set; }

        [StringLength(250)]
        public string Anexo1 { get; set; }

        [StringLength(250)]
        public string Anexo2 { get; set; }
        public int? Tipo { get; set; }
        public Boolean Activo { get; set; }
        #endregion

        #region Navegacion
        public GrupoPreguntasView GrupoPreguntas { get; set; }
        public ICollection<Dia> Dias { get; set; }
        public ICollection<Turno> Turnos { get; set; }
        public ICollection<RespuestaView> Respuestas { get; set; }
        #endregion
    }
}