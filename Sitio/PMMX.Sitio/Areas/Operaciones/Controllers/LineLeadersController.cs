using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Operaciones.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sitio.Areas.Operaciones
{
    [Authorize]
    public class LineLeadersController : ApiController
    {
        private PMMXContext db = new PMMXContext();


        [ResponseType(typeof(IRespuestaServicio<List<WorkCenter>>))]
        public IHttpActionResult GetWorkCentersPorLineLeader(int id)
        {
            
            RespuestaServicio<List<WorkCenter>> respuesta = new RespuestaServicio<List<WorkCenter>>();
            if (id<=0) {
                respuesta.Mensaje = "Identificador del LineLeader es incorrecto ("+id+")";
                return Ok(respuesta);
            }

            WorkCenterServicio servicio = new WorkCenterServicio(db);
            respuesta = servicio.GetWorkCentersByLineLeader(id);
            
            return Ok(respuesta);
        }
    }
}
