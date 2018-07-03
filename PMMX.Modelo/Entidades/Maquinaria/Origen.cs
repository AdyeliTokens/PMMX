using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMMX.Modelo.Vistas;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class Origen
    {

        #region Propiedades

        public int Id { get; set; }
        public int? IdModulo { get; set; }
        public int IdWorkCenter { get; set; }
        public int? Orden { get; set; }

        #endregion

        #region Navegacion

        public Modulo Modulo { get; set; }
        public WorkCenter WorkCenter { get; set; }
        public ICollection<Paro> Paros { get; set; }
        public ICollection<Defecto> Defectos { get; set; }
        public ICollection<PreguntaTurno> PreguntaTurno { get; set; }
        //public ICollection<Pregunta> Preguntas { get; set; }
        public ICollection<OrigenRespuesta> OrigenRespuestas { get; set; }
        public ICollection<Remitentes> Remitentes { get; set; }
        public ICollection<GembaWalk> GembaWalk { get; set; }
        public ICollection<Mantenimiento> Mantenimientos { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public ICollection<EventoOrigen> EventoOrigen { get; set; }
        #endregion

        public static implicit operator Origen(OrigenView v)
        {
            throw new NotImplementedException();
        }
    }
}