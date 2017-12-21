using System.Collections.Generic;

namespace PMMX.Modelo.Vistas
{
    public class ModuloSeccionView
    {
        #region Propiedades
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public ICollection<ModuloView> Modulos { get; set; }
        public ICollection<NoConformidadView> NoConformidades { get; set; }

        #endregion
    }
}