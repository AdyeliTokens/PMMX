using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Proveedores
    {
        #region Propiedades
        public int Id { get; set; }
        public int NumeroProveedor { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public ICollection<Ventana> Ventanas { get; set; }
        #endregion
    }
}
