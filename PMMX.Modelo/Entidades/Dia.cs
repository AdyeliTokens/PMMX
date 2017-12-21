using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades
{
    public class Dia
    {
        
        private List<int> list =new List<int>();
        private IEnumerable<int> dias;

        public Dia()
        {
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
        }

        public Dia(IEnumerable<int> dias)
        {
            this.dias = list;
        }

        public int Id { get; set; }

       public ICollection<Pregunta> Preguntas { get; set; }
       public ICollection<Turno> Turnos { get; set; }
       public IEnumerable<int> Dias { get => dias; set => dias = value; }
       public ICollection<PreguntaTurno> PreguntasTurno { get; set; }
    }
}
