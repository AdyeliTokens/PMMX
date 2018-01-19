using PMMX.Modelo.Entidades.JustDoIts;
using PMMX.Modelo.Entidades.Warehouse;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class EstatusView
    {
        #region Propiedades
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public CategoriaView Categoria { get; set; }

        public ICollection<StatusVentanaView> StatusVentana { get; set; }
        #endregion
    }
}
