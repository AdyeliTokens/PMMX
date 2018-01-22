using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class PreguntaMap : EntityTypeConfiguration<Pregunta>
    {
        public PreguntaMap()
        {
            #region Propiedades
            this.ToTable("Preguntas");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdGrupo).HasColumnName("IdGrupo");
            this.Property(c => c.Interrogante).HasColumnName("Pregunta");
            this.Property(c => c.Anexo1).HasColumnName("Anexo1");
            this.Property(c => c.Anexo2).HasColumnName("Anexo2");
            this.Property(c => c.Tipo).HasColumnName("Tipo");
            this.Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region Navegacion
            this.HasRequired(c => c.GrupoPreguntas).WithMany(x => x.Preguntas).HasForeignKey(c => c.IdGrupo);
            this.HasMany(c => c.Dias).WithMany(x => x.Preguntas);
            this.HasMany(c => c.Turnos).WithMany(x => x.Preguntas);
            this.HasMany(c => c.Respuestas).WithRequired(x => x.Pregunta);
            #endregion
        }
    }
}