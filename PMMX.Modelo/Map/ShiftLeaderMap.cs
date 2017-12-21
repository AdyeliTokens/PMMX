using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class ShiftLeaderMap : EntityTypeConfiguration<ShiftLeaders>
    {
        /// <summary>
        /// 
        /// </summary>
        public ShiftLeaderMap()
        {
            #region Propiedades

            ToTable("ShiftLeaders");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdShiftLeader).HasColumnName("IdShiftLeader");
            Property(c => c.IdCelula).HasColumnName("IdCelula");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            #endregion

            #region HasOptional

            #endregion

            #region HasRequired

            HasRequired(c => c.ShiftLeader).WithMany(x => x.CelulasPorVigilar).HasForeignKey(x => x.IdShiftLeader);
            HasRequired(c => c.BussinesUnit).WithMany(x => x.ShiftLeaders).HasForeignKey(x => x.IdCelula);

            #endregion
        }
    }
}
