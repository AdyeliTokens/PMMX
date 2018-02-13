using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class FotoView
    {
        public int Id { get; set; }
        public int? IdGembaWalk { get; set; }
        public int? IdMantenimiento { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        public DefectoView Defecto { get; set; }
        public GembaWalkView GembaWalk { get; set; }
        public MantenimientoView Mantenimiento { get; set; }
    }
}
