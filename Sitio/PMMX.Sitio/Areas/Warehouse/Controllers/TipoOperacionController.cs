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
    public class TipoOperacionController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/TipoOperacion
        public ActionResult Index()
        {
            return View(db.TipoOperacions.ToList());
        }

        // GET: Warehouse/TipoOperacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOperacion tipoOperacion = db.TipoOperacions.Find(id);
            if (tipoOperacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoOperacion);
        }

        // GET: Warehouse/TipoOperacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/TipoOperacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Activo")] TipoOperacion tipoOperacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoOperacions.Add(tipoOperacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoOperacion);
        }

        // GET: Warehouse/TipoOperacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOperacion tipoOperacion = db.TipoOperacions.Find(id);
            if (tipoOperacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoOperacion);
        }

        // POST: Warehouse/TipoOperacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Activo")] TipoOperacion tipoOperacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoOperacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoOperacion);
        }

        // GET: Warehouse/TipoOperacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoOperacion tipoOperacion = db.TipoOperacions.Find(id);
            if (tipoOperacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoOperacion);
        }

        // POST: Warehouse/TipoOperacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoOperacion tipoOperacion = db.TipoOperacions.Find(id);
            db.TipoOperacions.Remove(tipoOperacion);
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
