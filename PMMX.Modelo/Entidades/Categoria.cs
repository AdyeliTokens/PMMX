using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public string Color { get; set; }
        public bool Activo { get; set; }

        public Persona Responsable { get; set; }
        public ICollection<GrupoPreguntas> GrupoPreguntas { get; set; }
        public ICollection<JustDoIt> JustDoIt { get; set; }
    }
}
