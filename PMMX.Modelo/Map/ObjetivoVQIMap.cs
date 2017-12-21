using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class ObjetivoVQIMap : EntityTypeConfiguration<ObjetivoVQI>
    {
        public ObjetivoVQIMap()
        {
            #region Propiedades
            ToTable("objetivovqi");
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
            HasRequired(c => c.WorkCenter).WithMany(x => x.ObjetivosVQI).HasForeignKey(c => c.IdWorkCenter);
            #endregion





        }
    }
}
