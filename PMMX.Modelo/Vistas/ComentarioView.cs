using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class ComentarioView
    {
        public int Id { get; set; }
        public int IdComentador { get; set; }
        public int IdDefecto { get; set; }
        public string Opinion { get; set; }
        public DateTime Fecha { get; set; }

        public PersonaView Comentador { get; set; }
        public DefectoView Defecto { get; set; }
    }
}
