using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    

    /// <summary>
    /// 
    /// </summary>
    public class PesadorMap : EntityTypeConfiguration<Pesador>
    {
        /// <summary>
        /// 
        /// </summary>
        public PesadorMap()
        {
            #region Propiedades

            ToTable("pesadores");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdPesador).HasColumnName("IdPesador");
            Property(c => c.IdArea).HasColumnName("IdArea");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            #endregion

            #region HasOptional

            #endregion

            #region HasRequired

            HasRequired(c => c.Operador_Pesador).WithMany(x => x.Pesadores).HasForeignKey(x => x.IdPesador);
            HasRequired(c => c.Area).WithMany(x => x.Pesadores).HasForeignKey(x => x.IdArea);

            #endregion
        }
    }
}
