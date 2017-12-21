using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class IntervencionView
    {

        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int idModuloWorkCenter { get; set; }
        public bool Status { get; set; }
        public byte[] Evidencia { get; set; }
        public actividadView actividad { get; set; }
    }
}