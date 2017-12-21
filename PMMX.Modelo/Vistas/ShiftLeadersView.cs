using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class ShiftLeadersView
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdShiftLeader { get; set; }
        public int IdCelula { get; set; }
        public Boolean Activo { get; set; }

        #endregion

        #region Navegacion

        public PersonaView ShiftLeader { get; set; }
        public BussinesUnitView BussinesUnit { get; set; }

        #endregion
    }
}
