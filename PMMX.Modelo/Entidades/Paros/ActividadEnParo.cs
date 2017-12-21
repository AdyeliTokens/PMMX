using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Paros
{
    public class ActividadEnParo
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdParo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }




        public Paro Paro { get; set; }
        public Persona Ejecutante { get; set; }
    }
}