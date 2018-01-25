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
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Codigo { get; set; }
        public Boolean Activo { get; set; }
        public Double? PesoPorCigarrillo { get; set; }

        public ICollection<Desperdicio> Desperdicios { get; set; }
        public ICollection<VolumenDeProduccion> VolumenesProducidos { get; set; }
    }
}
