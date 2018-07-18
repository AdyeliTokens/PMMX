using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class SubAreaMap : EntityTypeConfiguration<SubArea>
    {
        public SubAreaMap()
        {
            #region Propiedades
            ToTable("SubArea");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdArea).HasColumnName("IdArea");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            HasMany(c => c.ListaDistribucion).WithRequired(x => x.SubArea).HasForeignKey(c => c.IdSubarea);
            HasMany(c=> c.WorkFlows).WithRequired(x => x.SubArea).HasForeignKey(c => c.IdSubArea);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Area).WithMany(x => x.SubAreas).HasForeignKey(c => c.IdArea);
            HasRequired(c => c.Responsable).WithMany(x => x.SubArea).HasForeignKey(c => c.IdResponsable);
            #endregion
            
        }
    }
}