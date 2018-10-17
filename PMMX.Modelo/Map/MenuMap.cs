using PMMX.Modelo.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades;
using System.Data.Entity.ModelConfiguration;


namespace PMMX.Modelo.Map
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            #region Properties
            ToTable("Menu");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Menu");
            Property(c => c.SubMenu).HasColumnName("SubMenu");
            Property(c => c.Programa).HasColumnName("Programa");
            Property(c => c.Ruta).HasColumnName("Ruta");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasRequired
            #endregion

            #region HasMany
            HasMany(c => c.Personas).WithMany(x => x.Menu).Map(cs =>
            {
                cs.MapLeftKey("IdMenu");
                cs.MapRightKey("IdPersona");
                cs.ToTable("MenuPersona");
            });
            #endregion

            #region HasOptional
            #endregion
        }

    }
}
