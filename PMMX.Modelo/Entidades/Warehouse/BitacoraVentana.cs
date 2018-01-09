using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class BitacoraVentana
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdVentana { get; set; }
        public DateTime Fecha { get; set; }
        public int IdResponsable { get; set; }
        public string Comentarios { get; set; }
        public int IdActividadVentana { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        #endregion
    }
}
