using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class SubCategoria
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public bool Activo { get; set; }

        public Persona Responsable { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<Ventana> Ventanas { get; set; }
        public ICollection<JustDoIt> JustDoIt { get; set; }
    }
}
