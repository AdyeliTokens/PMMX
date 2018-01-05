using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    
    public class DesperdicioMap : EntityTypeConfiguration<Desperdicio>
    {
        public DesperdicioMap()
        {
            #region Propiedades

            ToTable("Desperdicios");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Cantidad).HasColumnName("Cantidad");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.IdWorkCenter).HasColumnName("IdWorkCenter");
            Property(c => c.IdSeccion).HasColumnName("IdSeccion");
            Property(c => c.IdMarca).HasColumnName("IdMarca");

            #endregion

            #region HasMany

            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Reportante).WithMany(x => x.DesperdicioReportado).HasForeignKey(c => c.IdPersona);
            HasRequired(c => c.Seccion).WithMany(x => x.Desperdicios).HasForeignKey(c => c.IdSeccion);
            HasRequired(c => c.WorkCenter).WithMany(x => x.Desperdicios).HasForeignKey(c => c.IdWorkCenter);
            HasRequired(c => c.MarcaDelCigarrillo).WithMany(x => x.Desperdicios).HasForeignKey(c => c.IdMarca);

            #endregion






        }
    }
}
