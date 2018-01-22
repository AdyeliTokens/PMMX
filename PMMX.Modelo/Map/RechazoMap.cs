using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class RechazoMap : EntityTypeConfiguration<Rechazo>
    {
        public RechazoMap()
        {
            #region Propiedades
            ToTable("Rechazos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdStatus).HasColumnName("IdStatus");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            this.HasMany(x => x.BitacoraVentana).WithOptional(c => c.Rechazo).HasForeignKey(x => x.IdRechazo);
            this.HasMany(x => x.BitacoraGembaWalk).WithOptional(c => c.Rechazo).HasForeignKey(x => x.IdRechazo);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(x => x.Estatus).WithMany(c => c.Rechazo);
            #endregion
        }
    }
}
