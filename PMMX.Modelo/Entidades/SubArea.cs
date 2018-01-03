using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class SubArea
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Area Area { get; set; }
        public Persona Responsable { get; set; }
        public ICollection<ListaDistribucion> ListaDistribucion { get; set; }
        #endregion

    }
}
