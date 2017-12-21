using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class RespuestaMap : EntityTypeConfiguration<Respuesta>
    {
        public RespuestaMap()
        {
            this.ToTable("Respuestas");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdPregunta).HasColumnName("IdPregunta");
            this.Property(c => c.IdOrigenRespuesta).HasColumnName("IdOrigenRespuesta");
            this.Property(c => c.Solucion).HasColumnName("Respuesta");
            this.Property(c => c.Comentario).HasColumnName("Comentario");
            this.Property(c => c.Fecha).HasColumnName("Fecha");
            this.Property(c => c.Activo).HasColumnName("Activo");
            
            this.HasRequired(c => c.Pregunta).WithMany(p => p.Respuestas).HasForeignKey(c => c.IdPregunta);
            this.HasRequired(c => c.OrigenRespuesta).WithMany(p => p.Respuestas).HasForeignKey(c => c.IdOrigenRespuesta);

            this.HasMany(c => c.PreguntasTurno).WithMany(p => p.Respuestas);
        }
    }
}