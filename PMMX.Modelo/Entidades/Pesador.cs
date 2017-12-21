using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Pesador
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdPesador { get; set; }
        public int IdArea { get; set; }
        public Boolean Activo { get; set; }

        #endregion

        #region Navegacion

        public Persona Operador_Pesador { get; set; }
        public Area Area { get; set; }

        #endregion
    }
}
