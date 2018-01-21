using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Warehouse
{
    public class Rechazos
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Estatus Estatus { get; set; }
        #endregion
    }
}
