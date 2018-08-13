using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PMMX.Modelo.Entidades.Seguridad
{
    public class Acceso
    {
        #region Propiedades
        public int Id { get; set; }
        public string Area { get; set; }
        public string Menu { get; set; }
        public string SubMenu { get; set; }
        public string Programa { get; set; }
        public string Ruta { get; set; }
        public bool Activo { get; set; }
        #endregion
        #region Navegacion
        public ICollection<Persona> Personas { get; set; }
        #endregion
    }
}
