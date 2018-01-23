using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class ActividadEnParoView
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdParo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }




        public ParoView Paro { get; set; }
        public PersonaView Ejecutante { get; set; }
    }
}
