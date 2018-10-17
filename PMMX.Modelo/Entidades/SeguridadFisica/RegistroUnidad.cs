using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.SeguridadFisica
{
    public class RegistroUnidad
    {
        #region Propiedades
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Asunto { get; set; }
        public string NombreAutoriza { get; set; }
        public int NoGafette { get; set; }
        public int IdFormato { get; set; }
        #endregion

        #region Navegacion
        public Formato Formato { get; set; }
        public ICollection<DatosUnidad> Datos { get; set; }
        public ICollection<BitacoraUnidad> Bitacora { get; set; }
        #endregion
    }
}
