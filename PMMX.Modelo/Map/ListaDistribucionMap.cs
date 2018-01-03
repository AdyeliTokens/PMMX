using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class ListaDistribucionMap : EntityTypeConfiguration<ListaDistribucion>
    {
        public ListaDistribucionMap()
        {
            #region Propiedades
            ToTable("SubArea");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdArea).HasColumnName("IdArea");
            Property(c => c.IdSubarea).HasColumnName("IdSubarea");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Area).WithMany(x => x.ListaDistribucion);
            this.HasRequired(c => c.SubArea).WithMany(x => x.ListaDistribucion);
            this.HasRequired(c => c.Remitente).WithMany(x => x.ListaDistribucion);
            #endregion





        }
    }
}