using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class WorkCenterView
    {
        public int Id { get; set; }
        public int IdBussinesUnit { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public bool Activo { get; set; }
        public int ParosActivos { get; set; }
        public int DefectosActivos { get; set; }
        public int IdResponsable { get; set; }
        
        public ICollection<OrigenView> Modulos { get; set; }
        public BussinesUnitView BussinesUnit { get; set; }
        public PersonaView Responsable { get; set; }
        public ICollection<GrupoPreguntasView> Formatos { get; set; }
        public ICollection<PersonaView> Operadores { get; set; }
        public ICollection<DesperdicioView> Desperdicios { get; set; }
        public ICollection<CRRView> CRR { get; set; }

    }
}