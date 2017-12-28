using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// Clase que contiene el mapeo y navegacion correspondiente a la tabla "FotosPersonales"
    /// </summary>
    public class HorarioMap : EntityTypeConfiguration<Horario>
    {
        /// <summary>
        /// Mapeo de la tabla "FotosPersonales" junto con sus propiedades de navegacion
        /// </summary>
        public HorarioMap()
        {

            #region Pripiedades

            ToTable("Horarios");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Desccripcion).HasColumnName("Desccripcion");
            Property(c => c.Hora_Inicial).HasColumnName("Hora_Inicial");
            Property(c => c.Hora_Final).HasColumnName("Hora_Final");
            Property(c => c.Activo).HasColumnName("Activo");

            #endregion

            #region HasOptional

            #endregion

            #region HasMany
            
            #endregion

            #region HasRequired

            #endregion

        }
    }
}
