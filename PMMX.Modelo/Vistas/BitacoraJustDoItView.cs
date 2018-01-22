using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class BitacoraJustDoItView
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdJustDoIt { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public int? IdRechazo { get; set; }
        public DateTime Fecha { get; set; }
        public String Comentario { get; set; }
        #endregion

        #region Navegacion
        public JustDoItView JustDoIt { get; set; }
        public EstatusView Estatus { get; set; }
        public PersonaView Responsable { get; set; }
        public RechazoView Rechazo { get; set; }
        #endregion
    }
}
