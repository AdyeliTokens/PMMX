using PMMX.Modelo;
using PMMX.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Map
{
    public class PersonaMap : EntityTypeConfiguration<Persona>
    {
        public PersonaMap()
        {

            #region Propiedades

            ToTable("Personas");
            HasKey(c => c.Id);
            Property(c => c.Id).HasColumnName("Id");
            Property(c => c.Nombre).HasColumnName("Nombre");
            Property(c => c.Apellido1).HasColumnName("Apellido1");
            Property(c => c.Apellido2).HasColumnName("Apellido2");
            Property(c => c.IdPuesto).HasColumnName("IdPuesto");
            Property(c => c.Activo).HasColumnName("Activo");
            #endregion

            #region HasMany

            
            HasMany(c => c.ParosReportados).WithRequired(p => p.Reportador).HasForeignKey(c => c.IdReportador);
            HasMany(c => c.DefectosReportados).WithRequired(p => p.Reportador).HasForeignKey(c => c.IdReportador);
            HasMany(c => c.DefectosAsignados).WithRequired(p => p.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.ActividadesEnDefectoRealizadas).WithRequired(p => p.Ejecutante).HasForeignKey(c => c.IdEjecutante);
            HasMany(c => c.ActividadesEnParoRealizadas).WithRequired(p => p.Ejecutante).HasForeignKey(c => c.IdPersona);
            HasMany(c => c.BussinesUnitsDondeEsResponsable).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.WorkCentersDondeEsResponsable).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.Dispositivos).WithRequired(b => b.Propietario).HasForeignKey(c => c.IdPersona);
            HasMany(c => c.Operador).WithRequired(b => b.Operador).HasForeignKey(c => c.IdOperador);
            HasMany(c => c.CelulasPorVigilar).WithRequired(b => b.ShiftLeader).HasForeignKey(c => c.IdShiftLeader);
            HasMany(c => c.Encuestado).WithRequired(b => b.Entrevistado).HasForeignKey(c => c.IdEntrevistado);
            HasMany(c => c.Supervisor).WithRequired(b => b.Supervisor).HasForeignKey(c => c.IdSupervisor);
            HasMany(c => c.Comentarios).WithRequired(x => x.Comentador).HasForeignKey(c => c.IdComentador);
            HasMany(c => c.MantenimientosAsignados).WithRequired(x => x.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.MantenimientosReportados).WithRequired(x => x.Reportador).HasForeignKey(c => c.IdReportador);
            HasMany(c => c.JustDoItAsignados).WithRequired(x => x.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.JustDoItReportados).WithRequired(x => x.Reportador).HasForeignKey(c => c.IdReportador);
            HasMany(c => c.Asignaciones).WithRequired(x => x.Asignado).HasForeignKey(c => c.IdAsignado);
            HasMany(c => c.FotosSubidas).WithOptional(x => x.Contribuidor).HasForeignKey(c => c.IdContribuidor);
            HasMany(c => c.FotosPersonales).WithMany(x => x.Personas).Map(cs =>
            {
                cs.MapLeftKey("IdPersona");
                cs.MapRightKey("IdFoto");
                cs.ToTable("PersonasFotos");
            });

            HasMany(c => c.ListaDistribucion).WithRequired(x => x.Remitente).HasForeignKey(c => c.IdPersona);
            HasMany(c => c.Areas).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.SubArea).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.EventoResponsable).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.EventosReportados).WithRequired(b => b.Asignador).HasForeignKey(c => c.IdAsignador);
            HasMany(c => c.StatusVentana).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.BitacoraVentana).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.BitacoraGembaWalk).WithRequired(b => b.Responsable).HasForeignKey(c => c.IdResponsable);
            HasMany(c => c.MarcasDadasDeAlta).WithRequired(b => b.PersonaQueDioDeAlta).HasForeignKey(c => c.IdPersonaQueDioDeAlta);
            HasMany(c => c.PlanesDeProduccionReportados).WithRequired(b => b.Uploader).HasForeignKey(c => c.IdUploader);

            #endregion

            #region HasRequired

            HasRequired(c => c.Puesto).WithMany(x => x.Personas).HasForeignKey(c => c.IdPuesto);

            #endregion

            #region HasOptional


            #endregion

        }
    }
}