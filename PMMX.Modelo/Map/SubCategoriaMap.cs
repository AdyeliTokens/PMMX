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
            this.ToTable("SubCategorias");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            this.Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasRequired(c => c.Responsable).WithMany(x => x.SubCategorias).HasForeignKey(c => c.IdResponsable);
        }
    }
}