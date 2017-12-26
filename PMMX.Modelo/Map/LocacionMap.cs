using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class LocacionMap : EntityTypeConfiguration<Locacion>
    {
        public LocacionMap()
        {
            #region Propiedades
            ToTable("Locacion");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.Nombre).HasColumnName("Nombre");

            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            this.HasMany(c => c.VentanasProcedencia).WithRequired(x => x.Procedencia).HasForeignKey(c => c.IdProcedencia);
            this.HasMany(c => c.VentanasDestino).WithRequired(x => x.Destino).HasForeignKey(c => c.IdDestino);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            #endregion
        }
    }
}
