using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class FormatoMap : EntityTypeConfiguration<Formato>
    {
        public FormatoMap()
        {
            #region Propiedades
            ToTable("Formato");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Codigo).HasColumnName("Codigo");
            Property(c => c.Descripcion).HasColumnName("Descripcion");
            Property(c => c.FechaEfectividad).HasColumnName("FechaEfectividad");
            Property(c => c.TiempoRetencion).HasColumnName("TiempoRetencion");
            Property(c => c.Version).HasColumnName("Version");
            #endregion

            #region HasMany
            this.HasMany(m => m.RegistrosUnidad).WithRequired(r => r.Formato).HasForeignKey(f => f.IdFormato);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            #endregion         
        }

    }
}
