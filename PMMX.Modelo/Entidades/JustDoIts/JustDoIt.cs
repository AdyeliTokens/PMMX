using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Modelo.Entidades.Operaciones;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class JustDoIt
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public int IdReportador { get; set; }
        public int IdOrigen { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReporte { get; set; }
        public DateTime FechaEstimada { get; set; }
        public int Prioridad { get; set; }
        public int IdResponsable { get; set; }
        public int IdSubCategoria { get; set; }
        public Boolean Activo { get; set; }
        
        public Persona Reportador { get; set; }
        public Persona Responsable { get; set; }
        public Origen Origen { get; set; }
        public Evento Evento { get; set; }
        public SubCategoria SubCategoria { get; set; }
        public List<Foto> Fotos { get; set; }
    }
}
