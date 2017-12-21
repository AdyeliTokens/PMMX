﻿using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class RemitentesView
    {
        public int Id { get; set; }
        public int IdPuesto { get; set; }
        public int IdOrigen { get; set; }
        public int IdHealthCheck { get; set; }
        public bool Activo { get; set; }

        public string Email { get; set; }
        public string NombreWorkCenter { get; set; }
        public string NombreHealthcheck { get; set; }

        public PuestoView Puesto { get; set; }
        public OrigenView Origen { get; set; }
        public GrupoPreguntasView HealthCheck { get; set; }

        public ICollection<PersonaView> Destinatarios { get; set; }
    }
}