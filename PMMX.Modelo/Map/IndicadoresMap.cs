using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class IndicadoresMap : EntityTypeConfiguration<Indicador>
    {
        public IndicadoresMap()
        {
            #region Propiedades

            ToTable("Indicadores");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            HasMany(c => c.Areas).WithMany(x => x.Indicadores).Map(cs =>
            {
                cs.MapLeftKey("IdIndicador");
                cs.MapRightKey("IdArea");
                cs.ToTable("areasindicadores");
            });

            #endregion

            #region HasOptional

            #endregion

            #region HasRequired

            #endregion
            
        }
    }
}
