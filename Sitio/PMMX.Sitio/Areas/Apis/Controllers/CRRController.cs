using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sitio.Areas.Apis.Controllers
{
    public class CRRController : ApiController
    {
        private PMMXContext db = new PMMXContext();


        [ResponseType(typeof(Desperdicio))]
        public IHttpActionResult GetDesperdiciosPorIdWorkCenter(int IdWorkCenter)
        {

            DateTime diaSeleccionado = DateTime.Now.Date;
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);

            var desperdicios = db.Desperdicios.Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado)).ToList();
            //var objetivos = db.ObjetivosVQI.Select(x => x).Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.FechaInicial >= primerDiaDelAnio)).ToList();

            IList<CRRView> crr = new List<CRRView>();
            for (int i = delta; i <= (6 + delta); i++)
            {
                Double crrTotal = desperdicios.Where(x => x.Fecha.Date == diaSeleccionado.AddDays(i).Date).Sum(o => o.Cantidad);
                //int objetivo = objetivos.Where(x => x.FechaInicial <= diaSeleccionado.AddDays(i).Date).OrderByDescending(x => x.FechaInicial).Select(x => x.Objetivo).FirstOrDefault();

                crr.Add(new CRRView { Fecha = diaSeleccionado.AddDays(i), CRR_Total = crrTotal, Objetivo = 0 });
            }

            return Ok(crr);
        }



        [ResponseType(typeof(NoConformidad))]
        public IHttpActionResult GetDesperdicioPorIdWorkCenter(DateTime fecha, int IdWorkCenter)
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


    }
}
