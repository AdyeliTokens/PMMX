using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class BitacoraUnidadView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdGuardia { get; set; }
        public string Puerta { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public int IdRegistroUnidad { get; set; }
        #endregion

        #region Navegacion
        public PersonaView Guardia { get; set; }
        public RegistroUnidadView RegistroUnidad { get; set; }
        #endregion
    }
}
