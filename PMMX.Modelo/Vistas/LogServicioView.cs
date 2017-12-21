using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class actividadView
    {
        public int ID { get; set; }
        public int idStatus { get; set; }
        public int idPersona { get; set; }
        public int idServicio { get; set; }
        public DateTime Fecha { get; set; }
        public int idIntervencion { get; set; }
    }
}