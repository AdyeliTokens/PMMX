using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    /// <summary>
    /// 
    /// </summary>
    public class BussinesUnit
    {
        
        #region Propiedades

        public int Id { get; set; }
        public int IdResponsable { get; set; }
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public Boolean Activo { get; set; }

        #endregion

        #region Navegacion

        public Persona Responsable { get; set; }
        public Area Area { get; set; }
        public ICollection<WorkCenter> WorkCenters { get; set; }
        public ICollection<Mecanicos> Mecanicos { get; set; }
        public ICollection<Electricos> Electricos { get; set; }
        public ICollection<ShiftLeaders> ShiftLeaders { get; set; }

        #endregion

    }
}