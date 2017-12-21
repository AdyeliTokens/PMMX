using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public class ShiftLeaders
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdShiftLeader { get; set; }
        public int IdCelula { get; set; }
        public Boolean Activo { get; set; }

        #endregion

        #region Navegacion

        public Persona ShiftLeader { get; set; }
        public BussinesUnit BussinesUnit { get; set; }

        #endregion

    }
}
