using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System.Collections.Generic;

namespace PMMX.Modelo.Entidades
{
    public class ListaDistribucion
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdSubarea { get; set; }
        public int IdPersona { get; set; }
        public int? IdProveedor { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Remitente { get; set; }
        public SubArea SubArea { get; set; }
        public Proveedores Proveedor { get; set; }
        #endregion

    }
}
