using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class CategoriaMap : EntityTypeConfiguration<Categoria>
    {
        public CategoriaMap()
        {
            #region Propiedades
            this.ToTable("Categorias");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            this.Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            this.Property(c => c.Color).HasColumnName("Color");
            this.Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Responsable).WithMany(x => x.Categorias).HasForeignKey(c => c.IdResponsable);
            #endregion

            #region HasMany
            this.HasMany(c => c.SubCategorias).WithRequired(x => x.Categoria).HasForeignKey(c => c.IdCategoria);
            this.HasMany(c => c.GrupoPreguntas).WithRequired(x => x.Categoria).HasForeignKey(c => c.IdCategoria);
            this.HasMany(c => c.Eventos).WithRequired(x => x.Categoria).HasForeignKey(c => c.IdCategoria);
            this.HasMany(c => c.Estatus).WithRequired(x => x.Categoria).HasForeignKey(c => c.IdCategoria);
            #endregion
        }
    }
}