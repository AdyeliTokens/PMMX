using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{

    public class VolumenDeProduccionMap : EntityTypeConfiguration<VolumenDeProduccion>
    {
        public VolumenDeProduccionMap()
        {
            #region Propiedades

            ToTable("VolumenDeProduccion");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Cantidad).HasColumnName("Cantidad");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.IdWorkCenter).HasColumnName("IdWorkCenter");
            Property(c => c.IdMarca).HasColumnName("IdMarca");

            #endregion

            #region HasMany

            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Reportante).WithMany(x => x.VolumenReportado).HasForeignKey(c => c.IdPersona);
            HasRequired(c => c.WorkCenter).WithMany(x => x.VolumenesDeProduccion).HasForeignKey(c => c.IdWorkCenter);
            HasRequired(c => c.MarcaDelCigarrillo).WithMany(x => x.VolumenesProducidos).HasForeignKey(c => c.IdMarca);

            #endregion






        }
    }
}
