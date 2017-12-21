using PMMX.Modelo;
using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("users");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdPersona).HasColumnName("IdPersona");
            //Property(c => c.IdEntorno).HasColumnName("IdEntorno");
            Property(c => c.UserName).HasColumnName("UserName");
            Property(c => c.Email).HasColumnName("Email");
            Property(c => c.Password).HasColumnName("Password");
            Property(c => c.Activo).HasColumnName("Activo");



            HasRequired(c => c.Persona).WithMany(p => p.Usuarios).HasForeignKey(c => c.IdPersona);
            //HasRequired(c => c.Entorno).WithMany(p => p.Usuarios).HasForeignKey(c => c.IdEntorno);

            HasMany(c => c.Entornos).WithMany(x => x.Usuarios).Map(cs =>
            {
                cs.MapLeftKey("IdUsuario");
                cs.MapRightKey("IdEntorno");
                cs.ToTable("UsuariosEntornos");
            });

        }
    }
}