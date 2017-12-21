using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class AsignacionMap : EntityTypeConfiguration<Asignacion>
    {
        /// <summary>
        /// 
        /// </summary>
        public AsignacionMap()
        {
            #region Propiedades
            ToTable("asignacion");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdAsignado).HasColumnName("IdAsignado");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            HasMany(c => c.Defectos).WithMany(x => x.Asignaciones).Map(cs =>
            {
                cs.MapLeftKey("IdAsignacion");
                cs.MapRightKey("IdDefecto");
                cs.ToTable("asignacionesendefecto");
            });
            HasMany(c => c.Paros).WithMany(x => x.Asignaciones).Map(cs =>
            {
                cs.MapLeftKey("IdAsignacion");
                cs.MapRightKey("IdParo");
                cs.ToTable("asignacionesenparo");
            });
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Asignado).WithMany(x => x.Asignaciones).HasForeignKey(c => c.IdAsignado);
            #endregion
        }
    }
}
