using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PMMX.Modelo.Entidades.GembaWalks;
using PMMX.Modelo.Entidades.Warehouse;

namespace PMMX.Modelo.Entidades
{
    public class Persona
    {
        

        #region Propiedades
        public int Id { get; set; }
        public int IdPuesto { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Apellido1 { get; set; }

        [StringLength(250)]
        public string Apellido2 { get; set; }
        public bool Activo { get; set; }

        #endregion

        #region Navegacion

        public Puesto Puesto { get; set; }
        public ICollection<Mecanicos> BusinessUnitParaReparar_Mecanico { get; set; }
        public ICollection<Electricos> BusinessUnitParaReparar_Electrico { get; set; }
        public ICollection<Operadores> WorkCenterParaOperar { get; set; }
        public ICollection<ShiftLeaders> CelulasPorVigilar { get; set; }
        public ICollection<WorkCenter> WorkCentersDondeEsResponsable { get; set; }
        public ICollection<BussinesUnit> BussinesUnitsDondeEsResponsable { get; set; }
        public ICollection<User> Usuarios { get; set; }
        public ICollection<UsuariosPorPersona> Users { get; set; }
        public ICollection<Paro> ParosReportados { get; set; }
        public ICollection<Paro> ParosAsignados { get; set; }
        public ICollection<Defecto> DefectosReportados { get; set; }
        public ICollection<Defecto> DefectosAsignados { get; set; }
        public ICollection<ActividadEnParo> ActividadesEnParoRealizadas { get; set; }
        public ICollection<ActividadEnDefecto> ActividadesEnDefectoRealizadas { get; set; }
        public ICollection<Dispositivo> Dispositivos { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<OrigenRespuesta> Operador { get; set; }
        public ICollection<OrigenRespuesta> Encuestado { get; set; }
        public ICollection<OrigenRespuesta> Supervisor { get; set; }
        public ICollection<Area> Areas { get; set; }
        public ICollection<SubArea> SubArea { get; set; }
        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<SubCategoria> SubCategorias { get; set; }
        public ICollection<Evento> EventosReportados { get; set; }
        public ICollection<GembaWalk> JustDoItReportados { get; set; }
        public ICollection<GembaWalk> JustDoItAsignados { get; set; }
        public ICollection<Mantenimiento> MantenimientosReportados { get; set; }
        public ICollection<Mantenimiento> MantenimientosAsignados { get; set; }
        public ICollection<Foto> FotosPersonales { get; set; }
        public ICollection<Foto> FotosSubidas { get; set; }
        public ICollection<NoConformidad> NoConformidadesIngresadas { get; set; }
        public ICollection<Desperdicio> DesperdicioReportado { get; set; }
        public ICollection<Asignacion> Asignaciones { get; set; }
        public ICollection<Alias> AliasDadosDeAlta { get; set; }
        public ICollection<Pesador> Pesadores { get; set; }
        public ICollection<ListaDistribucion> ListaDistribucion { get; set; }
        public ICollection<EventoResponsable> EventoResponsable { get; set; }
        public ICollection<VolumenDeProduccion> VolumenReportado { get; set; }

        public List<StatusVentana> StatusVentana { get; set; }
        public List<BitacoraVentana> BitacoraVentana { get; set; }
        public List<BitacoraGembaWalk> BitacoraGembaWalk { get; set; }
        #endregion

    }
}