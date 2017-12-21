﻿using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Entidades.Operaciones
{
    public class Evento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdOrigen { get; set; }
        public int IdCategoria { get; set; }
        public int IdAsignador { get; set; }
        public int IdResponsable { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaInicio { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaFin { get; set; }
        public string Nota { get; set; }
        public bool EsRecurrente { get; set; }
        public bool Activo { get; set; }

        public Persona Asignador { get; set; }
        public Persona Responsable { get; set; }
        public Origen Origen { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<JustDoIt> JustDoIt { get; set; }
    }
}
