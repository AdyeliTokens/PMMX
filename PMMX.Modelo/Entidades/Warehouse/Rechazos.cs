using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Rechazo
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdStatus { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Estatus Estatus { get; set; }
        public ICollection<BitacoraVentana> BitacoraVentana { get; set; }
        public ICollection<BitacoraGembaWalk> BitacoraGembaWalk { get; set; }
        #endregion
    }
}
