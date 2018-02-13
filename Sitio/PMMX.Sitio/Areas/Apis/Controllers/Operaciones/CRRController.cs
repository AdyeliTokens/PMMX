using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Vistas;
using PMMX.Operaciones.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class CRRController : ApiController
    {
        private PMMXContext db = new PMMXContext();


        [ResponseType(typeof(Desperdicio))]
        public IHttpActionResult GetDesperdiciosPorIdWorkCenter(int IdWorkCenter)
        {
            DesperdicioServicio servicio = new DesperdicioServicio(db);
            var respuesta = servicio.GetCRRPorWorkCenterPorSemanaActual(IdWorkCenter);
            return Ok(respuesta);
        }



        [ResponseType(typeof(Desperdicio))]
        public IHttpActionResult GetDesperdicioPorIdWorkCenter(DateTime fecha, int IdWorkCenter)
        {
            DesperdicioServicio servicio = new DesperdicioServicio(db);
            var respuesta = servicio.GetCRRPorWorkCenterPorSemana(fecha ,IdWorkCenter);
            return Ok(respuesta);


        }


    }
}
