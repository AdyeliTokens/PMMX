using PMMX.Modelo.Entidades.Maquinaria;
using System.Data.Entity.ModelConfiguration;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// Clase que contiene el mapeo y navegacion correspondiente a la tabla "WorkCenters"
    /// </summary>
    public class WorkCenterMap : EntityTypeConfiguration<WorkCenter>
    {
        /// <summary>
        /// Mapeo de la tabla "WorkCenters" junto con sus propiedades de navegacion
        /// </summary>
        public WorkCenterMap()
        {
            ToTable("WorkCenters");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdBussinesUnit).HasColumnName("IdBussinesUnit");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            Property(c => c.Activo).HasColumnName("Activo");

            HasRequired(c => c.Responsable).WithMany(x => x.WorkCentersDondeEsResponsable).HasForeignKey(c => c.IdResponsable);
            HasRequired(c => c.BussinesUnit).WithMany(x => x.WorkCenters).HasForeignKey(c => c.IdBussinesUnit);


            HasMany(c => c.Operadores).WithRequired(x => x.WorkCenter).HasForeignKey(c => c.IdWorkCenter);
            HasMany(c => c.Origenes).WithRequired(x => x.WorkCenter).HasForeignKey(c => c.IdWorkCenter);
            HasMany(c => c.PlanesDeProduccion).WithRequired(x => x.WorkCenterEfectivo).HasForeignKey(c => c.IdWorkCenter);
            HasMany(c => c.Alias).WithMany(x => x.WorkCenters).Map(cs =>
            {
                cs.MapLeftKey("IdWorkCenter");
                cs.MapRightKey("IdAlias");
                cs.ToTable("AliasWorkCenter");
            });
            HasMany(c => c.Formatos).WithMany(x => x.WorkCenters);
            
        }
    }
}