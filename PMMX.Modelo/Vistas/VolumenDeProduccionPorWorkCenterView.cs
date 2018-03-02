using PMMX.Modelo.Entidades.Maquinaria;
using System.Collections.Generic;

namespace PMMX.Modelo.Vistas
{
    public class VolumenDeProduccionPorWorkCenterView
    {
        public WorkCenter WorkCenter { get; set; }

        public List<PlanAttainmentView> PlanesDeProduccion { get; set; }
    }
}