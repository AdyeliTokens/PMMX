using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Paros;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class ActividadEnParoController : ApiController
    {
        private PMMXContext db;
        private ActividadEnParoServicio _servicio;

        ActividadEnParoController()
        {
            db = new PMMXContext();
            _servicio = new ActividadEnParoServicio(db);
        }

        #region GET
        public RespuestaServicio<IQueryable<ActividadEnParo>> GetActividadEnParo()
        {
            return _servicio.GetActividadesEnParo();
        }

        [ResponseType(typeof(RespuestaServicio<ActividadEnParo>))]
        public IHttpActionResult GetActividadEnParo(int id)
        {
            var respuesta = _servicio.GetActividadEnParo(id);

            return Ok(respuesta);
        }

        #endregion

        #region PUT
        [ResponseType(typeof(RespuestaServicio<ActividadEnParo>))]
        public IHttpActionResult PutActividadEnParo(int id, ActividadEnParo actividadEnParo)
        {
            var respuesta = _servicio.PutActividadEnParo(id, actividadEnParo);

            return Ok(respuesta);
        }
        #endregion

        #region Post
        [ResponseType(typeof(RespuestaServicio<ActividadEnParo>))]
        public IHttpActionResult PostActividadEnParo(ActividadEnParo actividadEnParo)
        {
            var respuesta = _servicio.PostActividadEnParo(actividadEnParo);

            return Ok(respuesta);
        }
        #endregion

        #region Delete
        [ResponseType(typeof(RespuestaServicio<ActividadEnParo>))]
        public IHttpActionResult DeleteActividadEnParo(int id)
        {
            var respuesta = _servicio.DeleteActividadEnParo(id);

            return Ok(respuesta);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}