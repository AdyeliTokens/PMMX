﻿using PMMX.Modelo.Entidades.SeguridadFisica;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class BitacoraUnidadMap : EntityTypeConfiguration<BitacoraUnidad>
    {
        public BitacoraUnidadMap()
        {
            #region Propiedades
            ToTable("BitacoraUnidad");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.NombreGuardia).HasColumnName("NombreGuardia");
            Property(c => c.Puerta).HasColumnName("Puerta");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.TipoMovimiento).HasColumnName("TipoMovimiento");
            Property(c => c.IdRegistroUnidad).HasColumnName("IdRegistroUnidad");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(r => r.RegistroUnidad).WithMany(e => e.Bitacora);
            this.HasRequired(r => r.Persona).WithMany(e => e.BitacorasUnidad);
            #endregion         
        }

    }
}
