using PMMX.Modelo.Entidades.Maquinaria;
using System.Data.Entity.ModelConfiguration;

namespace PMMX.Modelo.Map
{
    public class ModuloSeccionMap : EntityTypeConfiguration<ModuloSeccion>
    {
        public ModuloSeccionMap()
        {
            #region Propiedades

            ToTable("moduloseccion");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            HasMany(c => c.NoConformidades).WithRequired(x => x.Seccion).HasForeignKey(c => c.IdSeccion);
            HasMany(c => c.Modulos).WithOptional(x => x.Seccion).HasForeignKey(c => c.IdSeccion);
            HasMany(c => c.Operadores).WithMany(x => x.Secciones).Map(cs =>
            {
                cs.MapLeftKey("IdSeccion");
                cs.MapRightKey("IdOperador");
                cs.ToTable("OperadorSeccion");
            });
            HasMany(c => c.Mecanicos).WithMany(x => x.Secciones).Map(cs =>
            {
                cs.MapLeftKey("IdSeccion");
                cs.MapRightKey("IdMecanico");
                cs.ToTable("MecanicoSeccion");
            });


            #endregion

            #region HasOptional

            #endregion
            
            #region HasRequired

            #endregion
            
        }
    }
}
