using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class AspNetRolesMap : EntityTypeConfiguration<AspNetRoles>
    {
        public AspNetRolesMap()
        {
            #region Propiedades

            ToTable("aspnetroles");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Name).HasColumnName("Name");

            #endregion

            #region HasMany
            HasMany(c => c.Users).WithMany(x => x.Roles).Map(cs =>
            {
                cs.MapLeftKey("RoleId");
                cs.MapRightKey("UserId");
                cs.ToTable("aspnetuserroles");
            });
            #endregion

        }
    }
}
