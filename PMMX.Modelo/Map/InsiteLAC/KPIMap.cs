using PMMX.Modelo.Entidades.InsiteLAC;
using System.Data.Entity.ModelConfiguration;

namespace PMMX.Modelo.Map.InsiteLAC
{
    public class KPIMap : EntityTypeConfiguration<KPI>
    {
        public KPIMap()
        {
            #region Propiedades
            ToTable("insitelac_kpis");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Description).HasColumnName("Description");
            Property(c => c.YTD).HasColumnName("YTD");
            Property(c => c.Mes_Efectivo).HasColumnName("Mes_Efectivo");
            Property(c => c.Anio_Efectivo).HasColumnName("Anio_Efectivo");
            #endregion

            #region HasMany

            #endregion

            #region HasOptional
            #endregion

            #region HasRequired

            #endregion

        }
    }
}