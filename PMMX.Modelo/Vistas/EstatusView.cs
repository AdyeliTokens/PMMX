using PMMX.Modelo.Entidades.GembaWalks;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public ICollection<BitacoraGembaWalk> BitacoraGembaWalk { get; set; }
        public ICollection<WorkFlowView> WorkFlowInicial { get; set; }
        public ICollection<WorkFlowView> WorkFlowAnterior { get; set; }
        public ICollection<WorkFlowView> WorkFlowSiguiente { get; set; }
        public ICollection<WorkFlowView> WorkFlowCancelado { get; set; }
        #endregion
    }
}
