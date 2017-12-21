using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class GrupoPreguntasView
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool DDS { get; set; }
        public int IdCategoria { get; set; }
        public bool Activo { get; set; }
        
        public CategoriaView Categorias { get; set; }
        public ICollection<PreguntaView> Preguntas { get; set; }
        public ICollection<RespuestaView> Respuestas { get; set; }
        public ICollection<WorkCenterView> WorkCenters { get; set; }
    }
}