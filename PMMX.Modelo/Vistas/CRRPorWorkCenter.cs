using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades.Maquinaria;

namespace PMMX.Modelo.Vistas
{
    public class CRRPorWorkCenter
    {
        public WorkCenter WorkCenter { get; set; }
        public List<ReporteTotalView> Valores { get; set; }
        public List<DesperdicioView> Desperdicio { get; set; }
    }
}
