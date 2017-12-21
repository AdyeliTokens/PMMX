using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System.Web.Routing;
using PMMX.Modelo.Entidades;
using PMMX.Seguridad.Servicios;
using Microsoft.AspNet.Identity;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/Mantenimiento
        public ActionResult Index()
        {
            MantenimientoServicio servicio = new MantenimientoServicio();
            var Mantenimiento = servicio.GetMantenimiento();

            return View(Mantenimiento.Respuesta.ToList());
        }

        // GET: Maquinaria/Mantenimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idMantenimiento = (int)id;
            MantenimientoServicio servicio = new MantenimientoServicio();
            RespuestaServicio<MantenimientoView> Mantenimiento = servicio.GetMantenimiento(idMantenimiento);
            if (Mantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(Mantenimiento.Respuesta);
        }

        // GET: Maquinaria/Mantenimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maquinaria/Mantenimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdReportador,IdOrigen,Descripcion,Activo,FechaReporte,FechaEstimada")] Mantenimiento Mantenimiento)
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComentario(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
                if (persona.EjecucionCorrecta)
                {
                    comentario.IdComentador = persona.Respuesta.Id;
                    ComentarioServicio servicio = new ComentarioServicio();
                    RespuestaServicio<ComentarioView> respuesta = servicio.PostComentario(comentario);

                }
                else
                {

                }

            }

            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "Mantenimiento", action = "Details", Id = comentario.IdMantenimiento }));
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNotificacionSAP(int id, string NotificacionSAP)
        {

            if (ModelState.IsValid)
            {
                MantenimientoServicio servicio = new MantenimientoServicio();
                RespuestaServicio<MantenimientoView> _Mantenimiento = servicio.PutMantenimiento(id, NotificacionSAP);

                if (_Mantenimiento == null)
                {
                    return HttpNotFound();
                }
            }

            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "Mantenimiento", action = "Details", Id = id }));
        }

        // GET: Maquinaria/Mantenimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idMantenimiento = (int)id;
            MantenimientoServicio servicio = new MantenimientoServicio();
            RespuestaServicio<MantenimientoView> Mantenimiento = servicio.GetMantenimiento(idMantenimiento);
            if (Mantenimiento == null)
            {
                return HttpNotFound();
            }

            return View(Mantenimiento.Respuesta);
        }

        // POST: Maquinaria/Mantenimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdReportador,IdOrigen,Descripcion,Activo,FechaReporte,FechaEstimada")] Mantenimiento Mantenimiento)
        {
            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", Mantenimiento.IdOrigen);
            ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre", Mantenimiento.IdReportador);
            return View();
        }

        // GET: Maquinaria/Mantenimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Maquinaria/Mantenimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MantenimientoServicio servicio = new MantenimientoServicio();
                servicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
