using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class ClasificacionHallazgoMap : EntityTypeConfiguration<ClasificacionHallazgo>
    {
        public ClasificacionHallazgoMap()
        {
            #region Propiedades
            this.ToTable("ClasificacionHallazgo");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.IdSubCategoria).HasColumnName("IdSubCategoria");
            this.Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasRequired
            this.HasRequired(c => c.SubCategoria).WithMany(p => p.ClasificacionHallazgo);
            #endregion

            #region HasMany
            this.HasMany(c => c.GembaWalks).WithMany(x => x.ClasificacionHallazgos).Map(cs =>
            {
                cs.MapLeftKey("IdClasificacion");
                cs.MapRightKey("IdGembaWalk");
                cs.ToTable("ClasificacionGembaWalk");
            });
            #endregion
        }
    }
}
