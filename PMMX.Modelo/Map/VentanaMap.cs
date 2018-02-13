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
            Property(c => c.PO).HasColumnName("PO");
            Property(c => c.IdOperacion).HasColumnName("IdOperacion");
            Property(c => c.IdSubCategoria).HasColumnName("IdSubCategoria");
            Property(c => c.IdCarrier).HasColumnName("IdCarrier");
            Property(c => c.NombreCarrier).HasColumnName("NombreCarrier");
            Property(c => c.IdDestino).HasColumnName("IdDestino");
            Property(c => c.IdEvento).HasColumnName("IdEvento");
            Property(c => c.IdProcedencia).HasColumnName("IdProcedencia");
            Property(c => c.IdProveedor).HasColumnName("IdProveedor");
            Property(c => c.Recurso).HasColumnName("Recurso");            
            Property(c => c.Cantidad).HasColumnName("Cantidad");
            Property(c => c.NumeroEconomico).HasColumnName("NumeroEconomico");
            Property(c => c.NumeroPlaca).HasColumnName("NumeroPlaca");
            Property(c => c.EconomicoRemolque).HasColumnName("EconomicoRemolque");
            Property(c => c.PlacaRemolque).HasColumnName("PlacaRemolque");
            Property(c => c.ModeloContenedor).HasColumnName("ModeloContenedor");
            Property(c => c.ColorContenedor).HasColumnName("ColorContenedor");
            Property(c => c.Sellos).HasColumnName("Sellos");
            Property(c => c.TipoUnidad).HasColumnName("TipoUnidad");
            Property(c => c.Dimension).HasColumnName("Dimension");
            Property(c => c.Temperatura).HasColumnName("Temperatura");
            Property(c => c.Conductor).HasColumnName("Conductor");
            Property(c => c.MovilConductor).HasColumnName("MovilConductor");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            this.HasMany(c => c.StatusVentana).WithRequired(x => x.Ventana).HasForeignKey(x => x.IdVentana);
            this.HasMany(c => c.BitacoraVentana).WithRequired(x => x.Ventana).HasForeignKey(x => x.IdVentana);
            #endregion

            #region HasOptional

            #endregion

            #region HasRequired
            this.HasRequired(c => c.Evento).WithMany(x => x.Ventanas);
            this.HasRequired(c => c.Carrier).WithMany(x => x.Ventanas);
            this.HasRequired(c => c.SubCategoria).WithMany(x => x.Ventanas);
            this.HasRequired(c => c.TipoOperacion).WithMany(x => x.Ventanas);
            #endregion
        }
    }
}
