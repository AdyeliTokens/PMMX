using PMMX.Modelo.Entidades.SeguridadFisica;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class DatosUnidadMap : EntityTypeConfiguration<DatosUnidad>
    {
        public DatosUnidadMap()
        {
            #region Propiedades
            ToTable("DatosUnidad");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.NombreConductor).HasColumnName("NombreConductor");
            Property(c => c.PlacasTractor).HasColumnName("PlacasTractor");
            Property(c => c.NoEcoTractor).HasColumnName("NoEconomicoTractor");
            Property(c => c.NoCaja).HasColumnName("NoCaja");
            Property(c => c.TipoRemolque).HasColumnName("TipoRemolque");
            Property(c => c.PlacasRemolque).HasColumnName("PlacasRemolque");
            Property(c => c.NoEcoRemolque).HasColumnName("NoEconomicoRemolque");
            Property(c => c.IdRegistroUnidad).HasColumnName("IdRegistroUnidad");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(r => r.RegistroUnidad).WithMany(e => e.Datos).HasForeignKey(r=> r.IdRegistroUnidad);
            #endregion         
        }

    }
}
