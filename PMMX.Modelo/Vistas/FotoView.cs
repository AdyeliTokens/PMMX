using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class FotoView
    {
        public int Id { get; set; }
        public int? IdJustDoIt { get; set; }
        public int? IdMantenimiento { get; set; }
        public string Path { get; set; }
        public string Nombre { get; set; }

        public DefectoView Defecto { get; set; }
        public JustDoItView JustDoIt { get; set; }
        public MantenimientoView Mantenimiento { get; set; }
    }
}
