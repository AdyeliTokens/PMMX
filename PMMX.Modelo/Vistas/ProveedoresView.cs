using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class ProveedoresView
    {
        #region Propiedades
        public int Id { get; set; }
        public int NumeroProveedor { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string NombreCorto { get; set; }

        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public ICollection<VentanaView> Ventanas { get; set; }
        #endregion
    }
}
