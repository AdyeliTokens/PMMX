
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Vistas;
using System.Collections;
using System.Collections.Generic;
using System;
using PMMX.Modelo.Entidades.Maquinaria;

namespace Sitio.Areas.Apis.Controllers
{
    public class VQIController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        [ResponseType(typeof(NoConformidad))]
        public IHttpActionResult GetNoConformidadesPorIdWorkCenter(int IdWorkCenter)
        {

            DateTime diaSeleccionado = DateTime.Now.Date;
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1,1);
            
            var noConformidades = db.NoConformidades.Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado)).ToList();
            var objetivos = db.ObjetivosVQI.Select(x=> x).Where(x=> (x.IdWorkCenter == IdWorkCenter) && (x.FechaInicial >= primerDiaDelAnio)).ToList();

            IList<VQIView> vqi = new List<VQIView>();
            for (int i = delta; i <= (6 + delta); i++)
            {
                int vqiTotal = noConformidades.Where(x => x.Fecha.Date == diaSeleccionado.AddDays(i).Date).Sum(o => o.Calificacion_VQI);
                int objetivo = objetivos.Where(x => x.FechaInicial <= diaSeleccionado.AddDays(i).Date).OrderByDescending(x=> x.FechaInicial).Select(x=> x.Objetivo).FirstOrDefault();
                
                vqi.Add(new VQIView { Fecha = diaSeleccionado.AddDays(i), VQI_Total = vqiTotal, Objetivo = objetivo });
            }

            return Ok(vqi);
        }


        [ResponseType(typeof(NoConformidad))]
        public IHttpActionResult GetNoConformidadesPorIdWorkCenter(DateTime fecha, int IdWorkCenter)
        {
           
            DateTime diaSeleccionado = fecha.Date;
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            DateTime sunday = monday.AddDays(6);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);




            var noConformidades = db.NoConformidades.Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado)).ToList();
            var objetivos = db.ObjetivosVQI.Select(x => x).Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.FechaInicial >= primerDiaDelAnio)).ToList();

            IList<VQIView> vqi = new List<VQIView>();
            for (int i = delta; i <= (6 + delta); i++)
            {
                int vqiTotal = noConformidades.Where(x => x.Fecha.Date == diaSeleccionado.AddDays(i).Date).Sum(o => o.Calificacion_VQI);
                int objetivo = objetivos.Where(x => x.FechaInicial <= diaSeleccionado.AddDays(i).Date).OrderByDescending(x => x.FechaInicial).Select(x => x.Objetivo).FirstOrDefault();

                vqi.Add(new VQIView { Fecha = diaSeleccionado.AddDays(i), VQI_Total = vqiTotal, Objetivo = objetivo });
            }

            return Ok(vqi);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoConformidadExists(int id)
        {
            return db.NoConformidades.Count(e => e.Id == id) > 0;
        }
    }
}