using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class RechazoView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public EstatusView Estatus { get; set; }
        public ICollection<BitacoraVentanaView> BitacoraVentana { get; set; }
        public ICollection<BitacoraJustDoItView> BitacoraJustDoIt { get; set; }
        #endregion
    }
}
