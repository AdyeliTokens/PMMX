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

            this.ToTable("AspNetUsers");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.Email).HasColumnName("Email");
            this.Property(c => c.EmailConfirmed).HasColumnName("EmailConfirmed");
            this.Property(c => c.PasswordHash).HasColumnName("PasswordHash");
            this.Property(c => c.SecurityStamp).HasColumnName("SecurityStamp");
            this.Property(c => c.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(c => c.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            this.Property(c => c.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
            this.Property(c => c.LockOutEndDateUTC).HasColumnName("LockOutEndDateUTC");
            this.Property(c => c.LockOutEnabled).HasColumnName("LockOutEnabled");
            this.Property(c => c.AccessFailedCount).HasColumnName("AccessFailedCount");
            this.Property(c => c.UserName).HasColumnName("UserName");

            #endregion

            #region HasMany
            
            #endregion

        }
    }
}
