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
    public class LocacionController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/Locacion
        public ActionResult Index()
        {
            return View(db.Locacion.ToList());
        }

        // GET: Warehouse/Locacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacion locacion = db.Locacion.Find(id);
            if (locacion == null)
            {
                return HttpNotFound();
            }
            return View(locacion);
        }

        // GET: Warehouse/Locacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Locacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreCorto,Nombre,Activo")] Locacion locacion)
        {
            if (ModelState.IsValid)
            {
                db.Locacion.Add(locacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locacion);
        }

        // GET: Warehouse/Locacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacion locacion = db.Locacion.Find(id);
            if (locacion == null)
            {
                return HttpNotFound();
            }
            return View(locacion);
        }

        // POST: Warehouse/Locacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreCorto,Nombre,Activo")] Locacion locacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locacion);
        }

        // GET: Warehouse/Locacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacion locacion = db.Locacion.Find(id);
            if (locacion == null)
            {
                return HttpNotFound();
            }
            return View(locacion);
        }

        // POST: Warehouse/Locacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locacion locacion = db.Locacion.Find(id);
            db.Locacion.Remove(locacion);
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
