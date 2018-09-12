using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Paros;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class PersonaView
    {
        #region Propiedades
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Apellido1 { get; set; }

        [StringLength(250)]
        public string Apellido2 { get; set; }
        public bool Activo { get; set; }
        public int IdPuesto { get; set; }
        #endregion

        #region Navegacion 
        public PuestoView Puesto { get; set; }
        public WorkCenterView  WorkCenter { get; set; }
        public BussinesUnitView BussinesUnit { get; set; }
        public List<DefectoView> DefectosAsignados { get; set; }
        public List<ParoView> ParosAsignados { get; set; }
        public List<DispositivoView> Dispositivos { get; set; }
        public List<MenuView> Menu { get; set; }
        #endregion

    }
}