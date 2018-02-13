using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.GembaWalks;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class EventoOrigenMap : EntityTypeConfiguration<EventoOrigen>
    {
        public EventoOrigenMap()
        {
            #region Propiedades
            ToTable("EventoOrigen");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdEvento).HasColumnName("IdEvento");
            Property(c => c.IdOrigen).HasColumnName("IdOrigen");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired
            this.HasRequired(eo => eo.Evento).WithMany(e => e.EventoOrigen);
            this.HasRequired(eo => eo.Origen).WithMany(e => e.EventoOrigen);
            #endregion

        }
    }
}