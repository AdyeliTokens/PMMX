using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class MecanicoMap : EntityTypeConfiguration<Mecanicos>
    {
        /// <summary>
        /// 
        /// </summary>
        public MecanicoMap()
        {
            #region Propiedades

            ToTable("Mecanicos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdMecanico).HasColumnName("IdMecanico");
            Property(c => c.IdBusinessUnit).HasColumnName("IdBusinessUnit");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            HasMany(c => c.Secciones).WithMany(x => x.Mecanicos).Map(cs =>
            {
                cs.MapLeftKey("IdMecanico");
                cs.MapRightKey("IdSeccion");
                cs.ToTable("MecanicoSeccion");
            });

            #endregion

            #region HasOptional

            #endregion

            #region HasRequired

            HasRequired(c => c.Mecanico).WithMany(x => x.BusinessUnitParaReparar_Mecanico).HasForeignKey(c => c.IdMecanico);
            HasRequired(c => c.BusinessUnit).WithMany(x => x.Mecanicos).HasForeignKey(c => c.IdBusinessUnit);

            #endregion
            
        }
    }
}
