﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMMX.Modelo.Vistas
{
    public class DefectoView
    {
        public int Id { get; set; }
        public int IdReportador { get; set; }
        public int IdOrigen { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
        public Boolean Activo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaReporte { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true , DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaEstimada { get; set; }
        public int Prioridad { get; set; }

        [StringLength(250)]
        public string NotificacionSAP{ get; set; }

        public List<ComentarioView> Comentarios { get; set; }
        public int? ComentariosCount { get; set; }
        public int? ActividadesCount { get; set; }

        public List<FotoView> Fotos { get; set; }
        public PersonaView Reportador { get; set; }
        public OrigenView Origen { get; set; }
    }
}