using System;

namespace PMMX.Modelo.Entidades.Maquinaria
{
    public class ObjetivoVQI
    {
        public int Id { get; set; }
        public int IdWorkCenter { get; set; }
        public int Objetivo { get; set; }
        public DateTime FechaInicial { get; set; }


        public WorkCenter WorkCenter { get; set; }
    }
}