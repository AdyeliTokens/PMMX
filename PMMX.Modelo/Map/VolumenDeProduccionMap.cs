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
            Property(c => c.Container).HasColumnName("Container");
            Property(c => c.Code_FA).HasColumnName("Code_FA");
            Property(c => c.Source_WH).HasColumnName("Source_WH");
            Property(c => c.Source_Loc).HasColumnName("Source_Loc");
            Property(c => c.Dest_WH).HasColumnName("Dest_WH");
            Property(c => c.Dest_Loc).HasColumnName("Dest_Loc");
            Property(c => c.Old_Qty).HasColumnName("Old_Qty");
            Property(c => c.New_Qty).HasColumnName("New_Qty");
            Property(c => c.UOM).HasColumnName("UOM");
            Property(c => c.SAP_Batch).HasColumnName("SAP_Batch");
            Property(c => c.Fecha).HasColumnName("Fecha");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            Property(c => c.IdWorkCenter).HasColumnName("IdWorkCenter");
            

            #endregion

            #region HasMany

            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.Reportante).WithMany(x => x.VolumenReportado).HasForeignKey(c => c.IdPersona);
            HasRequired(c => c.WorkCenter).WithMany(x => x.VolumenesDeProduccion).HasForeignKey(c => c.IdWorkCenter);
            HasRequired(c => c.MarcaDelCigarrillo).WithMany(x => x.VolumenesProducidos).HasForeignKey(c => c.Code_FA);

            #endregion






        }
    }
}
