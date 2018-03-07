using PMMX.Modelo.Entidades.Defectos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class DefectoMap : EntityTypeConfiguration<Defecto>
    {
        public DefectoMap()
        {
            ToTable("Defectos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdReportador).HasColumnName("IdReporteador");
            Property(c => c.IdOrigen).HasColumnName("IdOrigen");
            Property(c => c.Descripcion).HasColumnName("Descripcion");
            Property(c => c.Activo).HasColumnName("Activo");
            Property(c => c.FechaReporte).HasColumnName("FechaReporte");
            Property(c => c.FechaEstimada).HasColumnName("FechaEstimada");
            Property(c => c.Prioridad).HasColumnName("Prioridad");
            Property(c => c.NotificacionSAP).HasColumnName("NotificacionSAP");

            HasRequired(c => c.Reportador).WithMany(p => p.DefectosReportados).HasForeignKey(c => c.IdReportador);
            HasRequired(c => c.Origen).WithMany(p => p.Defectos).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.Actividades).WithRequired(x => x.Defecto).HasForeignKey(c => c.IdDefecto);

            //HasMany(c => c.FotosPersonales).WithOptional(f => f.Defecto).HasForeignKey(c => c.IdDefecto);
            HasMany(c => c.Fotos).WithMany(x => x.Defectos).Map(cs =>
            {
                cs.MapLeftKey("IdDefecto");
                cs.MapRightKey("IdFoto");
                cs.ToTable("DefectoFoto");
            });
            //HasMany(c => c.Asignaciones).WithMany(x => x.Defectos).Map(cs =>
            //{
            //    cs.MapLeftKey("IdDefecto");
            //    cs.MapRightKey("IdAsignacion");
            //    cs.ToTable("asignacionesendefecto");
            //});
        }

    }
}