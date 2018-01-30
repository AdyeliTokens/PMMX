using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class OrigenRespuestaMap : EntityTypeConfiguration<OrigenRespuesta>
    {
        public OrigenRespuestaMap()
        {
            #region Propiedades
            this.ToTable("OrigenRespuesta");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdOrigen).HasColumnName("IdModuloWorkCenter");
            this.Property(c => c.IdOperador).HasColumnName("IdOperador");
            this.Property(c => c.IdEntrevistado).HasColumnName("IdEntrevistado");
            this.Property(c => c.IdSupervisor).HasColumnName("IdSupervisor");
            this.Property(c => c.Fecha).HasColumnName("Fecha");
            this.Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasRequired
            this.HasRequired(c => c.Origen).WithMany(p => p.OrigenRespuestas).HasForeignKey(c => c.IdOrigen);
            this.HasRequired(c => c.Supervisor).WithMany(p => p.Supervisor).HasForeignKey(c => c.IdSupervisor);
            this.HasRequired(c => c.Entrevistado).WithMany(p => p.Encuestado).HasForeignKey(c => c.IdEntrevistado);
            this.HasRequired(c => c.Operador).WithMany(p => p.Operador).HasForeignKey(c => c.IdOperador);
            #endregion

            #region HasMany
            this.HasMany(c => c.Respuestas).WithRequired(p => p.OrigenRespuesta).HasForeignKey(c => c.IdOrigenRespuesta);
            #endregion
            

        }
    }
}