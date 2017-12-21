using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;

namespace PMMX.Modelo.Entidades
{
    public class Mecanicos
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdMecanico { get; set; }
        public int IdBusinessUnit { get; set; }
        public Boolean Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Mecanico { get; set; }
        public BussinesUnit BusinessUnit { get; set; }
        public ICollection<ModuloSeccion> Secciones { get; set; }

        #endregion
    }
}
