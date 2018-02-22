using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;

namespace PMMX.Modelo.Entidades
{
    public class ListaDistribucion
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdSubarea { get; set; }
        public int IdPersona { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Remitente { get; set; }
        public SubArea SubArea { get; set; }
        #endregion

    }
}
