using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class BussinesUnitView
    {
        public int Id { get; set; }
        public int IdResponsable { get; set; }
        public int IdArea { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string NombreCorto { get; set; }


        public Boolean Activo { get; set; }

        public int ParosActivos { get; set; }
        public int DefectosActivos { get; set; }

        public AreaView Area { get; set; }
        public PersonaView Responsable { get; set; }
        public ICollection<WorkCenterView> WorkCenters { get; set; }
        public ICollection<PersonaView> Mecanicos { get; set; }
        
    }
}