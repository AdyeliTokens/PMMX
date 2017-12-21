using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Turno
    {
        private List<int> list = new List<int>();
        private IEnumerable<int> turnos;

        public Turno()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);
        }

        public Turno(IEnumerable<int> turnos)
        {
            this.turnos = list;
        }

        public int Id { get; set; }
        
        public ICollection<Pregunta> Preguntas { get; set; }
        public ICollection<PreguntaTurno> PreguntaTurno { get; set; }
        public IEnumerable<int> Turnos { get => turnos; set => turnos = value; }
    }
}
