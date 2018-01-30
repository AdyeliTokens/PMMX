using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class ParoMap : EntityTypeConfiguration<Paro>
    {
        /// <summary>
        /// 
        /// </summary>
        public ParoMap()
        {
            #region Propiedades
            ToTable("Paros");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdReportador).HasColumnName("IdReporteador");
            Property(c => c.IdMecanico).HasColumnName("IdResponsable");
            Property(c => c.IdOrigen).HasColumnName("IdModuloWorkCenter");
            Property(c => c.Descripcion).HasColumnName("Descripcion");
            Property(c => c.FechaReporte).HasColumnName("FechaReporte");
            Property(c => c.Motivo).HasColumnName("Motivo");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasRequired
            HasRequired(c => c.Reportador).WithMany(x => x.ParosReportados).HasForeignKey(c => c.IdReportador);
            HasRequired(c => c.Origen).WithMany(x => x.Paros).HasForeignKey(c => c.IdOrigen);
            #endregion

            #region HasMany
            HasMany(c => c.TiemposDeParo).WithRequired(x => x.Paro);
            HasMany(c => c.Asignaciones).WithMany(x => x.Paros).Map(cs =>
            {
                cs.MapLeftKey("IdParo");
                cs.MapRightKey("IdAsignacion");
                cs.ToTable("asignacionesenparo");
            });
            #endregion

            #region HasOptional
            HasOptional(c => c.Mecanico).WithMany(x => x.ParosAsignados).HasForeignKey(c => c.IdMecanico);
            #endregion
        }
    }
}