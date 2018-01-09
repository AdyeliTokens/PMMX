using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class BitacoraVentanaMap : EntityTypeConfiguration<BitacoraVentana>
    {
        public BitacoraVentanaMap()
        {
            #region Propiedades
            ToTable("BitacoraVentana");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdActividadVentana).HasColumnName("IdActividadVentana");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.Comentarios).HasColumnName("Comentarios");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            #endregion
        }
    }
}
