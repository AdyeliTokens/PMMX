using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class ParoView
    {
        public int Id { get; set; }
        public int IdOrigen { get; set; }
        public int IdReportador { get; set; }
        public int? IdMecanico { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public DateTime FechaReporte { get; set; }
        public DateTime FechaCierre { get; set; }

        [StringLength(250)]
        public string Motivo { get; set; }
        public Boolean Activo { get; set; }



        public PersonaView Mecanico { get; set; }
        public PersonaView Reportador { get; set; }
        public OrigenView Origen { get; set; }

        public List<TiempoDeParoView> TiempoDeParos { get; set; }

    }
}