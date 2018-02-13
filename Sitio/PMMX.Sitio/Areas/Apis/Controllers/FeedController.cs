using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Sitio.Areas.Apis.Controllers
{
    public class FeedController : ApiController
    {

        private PMMXContext db;


        public FeedController()
        {
            db = new PMMXContext();
        }

        #region GET

        public IHttpActionResult GetFeedPorWorkCenter(int idWorkCenter)
        {
            DateTime hoy = DateTime.Now;
            DateTime antier = hoy.AddDays(-3);

            RespuestaServicio<List<FeedView>> respuesta = new RespuestaServicio<List<FeedView>>();
            if (idWorkCenter <= 0)
            {
                respuesta.Mensaje = "Incorrecto identificador para WorkCenter";
                return Ok(respuesta);
            }

            var feeds = new List<FeedView>();
            var actividadesEnParo = db.ActividadEnParos
                .Where(a => (a.Fecha >= antier && a.Fecha <= hoy) && (a.Paro.Origen.IdWorkCenter == idWorkCenter))
                .Select(f => new ActividadEnParoView
                {
                    Descripcion = f.Descripcion,
                    Fecha = f.Fecha,
                    Paro = new ParoView
                    {
                        Id = f.Paro.Id,
                        Descripcion = f.Paro.Descripcion,
                        Origen = new OrigenView
                        {
                            Id = f.Paro.Origen.Id,
                            Modulo = new ModuloView
                            {
                                Id = f.Paro.Origen.Modulo.Id,
                                Nombre = f.Paro.Origen.Modulo.Nombre,
                                NombreCorto = f.Paro.Origen.Modulo.NombreCorto

                            },
                            WorkCenter = new WorkCenterView
                            {
                                Id = f.Paro.Origen.WorkCenter.Id,
                                Nombre = f.Paro.Origen.WorkCenter.Nombre,
                                NombreCorto = f.Paro.Origen.WorkCenter.NombreCorto
                            }
                        }
                    },
                    Ejecutante = new PersonaView
                    {
                        Id = f.Ejecutante.Id,
                        Nombre = f.Ejecutante.Nombre,
                        Apellido1 = f.Ejecutante.Apellido1,
                        Apellido2 = f.Ejecutante.Apellido2
                    },
                })
                .ToList();
            foreach (var item in actividadesEnParo)
            {
                feeds.Add(new FeedView { Fecha = item.Fecha, actividadEnParo = item });
            }

            var actividadesEnDefecto = db.ActividadEnDefectos
                .Where(a => (a.Fecha >= antier && a.Fecha <= hoy) && (a.Defecto.Origen.IdWorkCenter == idWorkCenter))
                .Select(f => new ActividadEnDefectoView
                {
                    Descripcion = f.Descripcion,
                    Fecha = f.Fecha,
                    Defecto = new DefectoView
                    {
                        Id = f.Defecto.Id,
                        Descripcion = f.Defecto.Descripcion,
                        Origen = new OrigenView
                        {
                            Id = f.Defecto.Origen.Id,
                            Modulo = new ModuloView
                            {
                                Id = f.Defecto.Origen.Modulo.Id,
                                Nombre = f.Defecto.Origen.Modulo.Nombre,
                                NombreCorto = f.Defecto.Origen.Modulo.NombreCorto

                            },
                            WorkCenter = new WorkCenterView
                            {
                                Id = f.Defecto.Origen.WorkCenter.Id,
                                Nombre = f.Defecto.Origen.WorkCenter.Nombre,
                                NombreCorto = f.Defecto.Origen.WorkCenter.NombreCorto
                            }
                        }
                    },
                    Ejecutante = new PersonaView
                    {
                        Id = f.Ejecutante.Id,
                        Nombre = f.Ejecutante.Nombre,
                        Apellido1 = f.Ejecutante.Apellido1,
                        Apellido2 = f.Ejecutante.Apellido2
                    },
                })
                .ToList();
            foreach (var item in actividadesEnDefecto)
            {
                feeds.Add(new FeedView { Fecha = item.Fecha, actividadEnDefecto = item });
            }

            respuesta.Respuesta = feeds.OrderByDescending(f => f.Fecha).ToList();

            return Ok(respuesta);
        }

        #endregion
    }
}
