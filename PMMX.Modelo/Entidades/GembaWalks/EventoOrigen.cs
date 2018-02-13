using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.GembaWalks
{
    public class EventoOrigen
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public int IdOrigen { get; set; }

        #region Navegacion
        public Evento Evento { get; set; }
        public Origen Origen { get; set; }
        #endregion
    }
}
