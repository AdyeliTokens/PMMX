using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{

    public class MarcaMap : EntityTypeConfiguration<Marca>
    {
        public MarcaMap()
        {
            #region Propiedades
            ToTable("Marcas");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.Codigo).HasColumnName("Codigo");
            Property(c => c.PesoPorCigarrillo).HasColumnName("Peso");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            HasMany(c => c.Desperdicios).WithRequired(x => x.MarcaDelCigarrillo).HasForeignKey(c => c.IdMarca);
            #endregion

            #region HasOptional
            #endregion

            #region HasRequired

            #endregion





        }
    }
}
