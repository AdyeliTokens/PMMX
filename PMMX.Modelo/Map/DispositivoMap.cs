using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class DispositivoMap : EntityTypeConfiguration<Dispositivo>
    {
        public DispositivoMap()
        {
            this.ToTable("Dispositivos");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Llave).HasColumnName("Llave");
            this.Property(c => c.IdPersona).HasColumnName("IdPersona");
            this.Property(c => c.Activo).HasColumnName("Activo");

            this.HasRequired(c => c.Propietario).WithMany(x => x.Dispositivos).HasForeignKey(c => c.IdPersona);

        }

    }
}
