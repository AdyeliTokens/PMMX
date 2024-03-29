﻿using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Vistas
{
    public class VolumenDeProduccionView
    {
        public int Id { get; set; }
        public Double Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersona { get; set; }
        public int IdWorkCenter { get; set; }
        public int IdSeccion { get; set; }
        public string Code_FA { get; set; }




        public Persona Reportante { get; set; }
        public ModuloSeccion Seccion { get; set; }
        public WorkCenter WorkCenter { get; set; }
        public Marca MarcaDelCigarrillo { get; set; }
    }
}
