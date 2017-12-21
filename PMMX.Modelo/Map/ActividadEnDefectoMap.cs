using PMMX.Modelo.Entidades.Defectos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class ActividadEnDefectoMap : EntityTypeConfiguration<ActividadEnDefecto>
    {
        public ActividadEnDefectoMap()
        {
            this.ToTable("ActividadesEnDefectos");
            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasColumnName("Id");
            this.Property(c => c.IdDefecto).HasColumnName("IdDefecto");
            this.Property(c => c.IdEjecutante).HasColumnName("IdEjecutante");
            this.Property(c => c.Descripcion).HasColumnName("Descripcion");
            this.Property(c => c.Fecha).HasColumnName("Fecha");



            this.HasRequired(c => c.Defecto).WithMany(x => x.Actividades).HasForeignKey( c => c.IdDefecto);

        }
    }
}