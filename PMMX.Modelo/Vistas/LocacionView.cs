using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class LocacionView
    {
        #region Propiedades
        public int Id { get; set; }

        [StringLength(250)]
        public string NombreCorto { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }

        #endregion

        #region Navegacion
        public ICollection<VentanaView> VentanasProcedencia { get; set; }
        public ICollection<VentanaView> VentanasDestino { get; set; }
        #endregion
    }
}
