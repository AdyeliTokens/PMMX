using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
{

    public class MarcaMap : EntityTypeConfiguration<Marca>
    {
        public MarcaMap()
        {
            #region Propiedades
            ToTable("Marcas");
            HasKey(c => c.Code_FA);
            Property(c => c.Code_FA).HasColumnName("Codigo_FA");
            Property(c => c.Descripcion).HasColumnName("Descripcion");
            Property(c => c.Codigo_Cigarrillo).HasColumnName("Codigo_Cigarrillo");
            Property(c => c.PesoPorCigarrillo).HasColumnName("PesoPorCigarrillo");
            Property(c => c.PesoTabacco).HasColumnName("PesoTabacco");
            Property(c => c.FechaDeAlta).HasColumnName("FechaDeAlta");
            Property(c => c.IdPersonaQueDioDeAlta).HasColumnName("IdPersonaQueDioDeAlta");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            HasMany(c => c.Desperdicios).WithRequired(x => x.MarcaDelCigarrillo).HasForeignKey(c => c.Code_FA);
            HasMany(c => c.PlanesDeProduccion).WithRequired(x => x.Marca_FA).HasForeignKey(c => c.Code_FA);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired

            HasRequired(x => x.PersonaQueDioDeAlta).WithMany(y => y.MarcasDadasDeAlta).HasForeignKey(x => x.IdPersonaQueDioDeAlta);
            
            #endregion





        }
    }
}
