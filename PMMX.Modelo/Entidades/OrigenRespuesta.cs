using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades
{
    public class OrigenRespuesta
    {
        public int Id { get; set; }
        public int IdOrigen { get; set; }
        public int IdOperador { get; set; }
        public int IdEntrevistado { get; set; }
        public int IdSupervisor { get; set; }
        public int IdParo { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        
        public Origen Origen { get; set; }
        public Persona Operador { get; set; }
        public Persona Entrevistado { get; set; }
        public Persona Supervisor { get; set; }

        public Paro Paro { get; set; }

        public ICollection<Respuesta> Respuestas { get; set; }
    }
}
