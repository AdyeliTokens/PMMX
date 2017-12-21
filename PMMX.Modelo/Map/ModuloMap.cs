using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuloMap : EntityTypeConfiguration<Modulo>
    {
        /// <summary>
        /// 
        /// </summary>
        public ModuloMap()
        {
            #region Propiedades

            ToTable("Modulos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.NombreCorto).HasColumnName("NombreCorto");
            Property(c => c.IdSeccion).HasColumnName("IdSeccion");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasMany

            HasMany(c => c.Origenes).WithOptional(x => x.Modulo).HasForeignKey(c => c.IdModulo);

            #endregion

            #region HasOptional

            HasOptional(c => c.Seccion).WithMany(x => x.Modulos).HasForeignKey(c => c.IdSeccion);

            #endregion

            #region HasRequired
            #endregion

        }
    }
}