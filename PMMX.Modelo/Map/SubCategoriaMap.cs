using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class SubCategoriaMap : EntityTypeConfiguration<SubCategoria>
    {
        public SubCategoriaMap()
        {
            #region Propiedades
            this.ToTable("SubCategorias");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            this.Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            this.Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Responsable).WithMany(x => x.SubCategorias).HasForeignKey(c => c.IdResponsable);
            this.HasRequired(c => c.Categoria).WithMany(x => x.SubCategorias).HasForeignKey(c => c.IdCategoria);
            #endregion

            #region HasMany
            this.HasMany(c => c.Ventanas).WithRequired(x => x.SubCategoria).HasForeignKey(x => x.IdSubCategoria);
            this.HasMany(c => c.GembaWalk).WithRequired(x => x.SubCategoria).HasForeignKey(x => x.IdSubCategoria);
            this.HasMany(c => c.WorkFlows).WithRequired(x => x.SubCategoria).HasForeignKey(c => c.IdSubCategoria);
            this.HasMany(c => c.Eventos).WithRequired(x => x.SubCategoria).HasForeignKey(c => c.IdSubCategoria);
            #endregion

        }
    }
}