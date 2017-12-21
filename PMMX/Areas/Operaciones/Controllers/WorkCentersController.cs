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

        // GET: Maquinaria/WorkCenters
        public ActionResult Index()
        {
            var workCenter = db.WorkCenters.Include(w => w.BussinesUnit);
            return View(workCenter.ToList());
        }

        // GET: Maquinaria/WorkCenters/Details/5
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

        // GET: Maquinaria/WorkCenters/Create
        public ActionResult Create()
        {
            ViewBag.IdBussinesUnit = new SelectList(db.BussinesUnits, "Id", "Nombre");
            return View();
        }

        // POST: Maquinaria/WorkCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Maquinaria/WorkCenters/Edit/5
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
            return View(workCenter);
        }

        // POST: Maquinaria/WorkCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdBussinesUnit,Nombre,NombreCorto,Activo")] WorkCenter workCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workCenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBussinesUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", workCenter.IdBussinesUnit);
            return View(workCenter);
        }

        // GET: Maquinaria/WorkCenters/Delete/5
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

        // POST: Maquinaria/WorkCenters/Delete/5
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
