using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class ModuloSeccion
    {
        
        #region Propiedades

        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        
        #endregion

        #region Navegacion

        public ICollection<Modulo> Modulos { get; set; }
        public ICollection<Mecanicos> Mecanicos { get; set; }
        public ICollection<Electricos> Electricos { get; set; }
        public ICollection<Operadores> Operadores { get; set; }
        public ICollection<NoConformidad> NoConformidades { get; set; }
        public ICollection<Desperdicio> Desperdicios { get; set; }

        #endregion
    }
}