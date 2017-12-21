using PMMX.Modelo.Entidades.Maquinaria;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class BussinesUnitMap : EntityTypeConfiguration<BussinesUnit>
    {
        /// <summary>
        /// 
        /// </summary>
        public BussinesUnitMap()
        {
            #region Propiedades

            ToTable("BussinesUnit");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            Property(c => c.IdArea).HasColumnName("IdArea");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            HasMany(c => c.Mecanicos).WithRequired(x => x.BusinessUnit).HasForeignKey(c => c.IdBusinessUnit);
            HasMany(c => c.WorkCenters).WithRequired(x => x.BussinesUnit).HasForeignKey(c => c.IdBussinesUnit);
            HasMany(c => c.ShiftLeaders).WithRequired(x => x.BussinesUnit).HasForeignKey(c => c.IdCelula);

            #endregion

            #region HasOptional

            #endregion

            #region HasRequired

            HasRequired(c => c.Responsable).WithMany(x => x.BussinesUnitsDondeEsResponsable).HasForeignKey(c => c.IdResponsable);
            HasRequired(c => c.Area).WithMany(x => x.BussinessUnits).HasForeignKey(c => c.IdArea);


            #endregion

        }
    }
}