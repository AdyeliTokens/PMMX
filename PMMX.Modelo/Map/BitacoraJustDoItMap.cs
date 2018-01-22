using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class BitacoraJustDoItMap : EntityTypeConfiguration<BitacoraJustDoIt>
    {
        public BitacoraJustDoItMap()
        {
            #region Propiedades
            ToTable("BitacoraJustDoIt");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdJustDoIt).HasColumnName("IdJustDoIt");
            Property(c => c.IdStatus).HasColumnName("IdStatus");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            Property(c => c.IdRechazo).HasColumnName("IdRechazo");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.Comentario).HasColumnName("Comentarios");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            this.HasOptional(x => x.Rechazo).WithMany(c => c.BitacoraJustDoIt);
            #endregion

            #region HasRequired
            this.HasRequired(x => x.JustDoIt).WithMany(c => c.BitacoraJustDoIt);
            this.HasRequired(x => x.Estatus).WithMany(c => c.BitacoraJustDoIt);
            this.HasRequired(x => x.Responsable).WithMany(c => c.BitacoraJustDoIt);
            #endregion
        }
    }
}
