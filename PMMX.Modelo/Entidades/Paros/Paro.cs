using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Paros
{
    public class Paro
    {
        public int Id { get; set; }
        public int IdOrigen { get; set; }
        public int IdReportador { get; set; }
        public int? IdMecanico { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReporte { get; set; }
        public string Motivo { get; set; }
        public Boolean Activo { get; set; }




        public Persona Reportador { get; set; }
        public Persona Mecanico { get; set; }
        public Origen Origen { get; set; }



        public ICollection<OrigenRespuesta> OrigenRespuesta { get; set; }
        public ICollection<ActividadEnParo> ActividadesEnParo { get; set; }
        public ICollection<TiempoDeParo> TiemposDeParo { get; set; }
        public ICollection<Asignacion> Asignaciones { get; set; }
    }
}