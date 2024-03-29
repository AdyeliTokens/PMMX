﻿using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class SubArea
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdArea { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string NombreCorto { get; set; }

        public int IdResponsable { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public Area Area { get; set; }
        public Persona Responsable { get; set; }
        
        public ICollection<ListaDistribucion> ListaDistribucion { get; set; }
        public ICollection<WorkFlow> WorkFlows { get; set; }
        public ICollection<WorkFlow> WorkFlowAreaANotificar { get; set; }
        #endregion

    }
}
