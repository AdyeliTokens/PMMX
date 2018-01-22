using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class GembaWalkMap : EntityTypeConfiguration<GembaWalk>
    {
        public GembaWalkMap()
        {
            #region Propiedades
            this.ToTable("GembaWalk");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdEvento).HasColumnName("IdEvento");
            this.Property(c => c.IdReportador).HasColumnName("IdReporteador");
            this.Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            this.Property(c => c.IdOrigen).HasColumnName("IdOrigen");
            this.Property(c => c.IdSubCategoria).HasColumnName("IdSubCategoria");
            this.Property(c => c.Descripcion).HasColumnName("Descripcion");
            this.Property(c => c.Activo).HasColumnName("Activo");
            this.Property(c => c.FechaReporte).HasColumnName("FechaReporte");
            this.Property(c => c.FechaEstimada).HasColumnName("FechaEstimada");
            this.Property(c => c.Prioridad).HasColumnName("Prioridad");
            this.Property(c => c.Tipo).HasColumnName("Tipo");
            this.Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Reportador).WithMany(p => p.JustDoItReportados);
            this.HasRequired(c => c.Responsable).WithMany(p => p.JustDoItAsignados);
            this.HasRequired(c => c.Origen).WithMany(p => p.GembaWalk);
            this.HasRequired(c => c.Evento).WithMany(p => p.GembaWalk);
            this.HasRequired(c => c.SubCategoria).WithMany(p => p.GembaWalk);
            #endregion

            #region HasMany
            this.HasMany(c => c.Fotos).WithMany(x => x.GembaWalks).Map(cs =>
            {
                cs.MapLeftKey("IdGembaWalk");
                cs.MapRightKey("IdFoto");
                cs.ToTable("GembaWalkFotos");
            });

            this.HasMany(x => x.BitacoraGembaWalk).WithRequired(c => c.GembaWalk).HasForeignKey(c => c.IdGembaWalk);
            #endregion
        }
    }
}
