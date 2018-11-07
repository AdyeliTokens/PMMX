using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.SeguridadFisica
{
    public class DatosUnidad
    {
        #region Propiedades
        [Key]
        public int Id { get; set; }
        public string NombreConductor { get; set; }
        public string PlacasTractor { get; set; }
        public string NoEcoTractor { get; set; }
        public string TipoRemolque { get; set; }
        public string NoCaja { get; set; }
        public string PlacasRemolque { get; set; }
        public string NoEcoRemolque { get; set; }
        public int IdRegistroUnidad{ get; set; }
        #endregion

        #region Navegacion
        public RegistroUnidad RegistroUnidad { get; set; }
        #endregion
    }
}
