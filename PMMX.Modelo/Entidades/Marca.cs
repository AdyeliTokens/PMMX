using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Marca
    {
        [StringLength(250)]
        [Key]
        public string Code_FA { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        
        [StringLength(250)]
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

        public Persona PersonaQueDioDeAlta { get; set; }
        public ICollection<Desperdicio> Desperdicios { get; set; }
        public ICollection<VolumenDeProduccion> VolumenesProducidos { get; set; }
        public ICollection<PlanDeProduccion> PlanesDeProduccion { get; set; }
    }
}
