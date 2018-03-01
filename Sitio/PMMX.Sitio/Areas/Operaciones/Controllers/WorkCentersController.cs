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
using PMMX.Modelo.Entidades;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class WorkCentersController : Controller
    {
        private PMMXContext db = new PMMXContext();

        public ActionResult Index()
        {
            var workCenter = db.WorkCenters
                .Include(w => w.BussinesUnit)
                .Include(w => w.Responsable)
                .Include(w => w.Origenes)
            .Include(w => w.Operadores);

            return View(workCenter.ToList());
        }

        public ActionResult Modulos(int idWorkCenter)
        {
            var modulos  = db.Origens
                .Include(w => w.Modulo).Where(w=> w.IdWorkCenter == idWorkCenter);

            return View(modulos.ToList());
        }

        public ActionResult Operadores(int idWorkCenter)
        {
            var operadores = db.Operadores
                .Include(w => w.Operador).Where(w => w.IdWorkCenter == idWorkCenter);

            return View(operadores.ToList());
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
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkCenter workCenter)
        {
            if (ModelState.IsValid)
            {
                db.WorkCenters.Add(workCenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBussinesUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", workCenter.IdBussinesUnit);
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre", workCenter.IdResponsable);

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
