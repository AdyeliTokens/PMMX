using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class RequisicionDeDescargaMap : EntityTypeConfiguration<RequisicionDeDescarga>
    {
        public RequisicionDeDescargaMap()
        {
            ToTable("requisiciondedescargas");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Email).HasColumnName("Email");
            Property(c => c.Aprobacion).HasColumnName("Aprobacion");
            Property(c => c.Comentario).HasColumnName("Comentario");
            Property(c => c.Solicitud).HasColumnName("Solicitud");
            Property(c => c.Nombre).HasColumnName("Nombre");
            
        }
    }
}
