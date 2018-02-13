using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class TipoOperacionMap : EntityTypeConfiguration<TipoOperacion>
    {
        public TipoOperacionMap()
        {
            #region Propiedades
            ToTable("TipoOperacion");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            this.HasMany(c => c.Ventanas).WithRequired(x => x.TipoOperacion).HasForeignKey(c => c.IdOperacion);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            #endregion
        }
    }
}