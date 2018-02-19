using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class PlanDeProduccion
    {
        public String Codigo { get; set; }
        public int IdWorkCenter { get; set; }
        public String Code_FA { get; set; }
        public Double Cantidad { get; set; }
        public String ClaseDeOrden { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public DateTime FechaSubida { get; set; }
        public int IdUploader { get; set; }
        public Boolean Activo { get; set; }

        public Marca Marca_FA { get; set; }
        public Persona Uploader { get; set; }
        public WorkCenter WorkCenterEfectivo { get; set; }



    }
}
