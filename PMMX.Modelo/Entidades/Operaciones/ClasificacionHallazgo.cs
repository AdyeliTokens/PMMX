using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades.Operaciones;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class ClasificacionHallazgo
    {
        #region Propiedades
        public int Id{get;set;}
        public string Nombre { get; set; }
        public int IdSubCategoria { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public SubCategoria SubCategoria { get; set; }
        public ICollection<GembaWalk> GembaWalks { get; set; }
        #endregion

    }
}
