using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class EventoMap : EntityTypeConfiguration<Evento>
    {
        public EventoMap()
        {
            this.ToTable("Eventos");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Descripcion).HasColumnName("Descripcion");
            this.Property(c => c.IdOrigen).HasColumnName("IdOrigen");
            this.Property(c => c.IdCategoria).HasColumnName("IdCategoria");
            this.Property(c => c.IdAsignador).HasColumnName("IdAsignador");
            this.Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            this.Property(c => c.FechaInicio).HasColumnName("FechaInicio");
            this.Property(c => c.FechaFin).HasColumnName("FechaFin");
            this.Property(c => c.Nota).HasColumnName("Nota");
            this.Property(c => c.EsRecurrente).HasColumnName("EsRecurrente");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasRequired(c => c.Responsable).WithMany(x => x.EventosAsignados).HasForeignKey(c => c.IdResponsable);
            this.HasRequired(c => c.Asignador).WithMany(x => x.EventosReportados).HasForeignKey(c => c.IdAsignador);
            this.HasRequired(c => c.Origen).WithMany(x => x.Eventos).HasForeignKey(c => c.IdOrigen);
            this.HasRequired(c => c.Categoria).WithMany(x => x.Eventos).HasForeignKey(c => c.IdCategoria);

            this.HasMany(c => c.JustDoIt).WithRequired(x => x.Evento).HasForeignKey(c => c.IdEvento);
            this.HasMany(c => c.Ventanas).WithRequired(x => x.Evento).HasForeignKey(c => c.IdEvento);
        }
    }
}
