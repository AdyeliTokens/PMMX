using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Warehouse;

namespace Sitio.Areas.Warehouse.Controllers
{
    public class ActividadEnVentanaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/ActividadEnVentana
        public ActionResult Index()
        {
            return View(db.ActividadEnVentana.ToList());
        }

        // GET: Warehouse/ActividadEnVentana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnVentana actividadEnVentana = db.ActividadEnVentana.Find(id);
            if (actividadEnVentana == null)
            {
                return HttpNotFound();
            }
            return View(actividadEnVentana);
        }

        // GET: Warehouse/ActividadEnVentana/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/ActividadEnVentana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NombreCorto,Activo")] ActividadEnVentana actividadEnVentana)
        {
            if (ModelState.IsValid)
            {
                db.ActividadEnVentana.Add(actividadEnVentana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actividadEnVentana);
        }

        // GET: Warehouse/ActividadEnVentana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnVentana actividadEnVentana = db.ActividadEnVentana.Find(id);
            if (actividadEnVentana == null)
            {
                return HttpNotFound();
            }
            return View(actividadEnVentana);
        }

        // POST: Warehouse/ActividadEnVentana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NombreCorto,Activo")] ActividadEnVentana actividadEnVentana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividadEnVentana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actividadEnVentana);
        }

        // GET: Warehouse/ActividadEnVentana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActividadEnVentana actividadEnVentana = db.ActividadEnVentana.Find(id);
            if (actividadEnVentana == null)
            {
                return HttpNotFound();
            }
            return View(actividadEnVentana);
        }

        // POST: Warehouse/ActividadEnVentana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActividadEnVentana actividadEnVentana = db.ActividadEnVentana.Find(id);
            db.ActividadEnVentana.Remove(actividadEnVentana);
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
