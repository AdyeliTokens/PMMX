using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class WorkCenter
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdBussinesUnit { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
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
        public ICollection<ObjetivoCRR> ObjetivosCRR { get; set; }
        public ICollection<ObjetivoPlanAttainment> ObjetivosPlanAttainment { get; set; }
        public ICollection<VolumenDeProduccion> VolumenesDeProduccion { get; set; }

        #endregion

    }
}