using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class StatusMap : EntityTypeConfiguration<Status>
    {
        public StatusMap()
        {
            #region Propiedades
            ToTable("Status");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            this.HasMany(c => c.StatusVentana).WithRequired(x => x.Status).HasForeignKey(c => c.IdStatus);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            #endregion
        }
    }
}
