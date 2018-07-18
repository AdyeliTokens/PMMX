using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class WorkFlowMap : EntityTypeConfiguration<WorkFlow>
    {
        public WorkFlowMap()
        {
            #region Propiedades
            ToTable("WorkFlow");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.IdSubCategoria).HasColumnName("IdSubcategoria");
            Property(c => c.IdSubArea).HasColumnName("IdSubArea");
            Property(c => c.Inicial).HasColumnName("Inicial");
            Property(c => c.Anterior).HasColumnName("Anterior");
            Property(c => c.Siguiente).HasColumnName("Siguiente");
            Property(c => c.Cancelado).HasColumnName("Cancelado");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany
            #endregion

            #region HasOptional
            this.HasOptional(x => x.EstatusInicial).WithMany(c => c.WorkFlowInicial);
            this.HasOptional(x => x.EstatusAnterior).WithMany(c => c.WorkFlowAnterior);
            this.HasOptional(x => x.EstatusSiguiente).WithMany(c => c.WorkFlowSiguiente);
            #endregion

            #region HasRequired
            this.HasRequired(x => x.SubCategoria).WithMany(c => c.WorkFlows);
            this.HasRequired(x => x.SubArea).WithMany(c => c.WorkFlows);
            #endregion
        }
    }
}
