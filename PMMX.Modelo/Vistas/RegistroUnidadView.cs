using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class RegistroUnidadView
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
        public FormatoView Formato { get; set; }
        public ICollection<DatosUnidadView> Datos { get; set; }
        public ICollection<BitacoraUnidadView> Bitacora { get; set; }
        #endregion
    }
}
