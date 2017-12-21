﻿using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class Modulo
    {
        #region propiedades
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int? IdSeccion { get; set; }
        public bool? Activo { get; set; }
        #endregion

        #region Navegacion
        public ModuloSeccion Seccion { get; set; }
        public ICollection<Origen> Origenes { get; set; }
        #endregion
    }
}