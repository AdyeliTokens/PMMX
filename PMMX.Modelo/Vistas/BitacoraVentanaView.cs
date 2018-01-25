using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class BitacoraVentanaView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdVentana { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public int? IdRechazo { get; set; }
        public DateTime Fecha { get; set; }

        [StringLength(250)]
        public string Comentarios { get; set; }
        public bool Activo { get; set; }
        #endregion

        #region Navegacion
        public EstatusView Estatus { get; set; }
        public VentanaView Ventana { get; set; }
        public PersonaView Responsable { get; set; }
        public RechazoView Rechazo { get; set; }
        #endregion
    }
}
