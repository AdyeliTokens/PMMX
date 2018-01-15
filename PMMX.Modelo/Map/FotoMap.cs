using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// Clase que contiene el mapeo y navegacion correspondiente a la tabla "FotosPersonales"
    /// </summary>
    public class FotoMap : EntityTypeConfiguration<Foto>
    {
        /// <summary>
        /// Mapeo de la tabla "FotosPersonales" junto con sus propiedades de navegacion
        /// </summary>
        public FotoMap()
        {

            #region Pripiedades

            ToTable("Fotos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdContribuidor).HasColumnName("IdPersona");
            Property(c => c.IdMantenimiento).HasColumnName("IdMantenimiento");
            Property(c => c.Nombre).HasColumnName("Name");
            Property(c => c.Path).HasColumnName("Path");
            Property(c => c.Fecha).HasColumnName("Fecha");

            #endregion

            #region HasOptional
            
            HasOptional(c => c.Mantenimiento).WithMany(d => d.Fotos).HasForeignKey(c => c.IdMantenimiento);
            HasOptional(c => c.Contribuidor).WithMany(d => d.FotosSubidas).HasForeignKey(c => c.IdContribuidor);

            #endregion

            #region HasMany

            HasMany(c => c.Origenes).WithMany(x => x.Fotos).Map(cs =>
            {
                cs.MapLeftKey("IdFoto");
                cs.MapRightKey("IdOrigen");
                cs.ToTable("OrigenFoto");
            });
            HasMany(c => c.Defectos).WithMany(x => x.Fotos).Map(cs =>
            {
                cs.MapLeftKey("IdFoto");
                cs.MapRightKey("IdDefecto");
                cs.ToTable("DefectoFoto");
            });
            HasMany(c => c.Personas).WithMany(x => x.FotosPersonales).Map(cs =>
            {
                cs.MapLeftKey("IdFoto");
                cs.MapRightKey("IdPersona");
                cs.ToTable("PersonasFotos");
            });
            HasMany(c => c.JustDoIts).WithMany(x => x.Fotos).Map(cs =>
            {
                cs.MapLeftKey("IdFoto");
                cs.MapRightKey("IdJustDoIt");
                cs.ToTable("JustDoItFotos");
            });

            #endregion

            #region HasRequired

            #endregion

        }
    }
}
