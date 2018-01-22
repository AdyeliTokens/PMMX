using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PMMX.Modelo.Entidades.GembaWalks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class ListaDistribucion
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdSubarea { get; set; }
        public int IdPersona { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Remitente { get; set; }
        public SubArea SubArea { get; set; }
        #endregion

    }
}
