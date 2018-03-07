using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    
    public class Desperdicio
    {
        public int Id { get; set; }
        public Double Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }
        public int IdWorkCenter { get; set; }
        public string Code_FA { get; set; }




        public Persona Reportante { get; set; }
        public WorkCenter WorkCenter { get; set; }
        public Marca MarcaDelCigarrillo { get; set; }
    }
}
