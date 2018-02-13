using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class BitacoraGembaWalk
    {
        #region Propiedades
        public int Id { get; set; }
        public int IdGembaWalk { get; set; }
        public int IdStatus { get; set; }
        public int IdResponsable { get; set; }
        public DateTime Fecha { get; set; }

        [StringLength(250)]
        public string Comentario { get; set; }
        #endregion

        #region Navegacion
        public GembaWalkView GembaWalk { get; set; }
        public EstatusView Estatus { get; set; }
        public PersonaView Responsable { get; set; }
        #endregion
    }
}
