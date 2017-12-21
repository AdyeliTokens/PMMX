using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class GrupoPreguntasMap : EntityTypeConfiguration<GrupoPreguntas>
    {
        public GrupoPreguntasMap()
        {
            this.ToTable("GrupoPreguntas");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.IdCategoria).HasColumnName("IdCategoria");
            this.Property(c => c.DDS).HasColumnName("DDS");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasRequired(c => c.Categoria).WithMany(p => p.GrupoPreguntas);

            this.HasMany(c => c.WorkCenters).WithMany(p => p.Formatos);
            this.HasMany(c => c.PreguntaTurno).WithRequired(x => x.HealthCheck);
            this.HasMany(c => c.Remitentes).WithRequired(p => p.HealthCheck).HasForeignKey(c => c.IdHealthCheck);
        }

    }
}