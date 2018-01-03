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

namespace Sitio.Areas.Operaciones
{
    public class SubAreaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/SubArea
        public ActionResult Index()
        {
            var SubArea = db.SubArea.Include(s => s.Responsable);
            return View(SubArea.ToList());
        }

        // GET: Operaciones/SubArea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubArea subArea = db.SubArea.Find(id);
            if (subArea == null)
            {
                return HttpNotFound();
            }
            return View(subArea);
        }

        // GET: Operaciones/SubArea/Create
        public ActionResult Create()
        {
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/SubArea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NombreCorto,IdResponsable,Activo")] SubArea subArea)
        {
            if (ModelState.IsValid)
            {
                db.SubArea.Add(subArea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", subArea.IdResponsable);
            return View(subArea);
        }

        // GET: Operaciones/SubArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubArea subArea = db.SubArea.Find(id);
            if (subArea == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", subArea.IdResponsable);
            return View(subArea);
        }

        // POST: Operaciones/SubArea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NombreCorto,IdResponsable,Activo")] SubArea subArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subArea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", subArea.IdResponsable);
            return View(subArea);
        }

        // GET: Operaciones/SubArea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubArea subArea = db.SubArea.Find(id);
            if (subArea == null)
            {
                return HttpNotFound();
            }
            return View(subArea);
        }

        // POST: Operaciones/SubArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubArea subArea = db.SubArea.Find(id);
            db.SubArea.Remove(subArea);
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
