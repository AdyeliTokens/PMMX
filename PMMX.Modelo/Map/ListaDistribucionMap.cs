using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class ListaDistribucionMap : EntityTypeConfiguration<ListaDistribucion>
    {
        public ListaDistribucionMap()
        {
            #region Propiedades
            ToTable("SubArea");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdArea).HasColumnName("IdArea");
            Property(c => c.IdSubarea).HasColumnName("IdSubarea");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            //HasMany(c => c.Indicadores).WithMany(x => x.Areas).Map(cs =>
            //{
            //    cs.MapLeftKey("IdArea");
            //    cs.MapRightKey("IdIndicador");
            //    cs.ToTable("areasindicadores");
            //});
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Persona).WithMany(x => x.Areas).HasForeignKey(c => c.Persona);
            #endregion





        }
    }
}