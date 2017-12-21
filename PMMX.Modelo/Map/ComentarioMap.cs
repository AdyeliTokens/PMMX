using PMMX.Modelo.Entidades.Defectos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class ComentarioMap : EntityTypeConfiguration<Comentario>
    {
        public ComentarioMap()
        {
            this.ToTable("Comentarios");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdDefecto).HasColumnName("IdDefecto");
            this.Property(c => c.IdComentador).HasColumnName("IdComentador");
            this.Property(c => c.Opinion).HasColumnName("Comentario");
            this.Property(c => c.Fecha).HasColumnName("Fecha");



            this.HasRequired(c => c.Defecto).WithMany(x => x.Comentarios).HasForeignKey(c => c.IdDefecto);
            this.HasRequired(c => c.Comentador).WithMany(x => x.Comentarios).HasForeignKey(c => c.IdComentador);
        }
    }
}
