using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class DatosUnidadView
    {
        #region Propiedades
        public int Id { get; set; }
        public string Placas { get; set; }
        public string NoEco { get; set; }
        public string NoCaja { get; set; }
        public string TipoRemolque { get; set; }
        public int IdRegistroUnidad { get; set; }
        #endregion

        #region Navegacion
        public RegistroUnidadView RegistroUnidad { get; set; }
        #endregion
    }
}
