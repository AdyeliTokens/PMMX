using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class PreguntaTurnoMap : EntityTypeConfiguration<PreguntaTurno>
    {
        public PreguntaTurnoMap()
        {
            this.ToTable("PreguntaTurno");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdPregunta).HasColumnName("IdPregunta");
            this.Property(c => c.IdDia).HasColumnName("IdDia");
            this.Property(c => c.IdTurno).HasColumnName("IdTurno");
            this.Property(c => c.IdOrigen).HasColumnName("IdModuloWorkCenter");
            this.Property(c => c.IdHealthCheck).HasColumnName("IdHealthCheck");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasRequired(c => c.Origen).WithMany(x => x.PreguntaTurno).HasForeignKey(c => c.IdOrigen);
            this.HasRequired(c => c.Dia).WithMany(x => x.PreguntasTurno).HasForeignKey(c => c.IdDia);
            this.HasRequired(c => c.Turno).WithMany(x => x.PreguntaTurno).HasForeignKey(c => c.IdTurno);
            this.HasRequired(c => c.HealthCheck).WithMany(x => x.PreguntaTurno).HasForeignKey(c => c.IdHealthCheck);

            this.HasMany(c => c.Respuestas).WithMany(x => x.PreguntasTurno);

        }
    }
}