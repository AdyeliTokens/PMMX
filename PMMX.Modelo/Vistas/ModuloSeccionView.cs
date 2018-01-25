using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Vistas
{
    public class ModuloSeccionView
    {
        #region Propiedades
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public ICollection<ModuloView> Modulos { get; set; }
        public ICollection<NoConformidadView> NoConformidades { get; set; }

        #endregion
    }
}