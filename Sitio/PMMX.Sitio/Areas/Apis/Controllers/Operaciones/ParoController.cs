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
using PMMX.Modelo.Entidades.Paros;
using PMMX.Modelo.Vistas;
using PMMX.Modelo;
using PMMX.Modelo.Entidades;
using Sitio.Helpers;
using PMMX.Seguridad.Servicios;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;

namespace Sitio.Areas.Apis.Controllers.Operaciones
{
    public class ParoController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        public IQueryable<Paro> GetParo()
        {

            return db.Paros;
        }
        
        [ResponseType(typeof(RespuestaServicio<Paro>))]
        public IHttpActionResult GetParo(int id)
        {

            ParoServicio servicio = new ParoServicio(db);
            var respuesta = servicio.GetParo(id);

            return Ok(respuesta);
        }

        [ResponseType(typeof(RespuestaServicio<List<Paro>>))]
        public IHttpActionResult getParosByOrigen(int idOrigen)
        {
            ParoServicio servicio = new ParoServicio(db);
            var respuesta = servicio.GetParosByOrigen(idOrigen);

            return Ok(respuesta);
        }
        
        [ResponseType(typeof(IList<ParoView>))]
        public IHttpActionResult getParosByBussinesUnit(int idBussinesUnit)
        {
            ParoServicio servicio = new ParoServicio(db);
            var respuesta = servicio.GetParosByBusinessUnit(idBussinesUnit);

            return Ok(respuesta);
        }

        [ResponseType(typeof(IList<ParoView>))]
        public IHttpActionResult getParosByWorkCenter(int idWorkCenter)
        {
            ParoServicio servicio = new ParoServicio(db);
            var respuesta = servicio.getParosByWorkCenter(idWorkCenter);

            return Ok(respuesta);
        }
        
        [ResponseType(typeof(Paro))]
        [HttpPut]
        public IHttpActionResult PutParo(int id, Paro paro)
        {
            ParoServicio servicio = new ParoServicio(db);
            var respuesta = servicio.PutParo(id,paro);

            return Ok(respuesta);
            
        }
        
        [ResponseType(typeof(RespuestaServicio<Paro>))]
        [HttpPost]
        public IHttpActionResult PostParo(Paro paro)
        {
            paro.FechaReporte = DateTime.Now;
            paro.Activo = true;
            paro.ActividadesEnParo = new List<ActividadEnParo> { new ActividadEnParo { Fecha = DateTime.Now, Descripcion = "Nueva Falla reportada!!", IdPersona = paro.IdReportador } };
            paro.TiemposDeParo = new List<TiempoDeParo> { new TiempoDeParo { Inicio = DateTime.Now } };

            ParoServicio servicio = new ParoServicio(db);
            var respuesta = servicio.PostParo(paro);

            

            NotificationService notify = new NotificationService();
            UsuarioServicio usuarioServicio = new UsuarioServicio();

            List<DispositivoView> dispositivos = usuarioServicio.GetMecanicosPorOrigen(paro.IdOrigen);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
                //notify.SendPushNotification(notificacion, "El modulo " + paroAdded.Origen.Modulo.NombreCorto + " necesita de tu ayuda urgentemente.", "Nueva Falla reportada en " + paroAdded.Origen.WorkCenter.NombreCorto + " por favor ve lo mas pronto posible a ayudarlos.");
            }

            return Ok(respuesta);
        }
        

        [ResponseType(typeof(Paro))]
        public IHttpActionResult DeleteParo(int id)
        {
            ParoServicio servicio = new ParoServicio(db);
            var respuesta = servicio.DeleteParo(id);

            return Ok(respuesta);


            
        }

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