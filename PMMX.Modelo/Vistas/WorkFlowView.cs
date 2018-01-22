using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class WorkFlowView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public int? Inicial { get; set; }
        public int? Anterior { get; set; }
        public int? Siguiente { get; set; }
        public int? Cancelado { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public CategoriaView Categoria { get; set; }
        public EstatusView EstatusInicial { get; set; }
        public EstatusView EstatusAnterior { get; set; }
        public EstatusView EstatusSiguiente { get; set; }
        public EstatusView EstatusCancelado { get; set; }
        #endregion
    }
}
