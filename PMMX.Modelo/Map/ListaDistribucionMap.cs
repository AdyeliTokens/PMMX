using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
{
    public class ListaDistribucionMap : EntityTypeConfiguration<ListaDistribucion>
    {
        public ListaDistribucionMap()
        {
            #region Propiedades
            ToTable("ListaDistribucion");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdSubarea).HasColumnName("IdSubarea");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(c => c.SubArea).WithMany(x => x.ListaDistribucion);
            this.HasRequired(c => c.Remitente).WithMany(x => x.ListaDistribucion);
            this.HasRequired(c => c.Proveedor).WithMany(x => x.ListaDistribucion);
            #endregion





        }
    }
}