using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;

namespace PMMX.Modelo.Entidades
{
    public class Operadores
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdOperador { get; set; }
        public int IdWorkCenter { get; set; }
        public Boolean Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Operador { get; set; }
        public WorkCenter WorkCenter { get; set; }

        public ICollection<ModuloSeccion> Secciones { get; set; }
        #endregion

    }
}
