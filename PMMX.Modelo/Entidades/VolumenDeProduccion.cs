using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class VolumenDeProduccion
    {
        public int Id { get; set; }
        
        public string Container { get; set; }
        public string Code_FA { get; set; }
        public string Source_WH { get; set; }
        public string Source_Loc { get; set; }
        public string Dest_WH { get; set; }
        public string Dest_Loc { get; set; }
        public Double Old_Qty { get; set; }
        public Double New_Qty { get; set; }
        public string UOM { get; set; }
        public string SAP_Batch { get; set; }


        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }
        public int IdWorkCenter { get; set; }
        




        public Persona Reportante { get; set; }
        public WorkCenter WorkCenter { get; set; }
        public Marca MarcaDelCigarrillo { get; set; }

    }
}
