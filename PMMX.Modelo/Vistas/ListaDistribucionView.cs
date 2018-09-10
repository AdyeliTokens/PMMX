using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class ListaDistribucionView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdSubarea { get; set; }
        public int IdPersona { get; set; }
        public int IdProveedor { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public PersonaView Remitente { get; set; }
        public SubAreaView SubArea { get; set; }
        public ProveedoresView Proveedor { get; set; }
        #endregion
    }
}
