using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class EventoResponsableView
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public int IdResponsable { get; set; }

        #region Navegacion
        public EventoView Evento { get; set; }
        public PersonaView Responsable { get; set; }
        #endregion
    }
}
