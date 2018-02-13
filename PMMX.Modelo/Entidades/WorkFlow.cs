using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;

namespace PMMX.Modelo.Entidades
{
    public class WorkFlow
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdSubCategoria { get; set; }
        public int? Inicial { get; set; }
        public int? Anterior { get; set; }
        public int? Siguiente { get; set; }
        public bool Cancelado { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public SubCategoria SubCategoria { get; set; }
        public Estatus EstatusInicial { get; set; }
        public Estatus EstatusAnterior { get; set; }
        public Estatus EstatusSiguiente { get; set; }
        #endregion
    }
}
