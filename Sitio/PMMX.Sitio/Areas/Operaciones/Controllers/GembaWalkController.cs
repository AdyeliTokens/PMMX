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

            ViewBag.Estatus = db.BitacoraGembaWalks.Where(x => x.IdGembaWalk == GembaWalk.Respuesta.Id).OrderByDescending(x => x.Fecha).Select(x => x.Estatus.Nombre).FirstOrDefault().ToString();
            
            if (GembaWalk == null)
            {
                return HttpNotFound();
            }
            return View(GembaWalk.Respuesta);
        }

        // GET: Maquinaria/GembaWalk/Create
        public ActionResult Create()
        {
            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id");
            return View();
        }

        // POST: Maquinaria/GembaWalk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GembaWalk gemba)
        {
            if (ModelState.IsValid)
            {
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
                if (persona.EjecucionCorrecta)
                {
                    gemba.IdReporta = persona.Respuesta.Id;
                    gemba.IdSubCategoria = 22;
                    gemba.Prioridad = 1;
                    db.GembaWalk.Add(gemba);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id"); 
            return View(gemba);
        }

        [HttpPost]
        public ActionResult ChangeStatus(PMMX.Modelo.Entidades.Operaciones.BitacoraGembaWalk bitacora)
        {
            if (ModelState.IsValid)
            {
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
                if (persona.EjecucionCorrecta)
                {
                    bitacora.IdResponsable = persona.Respuesta.Id;
                    bitacora.Fecha = DateTime.Now;
                    bitacora.Comentario = " ";
                    db.BitacoraGembaWalks.Add(bitacora);
                    db.SaveChanges();
                    return Json(new { status = 200 });
                }
            }
            return Json(new { status = 400 });
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
            ViewBag.IdReporta = new SelectList(db.Personas, "Id", "Nombre", GembaWalk.IdReporta);
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
