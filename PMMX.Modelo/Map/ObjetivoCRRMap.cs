using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    
    public class ObjetivoCRRMap : EntityTypeConfiguration<ObjetivoCRR>
    {
        public ObjetivoCRRMap()
        {
            #region Propiedades
            ToTable("objetivoCRR");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdWorkCenter).HasColumnName("IdWorkCenter");
            Property(c => c.Objetivo).HasColumnName("Objetivo");
            Property(c => c.FechaInicial).HasColumnName("FechaInicial");
            #endregion

            #region HasMany

            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            HasRequired(c => c.WorkCenter).WithMany(x => x.ObjetivosCRR).HasForeignKey(c => c.IdWorkCenter);
            #endregion





        }
    }
}
