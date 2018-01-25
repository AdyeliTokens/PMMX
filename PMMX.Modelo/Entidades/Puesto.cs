using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Paros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PMMX.Modelo.Entidades
{
    public class Puesto
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }

        public ICollection<Persona> Personas { get; set; }
        public ICollection<Remitentes> Remitentes { get; set; }

    }
}