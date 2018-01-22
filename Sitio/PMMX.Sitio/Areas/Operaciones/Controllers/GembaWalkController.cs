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
    public class GembaWalkController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/GembaWalk
        public ActionResult Index()
        {
            GembaWalkServicio servicio = new GembaWalkServicio();
            var GembaWalk = servicio.GetGembaWalk();

            return View(GembaWalk.Respuesta.ToList());
        }

        // GET: Maquinaria/GembaWalk/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int IdGembaWalk = (int)id;
            GembaWalkServicio servicio = new GembaWalkServicio();
            RespuestaServicio<GembaWalkView> GembaWalk = servicio.GetGembaWalk(IdGembaWalk);
            if (GembaWalk == null)
            {
                return HttpNotFound();
            }
            return View(GembaWalk.Respuesta);
        }

        // GET: Maquinaria/GembaWalk/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maquinaria/GembaWalk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GembaWalk GembaWalk)
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

            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "GembaWalk", action = "Details", Id = comentario.IdGembaWalk }));
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNotificacionSAP(int id, string NotificacionSAP)
        {

            if (ModelState.IsValid)
            {
                GembaWalkServicio servicio = new GembaWalkServicio();
                RespuestaServicio<GembaWalkView> _GembaWalk = servicio.PutGembaWalk(id, NotificacionSAP);

                if (_GembaWalk == null)
                {
                    return HttpNotFound();
                }
            }

            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "GembaWalk", action = "Details", Id = id }));
        }

        // GET: Maquinaria/GembaWalk/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int IdGembaWalk = (int)id;
            GembaWalkServicio servicio = new GembaWalkServicio();
            RespuestaServicio<GembaWalkView> GembaWalk = servicio.GetGembaWalk(IdGembaWalk);
            if (GembaWalk == null)
            {
                return HttpNotFound();
            }
            
            return View(GembaWalk.Respuesta);
        }

        // POST: Maquinaria/GembaWalk/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GembaWalk GembaWalk)
        {
            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", GembaWalk.IdOrigen);
            ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre", GembaWalk.IdReportador);
            ViewBag.IdSubCategoria = new SelectList(db.SubCategoria, "Id", "Id", GembaWalk.IdSubCategoria);
            return View();
        }

        // GET: Maquinaria/GembaWalk/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Maquinaria/GembaWalk/Delete/5
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
                GembaWalkServicio servicio = new GembaWalkServicio();
                servicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
