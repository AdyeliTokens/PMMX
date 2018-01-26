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
    public class Remitentes
    {
        public int Id { get; set; }
        public int IdPuesto { get; set; }
        public int IdOrigen { get; set; }
        public int IdGrupo { get; set; }
        public bool Activo { get; set; }

        public Puesto Puesto { get; set; }
        public Origen Origen { get; set; }
        public GrupoPreguntas GrupoPregunta { get; set; }
        
    }
}
