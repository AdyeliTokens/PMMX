﻿using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class AreaView
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public int IdResponsable { get; set; }
        public bool Activo { get; set; }

        public PersonaView Responsable { get; set; }
        public ICollection<BussinesUnitView> BussinesUnits { get; set; }
    }
}
