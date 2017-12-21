using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{

    public class TiempoDeParoMap : EntityTypeConfiguration<TiempoDeParo>
    {
        public TiempoDeParoMap()
        {
            this.ToTable("TiempoDeParos");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdParo).HasColumnName("IdParo");
            this.Property(c => c.Inicio).HasColumnName("Inicio");
            this.Property(c => c.Fin).HasColumnName("Fin");

            this.HasRequired(c => c.Paro).WithMany(x => x.TiemposDeParo).HasForeignKey(c => c.IdParo);
            

        }
    }
}
