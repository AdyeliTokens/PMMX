using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class EventoResponsable
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public int IdResponsable { get; set; }

        #region Navegacion
        public Evento Evento { get; set; }
        public Persona Responsable { get; set; }
        #endregion
    }
}
