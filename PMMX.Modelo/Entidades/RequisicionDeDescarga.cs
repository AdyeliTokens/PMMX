using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo
{
    public class RequisicionDeDescarga
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Comentario { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public DateTime Solicitud { get; set; }

        public DateTime Aprobacion { get; set; }
    }
}