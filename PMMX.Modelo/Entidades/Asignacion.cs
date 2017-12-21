using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Asignacion
    {
        #region Propiedades

        public int Id { get; set; }
        public int IdAsignado { get; set; }
        public DateTime Fecha { get; set; }
        public Boolean Activo { get; set; }

        #endregion

        #region Navegacion
        public Persona Asignado { get; set; }

        public ICollection<Defecto> Defectos { get; set; }
        public ICollection<Paro> Paros { get; set; }
        #endregion
    }
}
