using PMMX.Modelo.Entidades.Maquinaria;
using System.Collections.Generic;

namespace PMMX.Modelo.Vistas
{
    public class PlanDeProduccionPorWorkCenterView
    {
        public WorkCenter WorkCenter { get; set; }

        public List<PlanDeProduccionView> Produccion { get; set; }
    }
}