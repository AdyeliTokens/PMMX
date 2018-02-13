using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Defectos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class ActividadEnDefectoView
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdDefecto { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }




        public DefectoView Defecto { get; set; }
        public PersonaView Ejecutante { get; set; }
    }
}
