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

        // GET: Warehouse/Rechazos
        public ActionResult Index()
        {
            return View(db.Rechazos.ToList());
        }

        // GET: Warehouse/Rechazos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechazos Rechazos = db.Rechazos.Find(id);
            if (Rechazos == null)
            {
                return HttpNotFound();
            }
            return View(Rechazos);
        }

        // GET: Warehouse/Rechazos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Rechazos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Rechazos Rechazos)
        {
            if (ModelState.IsValid)
            {
                db.Rechazos.Add(Rechazos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Rechazos);
        }

        // GET: Warehouse/Rechazos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechazos Rechazos = db.Rechazos.Find(id);
            if (Rechazos == null)
            {
                return HttpNotFound();
            }
            return View(Rechazos);
        }

        // POST: Warehouse/Rechazos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Rechazos Rechazos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Rechazos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Rechazos);
        }

        // GET: Warehouse/Rechazos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechazos Rechazos = db.Rechazos.Find(id);
            if (Rechazos == null)
            {
                return HttpNotFound();
            }
            return View(Rechazos);
        }

        // POST: Warehouse/Rechazos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rechazos Rechazos = db.Rechazos.Find(id);
            db.Rechazos.Remove(Rechazos);
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
