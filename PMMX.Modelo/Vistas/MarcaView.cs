using System;
using System.Collections.Generic;

namespace PMMX.Modelo.Vistas
{
    public class MarcaView
    {
        
        public string Code_FA { get; set; }

        public string Descripcion { get; set; }

        public string Codigo_Cigarrillo { get; set; }
        public Double? PesoPorCigarrillo { get; set; }
        public Double? PesoTabacco { get; set; }
        public Boolean Activo { get; set; }

        public DateTime FechaDeAlta { get; set; }
        public int IdPersonaQueDioDeAlta { get; set; }
        public Double DesperdicioTotal
        {
            get
            {
                if (Desperdicios != null)
                {
                    Double desperdicioTotal = 0;
                    foreach (var item in Desperdicios)
                    {
                        desperdicioTotal = desperdicioTotal + item.Cantidad;
                    }
                    return desperdicioTotal;
                }
                else
                {
                    return 0;
                }

            }
        }

        public PersonaView PersonaQueDioDeAlta { get; set; }
        public ICollection<DesperdicioView> Desperdicios { get; set; }
        public ICollection<VolumenDeProduccionPorWorkCenterView> VolumenesProducidos { get; set; }
        public ICollection<PlanDeProduccionPorWorkCenterView> PlanesDeProduccion { get; set; }
    }
}