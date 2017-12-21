using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Electricos
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdElectrico { get; set; }
        public int IdBusinessUnit { get; set; }
        public Boolean Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Electrico_Persona { get; set; }
        public BussinesUnit BusinessUnit { get; set; }
        public ICollection<ModuloSeccion> Secciones { get; set; }

        #endregion
    }
}
