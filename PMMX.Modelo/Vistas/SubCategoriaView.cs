using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class SubCategoriaView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public int IdGrupo { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public PersonaView Responsable { get; set; }
        public CategoriaView Categoria { get; set; }
        
        public ICollection<VentanaView> Ventanas { get; set; }
        public ICollection<GembaWalkView> GembaWalk { get; set; }
        #endregion
    }
}
