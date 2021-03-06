﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Entidades.Defectos
{
    public class ActividadEnDefecto
    {
        public int Id { get; set; }
        public int IdDefecto { get; set; }
        public int IdEjecutante { get; set; }

        [StringLength(250)]
        public String Descripcion  { get; set; }
        public DateTime Fecha { get; set; }
        
        public Defecto Defecto { get; set; }
        public Persona Ejecutante { get; set; }
    }
}