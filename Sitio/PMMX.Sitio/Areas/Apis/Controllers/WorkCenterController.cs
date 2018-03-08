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
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Vistas;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;

namespace Sitio.Areas.Apis.Controllers
{
    public class WorkCenterController : ApiController
    {
        #region Gets

        public IQueryable<WorkCenter> GetWorkCenter()
        {
            WorkCenterServicio servicio = new WorkCenterServicio();
            
            return servicio.GetWorkCenters().Respuesta;
        }

        [ResponseType(typeof(IList<WorkCenter>))]
        public IHttpActionResult GetWorkCenterbyActivo(bool activo)
        {
            WorkCenterServicio servicio = new WorkCenterServicio();
            RespuestaServicio<List<WorkCenterView>> respuesta = new RespuestaServicio<List<WorkCenterView>>();
            respuesta = servicio.GetWorkCenterbyOrigen(activo);

            if (respuesta.EjecucionCorrecta)
            {
                return Ok(respuesta.Respuesta);
            }
            else
            {
                return NotFound();
            }
        }

        [ResponseType(typeof(WorkCenter))]
        public IHttpActionResult GetWorkCenter(int id)
        {
            WorkCenterServicio servicio = new WorkCenterServicio();
            RespuestaServicio<WorkCenterView> respuesta = new RespuestaServicio<WorkCenterView>();
            respuesta = servicio.GetWorkCenter(id);

            if (respuesta.EjecucionCorrecta)
            {
                return Ok(respuesta.Respuesta);
            }
            else
            {
                return NotFound();
            }

        }

        [ResponseType(typeof(IList<WorkCenter>))]
        public IHttpActionResult GetWorkCenterByPersona(int idPersona)
        {
            WorkCenterServicio servicio = new WorkCenterServicio();
            RespuestaServicio<List<WorkCenterView>> respuesta = new RespuestaServicio<List<WorkCenterView>>();
            respuesta = servicio.GetWorkCentersByPersona(idPersona);

            if (respuesta.EjecucionCorrecta)
            {
                return Ok(respuesta.Respuesta);
            }
            else
            {
                return NotFound();
            }

        }

        [ResponseType(typeof(IList<WorkCenter>))]
        public IHttpActionResult GetWorkCentersByBussinesUnit(int idBussinesUnit)
        {
            WorkCenterServicio servicio = new WorkCenterServicio();
            RespuestaServicio<List<WorkCenterView>> respuesta = new RespuestaServicio<List<WorkCenterView>>();
            respuesta = servicio.GetWorkCentersByBussinesUnit(idBussinesUnit);

            if (respuesta.EjecucionCorrecta)
            {
                return Ok(respuesta.Respuesta);
            }
            else
            {
                return NotFound();
            }

        }

        #endregion

        #region Puts

        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkCenter(int id, WorkCenter workCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workCenter.Id)
            {
                return BadRequest();
            }
            WorkCenterServicio servicio = new WorkCenterServicio();
            RespuestaServicio<WorkCenterView> respuesta = new RespuestaServicio<WorkCenterView>();

            respuesta = servicio.ActualizarWorkCenter(id, workCenter);

            if (respuesta.EjecucionCorrecta)
            {
                return Ok(respuesta.Respuesta);
            }
            else
            {
                return NotFound();
            }

        }

        #endregion

        #region Posts

        [ResponseType(typeof(WorkCenter))]
        public IHttpActionResult PostWorkCenter(WorkCenter workCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            RespuestaServicio<WorkCenterView> respuesta = new RespuestaServicio<WorkCenterView>();
            WorkCenterServicio servicio = new WorkCenterServicio();
            respuesta = servicio.PostWorkCenter(workCenter);
            if (respuesta.EjecucionCorrecta)
            {
                return Ok(respuesta.Respuesta);
            }
            else
            {
                return NotFound();
            }

        }

        #endregion

        #region Deletes

        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteWorkCenter(int id)
        {
            WorkCenterServicio servicio = new WorkCenterServicio();
            RespuestaServicio<bool> respuesta = new RespuestaServicio<bool>();
            respuesta = servicio.DeleteWorkCenter(id);

            return Ok(respuesta);

        }

        #endregion

        #region Helpers

        protected override void Dispose(bool disposing)
        {
            WorkCenterServicio servicio = new WorkCenterServicio();
            if (disposing)
            {
                servicio.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}