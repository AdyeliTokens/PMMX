
using PMMX.Modelo.Entidades.Operaciones;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
{
    public class EstatusMap : EntityTypeConfiguration<Estatus>
    {
        public EstatusMap()
        {
            #region Propiedades
            ToTable("Estatus");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.IdCategoria).HasColumnName("IdCategoria");
            Property(c => c.Color).HasColumnName("Color");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            this.HasMany(c => c.StatusVentana).WithRequired(x => x.Status).HasForeignKey(c => c.IdStatus);
            this.HasMany(c => c.Rechazo).WithRequired(x => x.Estatus).HasForeignKey(c => c.IdStatus);
            this.HasMany(c => c.BitacoraVentana).WithRequired(x => x.Estatus).HasForeignKey(c => c.IdStatus);
            this.HasMany(c => c.BitacoraGembaWalk).WithRequired(x => x.Estatus).HasForeignKey(c => c.IdStatus);

            this.HasMany(c => c.WorkFlowInicial).WithOptional(x => x.EstatusInicial).HasForeignKey(c => c.Inicial);
            this.HasMany(c => c.WorkFlowAnterior).WithOptional(x => x.EstatusAnterior).HasForeignKey(c => c.Anterior);
            this.HasMany(c => c.WorkFlowSiguiente).WithOptional(x => x.EstatusSiguiente).HasForeignKey(c => c.Siguiente);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Categoria).WithMany(x => x.Estatus);
            #endregion
        }
    }
}
