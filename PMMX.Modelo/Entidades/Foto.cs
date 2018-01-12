using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PMMX.Modelo.Entidades
{
    public class Foto
    {

        #region Propiedades

        public int Id { get; set; }
        public int? IdContribuidor { get; set; }
        //public int? IdJustDoIt { get; set; }
        public int? IdMantenimiento { get; set; }
        public string Path { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }

        #endregion

        #region Navegacion

        //public JustDoIt JustDoIt { get; set; }
        public Mantenimiento Mantenimiento { get; set; }
        public ICollection<Origen> Origenes { get; set; }
        public ICollection<Defecto> Defectos { get; set; }
        public ICollection<Persona> Personas { get; set; }
        public ICollection<JustDoIt> JustDoIts { get; set; }
        public Persona Contribuidor { get; set; }

        #endregion

    }
}
