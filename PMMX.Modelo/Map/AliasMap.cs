using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
{

    public class AliasMap : EntityTypeConfiguration<Alias>
    {
        public AliasMap()
        {
            #region Propiedades
            ToTable("Alias");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany

            HasMany(c => c.WorkCenters).WithMany(x => x.Alias).Map(cs =>
            {
                cs.MapLeftKey("IdAlias");
                cs.MapRightKey("IdWorkCenter");
                cs.ToTable("AliasWorkCenter");
            });


            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.PersonaQueDioDeAlta).WithMany(x => x.AliasDadosDeAlta).HasForeignKey(c => c.IdPersona);
            #endregion





        }
    }
}
