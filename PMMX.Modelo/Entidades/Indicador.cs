using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Indicador
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }

        #endregion

        #region Navegacion
        
        public ICollection<Area> Areas { get; set; }

        #endregion

    }
}
