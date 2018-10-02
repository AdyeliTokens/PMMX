using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class FormatoView
    {
        #region  Propiedades
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEfectividad { get; set; }
        public int TiempoRetencion { get; set; }
        public int Version { get; set; }
        #endregion

        #region Navegación
        public ICollection<RegistroUnidadView> RegistrosUnidad { get; set; }
        #endregion
    }
}
