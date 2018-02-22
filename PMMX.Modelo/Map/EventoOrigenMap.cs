using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
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