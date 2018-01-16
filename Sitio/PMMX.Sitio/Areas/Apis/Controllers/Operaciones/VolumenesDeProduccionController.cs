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

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class VolumenesDeProduccionController : ApiController
    {
        private PMMXContext db = new PMMXContext();

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