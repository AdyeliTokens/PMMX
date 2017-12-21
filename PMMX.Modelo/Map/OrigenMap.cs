using PMMX.Modelo.Entidades.Maquinaria;
using System.Data.Entity.ModelConfiguration;

namespace PMMX.Modelo.Map
{
    /// <summary>
    /// Clase que contiene el mapeo y navegacion correspondiente a la tabla "ModuloWorkCenter"
    /// </summary>
    public class OrigenMap : EntityTypeConfiguration<Origen>
    {
        /// <summary>
        /// Mapeo de la tabla "ModuloWorkCenter" junto con sus propiedades de navegacion
        /// </summary>
        public OrigenMap()
        {
            #region Propiedades

            ToTable("ModuloWorkCenter");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdModulo).HasColumnName("IdModulo");
            Property(c => c.IdWorkCenter).HasColumnName("IdWorkCenter");
            Property(c => c.Foto).HasColumnName("Foto");
            Property(c => c.Orden).HasColumnName("Orden");

            #endregion

            #region HasMany

            //HasMany(c => c.Preguntas).WithRequired(x => x.Origen).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.PreguntaTurno).WithRequired(x => x.Origen).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.OrigenRespuestas).WithRequired(x => x.Origen).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.Remitentes).WithRequired(p => p.Origen).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.Eventos).WithRequired(x => x.Origen).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.JustDoIt).WithRequired(x => x.Origen).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.Mantenimientos).WithRequired(x => x.Origen).HasForeignKey(c => c.IdOrigen);
            HasMany(c => c.Fotos).WithMany(x => x.Origenes).Map(cs =>
            {
                cs.MapLeftKey("IdOrigen");
                cs.MapRightKey("IdFoto");
                cs.ToTable("OrigenFoto");
            });

            #endregion

            #region HasOptional

            HasOptional(c => c.Modulo).WithMany(x => x.Origenes).HasForeignKey(c => c.IdModulo);

            #endregion

            #region HasRequired

            HasRequired(c => c.WorkCenter).WithMany(x => x.Origenes).HasForeignKey(c => c.IdWorkCenter);

            #endregion

        }
    }
}