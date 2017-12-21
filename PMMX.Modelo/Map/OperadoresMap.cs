using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class OperadoresMap : EntityTypeConfiguration<Operadores>
    {
        /// <summary>
        /// 
        /// </summary>
        public OperadoresMap()
        {
            #region Propiedades

            ToTable("Operadores");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdOperador).HasColumnName("IdOperador");
            Property(c => c.IdWorkCenter).HasColumnName("IdWorkCenter");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            HasMany(c => c.Secciones).WithMany(x => x.Operadores).Map(cs =>
            {
                cs.MapLeftKey("IdOperador");
                cs.MapRightKey("IdSeccion");
                cs.ToTable("OperadorSeccion");
            });

            #endregion

            #region HasOptional
            #endregion

            #region HasRequired

            HasRequired(c => c.Operador).WithMany(x => x.WorkCenterParaOperar).HasForeignKey(c => c.IdOperador);
            HasRequired(c => c.WorkCenter).WithMany(x => x.Operadores).HasForeignKey(c => c.IdWorkCenter);

            #endregion
            
        }
    }
}
