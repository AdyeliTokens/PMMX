using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.SeguridadFisica
{
    public  class BitacoraUnidad
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string NombreGuardia { get; set; }
        public string Puerta { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public int IdRegistroUnidad { get; set; }
        #endregion

        #region Navegacion
        public Persona Persona { get; set; }
        public RegistroUnidad RegistroUnidad { get; set; }
        #endregion
    }
}
