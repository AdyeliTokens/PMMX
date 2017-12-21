using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class ActividadEnParoMap : EntityTypeConfiguration<ActividadEnParo>
    {
        public ActividadEnParoMap()
        {
            this.ToTable("ActividadesEnParos");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdPersona).HasColumnName("IdPersona");
            this.Property(c => c.Fecha).HasColumnName("Fecha");

            this.HasRequired(c => c.Ejecutante).WithMany(x => x.ActividadesEnParoRealizadas).HasForeignKey(c => c.IdPersona);
            this.HasRequired(c => c.Paro).WithMany(x => x.ActividadesEnParo).HasForeignKey(c => c.IdParo);
        }

    }
}