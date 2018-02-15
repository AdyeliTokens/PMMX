using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class PlanDeProduccionMap : EntityTypeConfiguration<PlanDeProduccion>
    {
        public PlanDeProduccionMap()
        {
            #region Propiedades
            ToTable("PlanDeProduccion");
            HasKey(c => c.Codigo);
            Property(c => c.Codigo).HasColumnName("Codigo");
            Property(c => c.Code_FA).HasColumnName("Code_FA");
            Property(c => c.IdWorkCenter).HasColumnName("IdWorkCenter");
            Property(c => c.Cantidad).HasColumnName("Cantidad");
            Property(c => c.ClaseDeOrden).HasColumnName("ClaseDeOrden");
            Property(c => c.Inicio).HasColumnName("FechaInicial");
            Property(c => c.Fin).HasColumnName("FechaFinal");
            Property(c => c.IdUploader).HasColumnName("IdUploader");
            Property(c => c.FechaSubida).HasColumnName("FachaSubida");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany




            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Uploader).WithMany(x => x.PlanesDeProduccionReportados).HasForeignKey(c => c.IdUploader);
            HasRequired(c => c.WorkCenterEfectivo).WithMany(x => x.PlanesDeProduccion).HasForeignKey(c => c.IdWorkCenter);
            HasRequired(c => c.Marca_FA).WithMany(x => x.PlanesDeProduccion).HasForeignKey(c => c.Code_FA);
            #endregion

        }
    }
}
