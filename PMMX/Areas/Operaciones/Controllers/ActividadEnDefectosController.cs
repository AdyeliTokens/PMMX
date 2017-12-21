using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Defectos;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class ActividadEnDefectosController : Controller
    {

        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/ActividadEnDefectos
        public ActionResult Index()
        {
            var actividadEnDefecto = db.ActividadEnDefectos.Include(a => a.Ejecutante);
            return View(actividadEnDefecto.ToList());
        }

        // GET: Maquinaria/ActividadEnDefectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnDefecto actividadEnDefecto = db.ActividadEnDefectos.Find(id);
            if (actividadEnDefecto == null)
            {
                return HttpNotFound();
            }
            return View(actividadEnDefecto);
        }

        // GET: Maquinaria/ActividadEnDefectos/Create
        public ActionResult Create()
        {
            //ViewBag.IdEjecutante = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdEjecutante = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Maquinaria/ActividadEnDefectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdDefecto,IdEjecutante,Descripcion,Fecha")] ActividadEnDefecto actividadEnDefecto)
        {
            if (ModelState.IsValid)
            {
                db.ActividadEnDefectos.Add(actividadEnDefecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEjecutante = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(actividadEnDefecto);
        }

        // GET: Maquinaria/ActividadEnDefectos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnDefecto actividadEnDefecto = db.ActividadEnDefectos.Find(id);
            if (actividadEnDefecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEjecutante = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(actividadEnDefecto);
        }

        // POST: Maquinaria/ActividadEnDefectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdDefecto,IdEjecutante,Descripcion,Fecha")] ActividadEnDefecto actividadEnDefecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividadEnDefecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEjecutante = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(actividadEnDefecto);
        }

        // GET: Maquinaria/ActividadEnDefectos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnDefecto actividadEnDefecto = db.ActividadEnDefectos.Find(id);
            if (actividadEnDefecto == null)
            {
                return HttpNotFound();
            }
            return View(actividadEnDefecto);
        }

        // POST: Maquinaria/ActividadEnDefectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActividadEnDefecto actividadEnDefecto = db.ActividadEnDefectos.Find(id);
            db.ActividadEnDefectos.Remove(actividadEnDefecto);
            db.SaveChanges();
            return RedirectToAction("Index");
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
