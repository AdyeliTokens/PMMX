using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitio.Models
{
    public class RequisicionDeDescarga
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Comentario { get; set; }

        public string Email { get; set; }

        public DateTime Solicitud { get; set; }

        public DateTime Aprobacion { get; set; }
    }
}