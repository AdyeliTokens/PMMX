using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class StatusVentanaMap : EntityTypeConfiguration<StatusVentana>
    {
        public StatusVentanaMap()
        {
            #region Propiedades
            ToTable("StatusVentana");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdVentana).HasColumnName("IdVentana");
            Property(c => c.IdStatus).HasColumnName("IdStatus");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            Property(c => c.Fecha).HasColumnName("Fecha");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Ventana).WithMany(x => x.StatusVentana);
            this.HasRequired(c => c.Status).WithMany(x => x.StatusVentana);
            this.HasRequired(c => c.Responsable).WithMany(x => x.StatusVentana);
            #endregion
        }
    }
}
