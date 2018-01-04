using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System.Web.Routing;
using PMMX.Modelo.Entidades;
using PMMX.Seguridad.Servicios;
using Microsoft.AspNet.Identity;
using PMMX.Modelo.Entidades.Operaciones;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class JustDoItController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/JustDoIt
        public ActionResult Index()
        {
            JustDoItServicio servicio = new JustDoItServicio();
            var JustDoIt = servicio.GetJustDoIt();

            return View(JustDoIt.Respuesta.ToList());
        }

        // GET: Maquinaria/JustDoIt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idJustDoIt = (int)id;
            JustDoItServicio servicio = new JustDoItServicio();
            RespuestaServicio<JustDoItView> JustDoIt = servicio.GetJustDoIt(idJustDoIt);
            if (JustDoIt == null)
            {
                return HttpNotFound();
            }
            return View(JustDoIt.Respuesta);
        }

        // GET: Maquinaria/JustDoIt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maquinaria/JustDoIt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JustDoIt JustDoIt)
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

            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "JustDoIt", action = "Details", Id = comentario.IdJustDoIt }));
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNotificacionSAP(int id, string NotificacionSAP)
        {

            if (ModelState.IsValid)
            {
                JustDoItServicio servicio = new JustDoItServicio();
                RespuestaServicio<JustDoItView> _JustDoIt = servicio.PutJustDoIt(id, NotificacionSAP);

                if (_JustDoIt == null)
                {
                    return HttpNotFound();
                }
            }

            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "JustDoIt", action = "Details", Id = id }));
        }

        // GET: Maquinaria/JustDoIt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idJustDoIt = (int)id;
            JustDoItServicio servicio = new JustDoItServicio();
            RespuestaServicio<JustDoItView> JustDoIt = servicio.GetJustDoIt(idJustDoIt);
            if (JustDoIt == null)
            {
                return HttpNotFound();
            }
            
            return View(JustDoIt.Respuesta);
        }

        // POST: Maquinaria/JustDoIt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JustDoIt justDoIt)
        {
            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", justDoIt.IdOrigen);
            ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre", justDoIt.IdReportador);
            ViewBag.IdCategoria = new SelectList(db.Categoria, "Id", "Id", justDoIt.IdCategoria);
            return View();
        }

        // GET: Maquinaria/JustDoIt/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Maquinaria/JustDoIt/Delete/5
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
                JustDoItServicio servicio = new JustDoItServicio();
                servicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
