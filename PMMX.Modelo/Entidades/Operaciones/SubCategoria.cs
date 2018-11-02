using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class SubCategoria
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        [StringLength(250)]
        public string Nombre { get; set; }
        [StringLength(250)]
        public string NombreCorto { get; set; }
        [StringLength(32)]
        public string Tipo { get; set; }
        public int IdResponsable { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Responsable { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Ventana> Ventanas { get; set; }
        public ICollection<GembaWalk> GembaWalk { get; set; }
        public ICollection<WorkFlow> WorkFlows { get; set; }
        public ICollection<Evento> Eventos { get; set; }
        #endregion
    }
}
