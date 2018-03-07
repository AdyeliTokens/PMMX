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
            ToTable("ActividadesEnDefectos");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdDefecto).HasColumnName("IdDefecto");
            Property(c => c.IdEjecutante).HasColumnName("IdEjecutante");
            Property(c => c.Descripcion).HasColumnName("Descripcion");
            Property(c => c.Fecha).HasColumnName("Fecha");



            HasRequired(c => c.Defecto).WithMany(x => x.Actividades).HasForeignKey( c => c.IdDefecto);
            HasRequired(c => c.Ejecutante).WithMany(x => x.ActividadesEnDefectoRealizadas).HasForeignKey(c => c.IdEjecutante);

        }
    }
}