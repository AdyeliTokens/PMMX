using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Vistas
{
    public class EstatusView
    {
        #region Propiedades
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public CategoriaView Categoria { get; set; }

        public ICollection<StatusVentanaView> StatusVentana { get; set; }
        public ICollection<RechazoView> Rechazo { get; set; }
        public ICollection<BitacoraVentanaView> BitacoraVentana { get; set; }
        public ICollection<BitacoraGembaWalkView> BitacoraGembaWalk { get; set; }
        public ICollection<WorkFlowView> WorkFlowInicial { get; set; }
        public ICollection<WorkFlowView> WorkFlowAnterior { get; set; }
        public ICollection<WorkFlowView> WorkFlowSiguiente { get; set; }
        public ICollection<WorkFlowView> WorkFlowCancelado { get; set; }
        #endregion
    }
}
