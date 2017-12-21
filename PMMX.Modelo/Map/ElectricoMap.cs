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
    public class ElectricosMap : EntityTypeConfiguration<Electricos>
    {
        /// <summary>
        /// 
        /// </summary>
        public ElectricosMap()
        {
            #region Propiedades

            ToTable("Electricos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdElectrico).HasColumnName("IdElectrico");
            Property(c => c.IdBusinessUnit).HasColumnName("IdBusinessUnit");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            HasMany(c => c.Secciones).WithMany(x => x.Electricos).Map(cs =>
            {
                cs.MapLeftKey("IdElectrico");
                cs.MapRightKey("IdSeccion");
                cs.ToTable("ElectricoSeccion");
            });

            #endregion

            #region HasOptional

            #endregion

            #region HasRequired

            HasRequired(c => c.Electrico_Persona).WithMany(x => x.BusinessUnitParaReparar_Electrico).HasForeignKey(c => c.IdElectrico);
            HasRequired(c => c.BusinessUnit).WithMany(x => x.Electricos).HasForeignKey(c => c.IdBusinessUnit);

            #endregion

        }
    }
}
