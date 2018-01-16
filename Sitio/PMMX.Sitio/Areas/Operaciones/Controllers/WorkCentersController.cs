using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Maquinaria;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class WorkCentersController : Controller
    {
        private PMMXContext db = new PMMXContext();

        public ActionResult Index()
        {
            var workCenter = db.WorkCenters.Include(w => w.BussinesUnit).Include(w => w.Responsable);
            return View(workCenter.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkCenter workCenter = db.WorkCenters.Find(id);
            if (workCenter == null)
            {
                return HttpNotFound();
            }
            return View(workCenter);
        }

        public ActionResult Create()
        {
            ViewBag.IdBussinesUnit = new SelectList(db.BussinesUnits, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdBussinesUnit,Nombre,NombreCorto,Activo")] WorkCenter workCenter)
        {
            if (ModelState.IsValid)
            {
                db.WorkCenters.Add(workCenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBussinesUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", workCenter.IdBussinesUnit);
            return View(workCenter);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkCenter workCenter = db.WorkCenters.Find(id);
            if (workCenter == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBussinesUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", workCenter.IdBussinesUnit);
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre", workCenter.IdResponsable);
            return View(workCenter);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkCenter workCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workCenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBussinesUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", workCenter.IdBussinesUnit);
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre", workCenter.IdResponsable);

            return View(workCenter);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkCenter workCenter = db.WorkCenters.Find(id);
            if (workCenter == null)
            {
                return HttpNotFound();
            }
            return View(workCenter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkCenter workCenter = db.WorkCenters.Find(id);
            db.WorkCenters.Remove(workCenter);
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
