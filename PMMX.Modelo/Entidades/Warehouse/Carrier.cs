using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Carrier
    {
        #region Propiedades
        public int Id { get; set; }
        [StringLength(250)]
        public string Nombre { get; set; }
        [StringLength(250)]
        public string NombreCorto { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public ICollection<Ventana> Ventanas { get; set; }
        #endregion
    }
}
