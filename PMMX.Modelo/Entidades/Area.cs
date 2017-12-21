using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class Area
    {
        #region Propiedades
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion

        public Persona Responsable { get; set; }
        public ICollection<BussinesUnit> BussinessUnits { get; set; }
        public ICollection<Indicador> Indicadores { get; set; }

        #endregion

    }
}
