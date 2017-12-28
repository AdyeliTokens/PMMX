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
    public class CarrierController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/Carrier
        public ActionResult Index()
        {
            return View(db.Carrier.ToList());
        }

        // GET: Warehouse/Carrier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrier carrier = db.Carrier.Find(id);
            if (carrier == null)
            {
                return HttpNotFound();
            }
            return View(carrier);
        }

        // GET: Warehouse/Carrier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Carrier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Carrier carrier)
        {
            if (ModelState.IsValid)
            {
                db.Carrier.Add(carrier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carrier);
        }

        // GET: Warehouse/Carrier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrier carrier = db.Carrier.Find(id);
            if (carrier == null)
            {
                return HttpNotFound();
            }
            return View(carrier);
        }

        // POST: Warehouse/Carrier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Carrier carrier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carrier);
        }

        // GET: Warehouse/Carrier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrier carrier = db.Carrier.Find(id);
            if (carrier == null)
            {
                return HttpNotFound();
            }
            return View(carrier);
        }

        // POST: Warehouse/Carrier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrier carrier = db.Carrier.Find(id);
            db.Carrier.Remove(carrier);
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
