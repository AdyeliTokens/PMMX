﻿using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class WorkCenter
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdBussinesUnit { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public bool Activo { get; set; }

        #endregion

        #region Navegacion

        public BussinesUnit BussinesUnit { get; set; }
        public Persona Responsable { get; set; }
        public ICollection<Operadores> Operadores { get; set; }
        public ICollection<Origen> Origenes { get; set; }
        public ICollection<GrupoPreguntas> Formatos { get; set; }
        public ICollection<NoConformidad> NoConformidades { get; set; }
        public ICollection<Desperdicio> Desperdicios { get; set; }
        public ICollection<ObjetivoVQI> ObjetivosVQI { get; set; }

        #endregion

    }
}