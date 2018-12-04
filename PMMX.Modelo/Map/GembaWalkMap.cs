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
            this.Property(c => c.IdReporta).HasColumnName("IdReporta");
            this.Property(c => c.IdOrigen).HasColumnName("IdOrigen");
            this.Property(c => c.IdSubCategoria).HasColumnName("IdSubCategoria");
            this.Property(c => c.Descripcion).HasColumnName("Descripcion");
            this.Property(c => c.Impacto).HasColumnName("Impacto");
            this.Property(c => c.ParametroIncumplido).HasColumnName("ParametroIncumplido");
            this.Property(c => c.AccionInmediata).HasColumnName("AccionInmediata");
            this.Property(c => c.FechaReporte).HasColumnName("FechaReporte");
            this.Property(c => c.Prioridad).HasColumnName("Prioridad");
            this.Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Reportador).WithMany(p => p.JustDoItReportados);
            this.HasRequired(c => c.Origen).WithMany(p => p.GembaWalk);
            this.HasRequired(c => c.SubCategoria).WithMany(p => p.GembaWalk);
            #endregion

            #region HasMany
            this.HasMany(c => c.Fotos).WithMany(x => x.GembaWalks).Map(cs =>
            {
                cs.MapLeftKey("IdGembaWalk");
                cs.MapRightKey("IdFoto");
                cs.ToTable("GembaWalkFotos");
            });

            this.HasMany(c => c.ClasificacionHallazgos).WithMany(x => x.GembaWalks).Map(cs =>
            {
                cs.MapLeftKey("IdGembaWalk");
                cs.MapRightKey("IdClasificacion");
                cs.ToTable("ClasificacionGembaWalk");
            });

            this.HasMany(x => x.BitacoraGembaWalk).WithRequired(c => c.GembaWalk).HasForeignKey(c => c.IdGembaWalk);
            #endregion
        }
    }
}
