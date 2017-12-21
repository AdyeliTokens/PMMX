using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Locacion
    {
        #region Propiedades
        public int Id { get; set; }
        public string NombreCorto { get; set; }
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }

        #endregion
        
    }
}
