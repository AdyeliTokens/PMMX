using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class GembaWalkView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdReporta { get; set; }
        public int IdOrigen { get; set; }

        [StringLength(532)]
        public string Descripcion { get; set; }
        [StringLength(532)]
        public string Impacto { get; set; }
        [StringLength(532)]
        public string ParametroIncumplido { get; set; }
        [StringLength(532)]
        public string AccionInmediata { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaReporte { get; set; }
        public int Prioridad { get; set; }
        public int IdSubCategoria { get; set; }
        public int Tipo { get; set; }
        public int? ActividadesCount { get; set; }
        public Boolean Activo { get; set; }

        //
        public string ReportadorNombre { get; set; }
        public string ResponsableNombre { get; set; }
        public string SubcategoriaNombre { get; set; }
        public string TipoNombre { get; set; }
        public string OrigenNombre { get; set; }
        public string SubCategoriaNombre { get; set; }
        public string StatusNombre { get; set; }
        #endregion

        #region Navegacion
        public PersonaView Reportador { get; set; }
        public OrigenView Origen { get; set; }
        public SubCategoriaView SubCategoria { get; set; }

        public List<FotoView> Fotos { get; set; }
        public ICollection<Entidades.Operaciones.BitacoraGembaWalk> BitacoraGembaWalk { get; set; }
        #endregion
    }
}