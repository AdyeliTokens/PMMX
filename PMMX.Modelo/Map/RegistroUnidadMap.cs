using PMMX.Modelo.Entidades.SeguridadFisica;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class RegistroUnidadMap : EntityTypeConfiguration<RegistroUnidad>
    {
        public RegistroUnidadMap()
        {
            #region Propiedades
            ToTable("RegistroUnidad");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Empresa).HasColumnName("Empresa");
            Property(c => c.Asunto).HasColumnName("Asunto");
            Property(c => c.NombreAutoriza).HasColumnName("NombreAutoriza");
            Property(c => c.NoGafette).HasColumnName("NoGafette");
            Property(c => c.IdFormato).HasColumnName("IdFormato");
            #endregion

            #region HasMany
            this.HasMany(m => m.Datos).WithRequired(r => r.RegistroUnidad).HasForeignKey(f => f.IdRegistroUnidad);
            this.HasMany(m => m.Bitacora).WithRequired(r => r.RegistroUnidad).HasForeignKey(f => f.IdRegistroUnidad);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(r => r.Formato).WithMany(m => m.RegistrosUnidad).HasForeignKey(f => f.IdFormato);
            #endregion         
        }

    }
}
