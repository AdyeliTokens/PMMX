﻿using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class RecursosMap : EntityTypeConfiguration<Recursos>
    {
        public RecursosMap()
        {
            #region Propiedades
            ToTable("Recursos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            #endregion
        }
    }
}
