using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class RemitentesMap: EntityTypeConfiguration<Remitentes>
    {
        public RemitentesMap()
        {
            this.ToTable("Remitentes");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdPuesto).HasColumnName("IdPuesto");
            this.Property(c => c.IdOrigen).HasColumnName("IdOrigen");
            this.Property(c => c.IdHealthCheck).HasColumnName("IdHealthCheck");
            this.Property(c => c.Activo).HasColumnName("Activo");
            
            this.HasRequired(c => c.Puesto).WithMany(x => x.Remitentes).HasForeignKey(c => c.IdPuesto);
            this.HasRequired(c => c.Origen).WithMany(x => x.Remitentes).HasForeignKey(c => c.IdOrigen);
            this.HasRequired(c => c.HealthCheck).WithMany(x => x.Remitentes).HasForeignKey(c => c.IdHealthCheck);
        }
    }
}
