using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class AreaMap : EntityTypeConfiguration<Area>
    {
        public AreaMap()
        {
            #region Propiedades
            ToTable("Areas");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            HasMany(c => c.BussinessUnits).WithRequired(x => x.Area);
            HasMany(c => c.Indicadores).WithMany(x => x.Areas).Map(cs =>
            {
                cs.MapLeftKey("IdArea");
                cs.MapRightKey("IdIndicador");
                cs.ToTable("areasindicadores");
            });
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Responsable).WithMany(x => x.Areas).HasForeignKey(c => c.IdResponsable);
            #endregion





        }
    }
}