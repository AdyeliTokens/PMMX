using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class MantenimientoMap : EntityTypeConfiguration<Mantenimiento>
    {
        public MantenimientoMap()
        {
            this.ToTable("Mantenimiento");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdReportador).HasColumnName("IdReporteador");
            this.Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            this.Property(c => c.IdOrigen).HasColumnName("IdOrigen");
            this.Property(c => c.Descripcion).HasColumnName("Descripcion");
            this.Property(c => c.Activo).HasColumnName("Activo");
            this.Property(c => c.FechaReporte).HasColumnName("FechaReporte");
            this.Property(c => c.FechaEstimada).HasColumnName("FechaEstimada");
            this.Property(c => c.Prioridad).HasColumnName("Prioridad");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasRequired(c => c.Reportador).WithMany(p => p.MantenimientosReportados);
            this.HasRequired(c => c.Responsable).WithMany(p => p.MantenimientosAsignados);
            this.HasRequired(c => c.Origen).WithMany(p => p.Mantenimientos);

            this.HasMany(c => c.Fotos).WithOptional(f => f.Mantenimiento).HasForeignKey(c => c.IdMantenimiento);
        }
    }
}
