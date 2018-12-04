using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades.Operaciones;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class GembaWalk
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
        public Boolean Activo { get; set; }
        #endregion

        #region Navegacion
        public Persona Reportador { get; set; }
        public Origen Origen { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public List<Foto> Fotos { get; set; }
        public ICollection<BitacoraGembaWalk> BitacoraGembaWalk { get; set; }
        #endregion
    }
}
