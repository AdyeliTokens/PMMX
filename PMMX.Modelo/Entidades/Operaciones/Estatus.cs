using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class Estatus
    {
        #region Propiedades
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Categoria Categoria { get; set; }

        public ICollection<StatusVentana> StatusVentana { get; set; }
        public ICollection<Rechazo> Rechazo { get; set; }
        public ICollection<BitacoraVentana> BitacoraVentana { get; set; }
        public ICollection<BitacoraGembaWalk> BitacoraGembaWalk { get; set; }
        public ICollection<WorkFlow> WorkFlowInicial { get; set; }
        public ICollection<WorkFlow> WorkFlowAnterior { get; set; }
        public ICollection<WorkFlow> WorkFlowSiguiente { get; set; }
        #endregion
    }
}
