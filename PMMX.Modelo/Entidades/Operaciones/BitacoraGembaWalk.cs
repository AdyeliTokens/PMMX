using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class BitacoraGembaWalk
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdGembaWalk { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public DateTime Fecha { get; set; }

        [StringLength(250)]
        public string Comentario { get; set; }
        #endregion

        #region Navegacion
        public GembaWalk GembaWalk { get; set; }
        public Estatus Estatus { get; set; }
        public Persona Responsable { get; set; }
        #endregion
    }
}
