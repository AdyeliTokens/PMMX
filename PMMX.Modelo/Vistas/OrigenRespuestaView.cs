using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class OrigenRespuestaView
    {
        public int Id { get; set; }
        public int IdOrigen { get; set; }
        public int IdOperador { get; set; }
        public int IdEntrevistado { get; set; }
        public int IdSupervisor { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }

        public OrigenView Origen { get; set; }
        public PersonaView Operador { get; set; }
        public PersonaView Entrevistado { get; set; }
        public PersonaView Supervisor { get; set; }
        
        public ICollection<RespuestaView> Respuestas { get; set; }
    }
}
