using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class BitacoraJustDoIt
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdJustDoIt { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public int? IdRechazo { get; set; }
        public DateTime Fecha { get; set; }
        public String Comentario { get; set; }
        #endregion

        #region Navegacion
        public JustDoIt JustDoIt { get; set; }
        public Estatus Estatus { get; set; }
        public Persona Responsable { get; set; }
        public Rechazo Rechazo { get; set; }
        #endregion
    }
}
