using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Defectos
{
    /// <summary>
    /// 
    /// </summary>
    public class Defecto
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdReportador { get; set; }
        public int IdOrigen { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public Boolean Activo { get; set; }
        public DateTime FechaReporte { get; set; }
        public DateTime FechaEstimada { get; set; }
        public int Prioridad { get; set; }

        [StringLength(250)]
        public string NotificacionSAP { get; set; }
        public int IdResponsable { get; set; }
        #endregion
        
        #region Navegacion
        public Persona Reportador { get; set; }
        public Origen Origen { get; set; }
        public Persona Responsable { get; set; }
        public List<Foto> Fotos { get; set; }
        public ICollection<ActividadEnDefecto> Actividades { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Asignacion> Asignaciones { get; set; }
        
        #endregion

    }
}