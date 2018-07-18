using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class SubAreaView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public AreaView Area { get; set; }
        public PersonaView Responsable { get; set; }
        public ICollection<ListaDistribucionView> ListaDistribucion { get; set; }
        public ICollection<WorkFlowView> WorkFlows { get; set; }
        #endregion
    }
}
