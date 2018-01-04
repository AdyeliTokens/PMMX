using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.JustDoIts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class EventoResponsableMap : EntityTypeConfiguration<EventoResponsable>
    {
        public EventoResponsableMap()
        {
            #region Propiedades
            ToTable("EventoResponsable");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdEvento).HasColumnName("IdEvento");
            Property(c => c.IdResponsable).HasColumnName("IdResponsable");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(er => er.Evento).WithMany(e => e.EventoResponsable);
            this.HasRequired(er => er.Responsable).WithMany(e => e.EventoResponsable);
            #endregion

        }
    }
}
