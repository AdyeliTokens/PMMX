using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Paros;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class ActividadEnParosController : Controller
    {

        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/ActividadEnParos
        public ActionResult Index()
        {
            var actividadEnParo = db.ActividadEnParos.Include(a => a.Ejecutante);
            return View(actividadEnParo.ToList());
        }

        // GET: Maquinaria/ActividadEnParos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnParo actividadEnParo = db.ActividadEnParos.Find(id);
            if (actividadEnParo == null)
            {
                return HttpNotFound();
            }
            return View(actividadEnParo);
        }

        // GET: Maquinaria/ActividadEnParos/Create
        public ActionResult Create()
        {
            //ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Maquinaria/ActividadEnParos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPersona,IdParo,Descripcion,Fecha")] ActividadEnParo actividadEnParo)
        {
            if (ModelState.IsValid)
            {
                db.ActividadEnParos.Add(actividadEnParo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPersona = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(actividadEnParo);
        }

        // GET: Maquinaria/ActividadEnParos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnParo actividadEnParo = db.ActividadEnParos.Find(id);
            if (actividadEnParo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(actividadEnParo);
        }

        // POST: Maquinaria/ActividadEnParos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPersona,IdParo,Descripcion,Fecha")] ActividadEnParo actividadEnParo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividadEnParo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersona = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(actividadEnParo);
        }

        // GET: Maquinaria/ActividadEnParos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnParo actividadEnParo = db.ActividadEnParos.Find(id);
            if (actividadEnParo == null)
            {
                return HttpNotFound();
            }
            return View(actividadEnParo);
        }

        // POST: Maquinaria/ActividadEnParos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActividadEnParo actividadEnParo = db.ActividadEnParos.Find(id);
            db.ActividadEnParos.Remove(actividadEnParo);
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
