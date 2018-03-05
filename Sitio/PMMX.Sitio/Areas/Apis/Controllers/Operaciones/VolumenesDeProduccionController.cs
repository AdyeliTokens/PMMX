using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class VolumenesDeProduccionController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        [ResponseType(typeof(VolumenDeProduccion))]
        public IHttpActionResult GetVolumenesPorIdWorkCenter(int IdWorkCenter)
        {
            RespuestaServicio<List<PlanAttainmentView>> respuesta = new RespuestaServicio<List<PlanAttainmentView>>();
            DateTime diaSeleccionado = DateTime.Now.Date.AddDays(1);
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);

            var volumenesDeProduccion = db.VolumenesDeProduccion.Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado)).ToList();
            var objetivos = db.ObjetivosPlanAttainment.Select(x => x).Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.FechaInicial >= primerDiaDelAnio)).ToList();

            List<PlanAttainmentView> volumenes = new List<PlanAttainmentView>();
            for (int i = delta; i <= (6 + delta); i++)
            {
                double volumenTotal = volumenesDeProduccion.Where(x => x.Fecha.Date == diaSeleccionado.AddDays(i).Date).Sum(o => o.New_Qty);
                double objetivo = objetivos.Where(x => x.FechaInicial <= diaSeleccionado.AddDays(i).Date).OrderByDescending(x => x.FechaInicial).Select(x => x.Objetivo).FirstOrDefault();

                volumenes.Add(new PlanAttainmentView { Fecha = diaSeleccionado.AddDays(i), Plan_Attainment_Total = volumenTotal, Objetivo = objetivo });
            }

            respuesta.Respuesta = volumenes;

            return Ok(respuesta);
        }

        [ResponseType(typeof(VolumenDeProduccion))]
        public IHttpActionResult GetVolumenPorIdWorkCenter(DateTime fecha, int IdWorkCenter)
        {
            DateTime diaSeleccionado = fecha.Date;
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            DateTime sunday = monday.AddDays(6);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);


            var volumenesDeProduccion = db.VolumenesDeProduccion.Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado)).ToList();
            var objetivos = db.ObjetivosPlanAttainment.Select(x => x).Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.FechaInicial >= primerDiaDelAnio)).ToList();

            List<PlanAttainmentView> volumenes = new List<PlanAttainmentView>();
            for (int i = delta; i <= (6 + delta); i++)
            {
                double volumenTotal = volumenesDeProduccion.Where(x => x.Fecha.Date == diaSeleccionado.AddDays(i).Date).Sum(o => o.New_Qty);
                double objetivo = objetivos.Where(x => x.FechaInicial <= diaSeleccionado.AddDays(i).Date).OrderByDescending(x => x.FechaInicial).Select(x => x.Objetivo).FirstOrDefault();

                volumenes.Add(new PlanAttainmentView { Fecha = diaSeleccionado.AddDays(i), Plan_Attainment_Total = volumenTotal, Objetivo = objetivo });
            }
            
            return Ok(volumenes);


        }


        [ResponseType(typeof(VolumenDeProduccion))]
        public IHttpActionResult PostVolumenDeProduccion(VolumenDeProduccion volumenDeProduccion)
        {
            RespuestaServicio<VolumenDeProduccion> respuesta = new RespuestaServicio<VolumenDeProduccion>();
            volumenDeProduccion.Fecha = DateTime.Now;
            VolumenDeProduccionServicio servicico = new VolumenDeProduccionServicio(db);
            respuesta = servicico.PostVolumenDeProduccion(volumenDeProduccion);

            return Ok(respuesta);
        }

    }
}