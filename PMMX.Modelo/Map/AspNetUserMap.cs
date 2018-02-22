using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    public class AspNetUserMap : EntityTypeConfiguration<AspNetUser>
    {
        public AspNetUserMap()
        {
            #region Propiedades

            ToTable("AspNetUsers");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Email).HasColumnName("Email");
            Property(c => c.EmailConfirmed).HasColumnName("EmailConfirmed");
            Property(c => c.PasswordHash).HasColumnName("PasswordHash");
            Property(c => c.SecurityStamp).HasColumnName("SecurityStamp");
            Property(c => c.PhoneNumber).HasColumnName("PhoneNumber");
            Property(c => c.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            Property(c => c.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
            Property(c => c.LockOutEndDateUTC).HasColumnName("LockOutEndDateUTC");
            Property(c => c.LockOutEnabled).HasColumnName("LockOutEnabled");
            Property(c => c.AccessFailedCount).HasColumnName("AccessFailedCount");
            Property(c => c.UserName).HasColumnName("UserName");

            #endregion

            #region HasMany
            HasMany(c => c.Roles).WithMany(x => x.Users).Map(cs =>
            {
                cs.MapLeftKey("UserId");
                cs.MapRightKey("RoleId");
                cs.ToTable("aspnetuserroles");
            });
            #endregion

        }
    }
}
