using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class VQIporWorkCenterView
    {
        public WorkCenter WorkCenter { get; set; }

        public List<VQIView> VQIs { get; set; }
    }
}
