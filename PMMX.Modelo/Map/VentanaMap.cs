using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class VentanaMap : EntityTypeConfiguration<Ventana>
    {
        public VentanaMap()
        {
            #region Propiedades
            ToTable("Ventana");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdCarrier).HasColumnName("IdCarrier");
            Property(c => c.IdDestino).HasColumnName("IdDestino");
            Property(c => c.IdEvento).HasColumnName("IdEvento");
            Property(c => c.IdProcedencia).HasColumnName("IdProcedencia");
            Property(c => c.IdProveedor).HasColumnName("IdProveedor");
            Property(c => c.Recurso).HasColumnName("Recurso");
            Property(c => c.PO).HasColumnName("PO");
            Property(c => c.Cantidad).HasColumnName("Cantidad");
            Property(c => c.NumeroEconomico).HasColumnName("NumeroEconomico");
            Property(c => c.NumeroPlaca).HasColumnName("NumeroPlaca");
            Property(c => c.TipoUnidad).HasColumnName("TipoUnidad");
            Property(c => c.Dimension).HasColumnName("Dimension");
            Property(c => c.Temperatura).HasColumnName("Temperatura");
            Property(c => c.Conductor).HasColumnName("Conductor");
            Property(c => c.MovilConductor).HasColumnName("MovilConductor");
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
