using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class UsuariosPorPersonaMap : EntityTypeConfiguration<UsuariosPorPersona>
    {
        public UsuariosPorPersonaMap()
        {
            this.ToTable("personasusuarios");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdAspNetUser).HasColumnName("IdAspNetUser");
            this.Property(c => c.IdPersona).HasColumnName("IdPersona");
            
            this.HasRequired(c => c.Usuario).WithMany(x => x.PersonasConEsteUsuario).HasForeignKey(c => c.IdAspNetUser);
            this.HasRequired(c => c.Persona).WithMany(x => x.Users).HasForeignKey(c => c.IdPersona);

        }
    }
}
