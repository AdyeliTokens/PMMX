using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class Categoria
    {
        #region Propiedades
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }

        [StringLength(250)]
        public string Color { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Responsable { get; set; }
        public ICollection<SubCategoria> SubCategorias { get; set; }
        public ICollection<GrupoPreguntas> GrupoPreguntas { get; set; }
        public ICollection<Evento> Eventos { get; set; }
        public ICollection<Estatus> Estatus { get; set; }
        #endregion
    }
}
