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
        #region Propiedades
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string Interrogante { get; set; }
        public string Anexo1 { get; set; }
        public string Anexo2 { get; set; }
        public int? Tipo { get; set; }
        public Boolean Activo { get; set; }
        #endregion

        #region Navegacion
        public GrupoPreguntas GrupoPreguntas { get; set; }
        public ICollection<Dia> Dias { get; set; }
        public ICollection<Turno> Turnos { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }
        #endregion
    }
}