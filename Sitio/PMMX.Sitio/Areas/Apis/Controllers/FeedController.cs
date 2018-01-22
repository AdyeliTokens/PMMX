using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitio.Areas.Apis.Controllers
{
    public class FeedController : ApiController
    {

        private PMMXContext db;
        //private FeedServicio _servicio;

        public FeedController()
        {
            db = new PMMXContext();
            //_servicio = new FeedServicio(db);
        }

        #region GET

        public IHttpActionResult GetFeedPorWorkCenter(int idWorkCenter)
        {
            DateTime hoy = DateTime.Now;
            DateTime antier = hoy.AddDays(-3);

            RespuestaServicio< List<FeedView>> respuesta = new RespuestaServicio<List<FeedView>> ();
            if (idWorkCenter <= 0)
            {
                respuesta.Mensaje = "Incorrecto identificador para WorkCenter";
                return Ok(respuesta);
            }

            var feeds = new List<FeedView>();
            var actividadesEnParo = db.ActividadEnParos.Where(a => (a.Fecha >= antier && a.Fecha <= hoy)).ToList();
            foreach (var item in actividadesEnParo)
            {
                feeds.Add(new FeedView { Fecha = item.Fecha , actividadEnParo = item });
            }
            var actividadesEnDefecto = db.ActividadEnDefectos.Where(a => (a.Fecha >= antier && a.Fecha <= hoy)).ToList();
            foreach (var item in actividadesEnDefecto)
            {
                feeds.Add(new FeedView { Fecha = item.Fecha, actividadEnDefecto = item });
            }

            respuesta.Respuesta = feeds.OrderByDescending(f=> f.Fecha).ToList();

            return Ok(respuesta);
        }

        #endregion
    }
}
