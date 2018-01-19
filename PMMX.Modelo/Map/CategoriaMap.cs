using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class CategoriaMap : EntityTypeConfiguration<Categoria>
    {
        public CategoriaMap()
        {
            this.ToTable("Categorias");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            this.Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            this.Property(c => c.Color).HasColumnName("Color");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasRequired(c => c.Responsable).WithMany(x => x.Categorias).HasForeignKey(c => c.IdResponsable);

            this.HasMany(c => c.SubCategorias).WithRequired(x => x.Categoria).HasForeignKey(c => c.IdCategoria);
            this.HasMany(c => c.GrupoPreguntas).WithRequired(x => x.Categoria).HasForeignKey(c => c.IdCategoria);
            this.HasMany(c => c.Eventos).WithRequired(x => x.Categoria).HasForeignKey(c => c.IdCategoria);
        }
    }
}