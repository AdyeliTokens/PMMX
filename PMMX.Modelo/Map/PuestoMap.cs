using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class PuestoMap : EntityTypeConfiguration<Puesto>
    {
        public PuestoMap()
        {
            this.ToTable("Puestos");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasMany(c => c.Personas).WithRequired(p => p.Puesto).HasForeignKey(c => c.IdPuesto);
            this.HasMany(c => c.Remitentes).WithRequired(p => p.Puesto).HasForeignKey(c => c.IdPuesto);
        }
    }
}