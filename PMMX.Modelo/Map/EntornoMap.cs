using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class EntornoMap : EntityTypeConfiguration<Entorno>
    {
        public EntornoMap()
        {
            this.ToTable("Entorno");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Nombre).HasColumnName("Nombre");
            this.Property(c => c.Activo).HasColumnName("Activo");

            HasMany(c => c.Usuarios).WithMany(x => x.Entornos).Map(cs =>
            {
                cs.MapLeftKey("IdEntorno");
                cs.MapRightKey("IdUsuario");
                cs.ToTable("UsuariosEntornos");
            });
        }
    }
}