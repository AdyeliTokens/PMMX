using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Locacion
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
        public ICollection<Ventana> VentanasProcedencia { get; set; }
        public ICollection<Ventana> VentanasDestino { get; set; }
        #endregion

    }
}
