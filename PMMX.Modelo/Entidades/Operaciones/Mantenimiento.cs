using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class Mantenimiento
    {
        public int Id { get; set; }
        public int IdReportador { get; set; }
        public int IdOrigen { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public DateTime FechaReporte { get; set; }
        public DateTime FechaEstimada { get; set; }
        public int Prioridad { get; set; }
        public int IdResponsable { get; set; }
        public Boolean Activo { get; set; }


        public Persona Reportador { get; set; }
        public Persona Responsable { get; set; }
        public Origen Origen { get; set; }

        
    }
}
