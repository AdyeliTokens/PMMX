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
    [Authorize]
    public class RechazoController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/Rechazo
        public ActionResult Index()
        {
            return View(db.Rechazo.ToList());
        }

        // GET: Warehouse/Rechazo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechazo Rechazo = db.Rechazo.Find(id);
            if (Rechazo == null)
            {
                return HttpNotFound();
            }
            return View(Rechazo);
        }

        // GET: Warehouse/Rechazo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Rechazo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rechazo Rechazo)
        {
            if (ModelState.IsValid)
            {
                db.Rechazo.Add(Rechazo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Rechazo);
        }

        // GET: Warehouse/Rechazo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechazo Rechazo = db.Rechazo.Find(id);
            if (Rechazo == null)
            {
                return HttpNotFound();
            }
            return View(Rechazo);
        }

        // POST: Warehouse/Rechazo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rechazo Rechazo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Rechazo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Rechazo);
        }

        // GET: Warehouse/Rechazo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rechazo Rechazo = db.Rechazo.Find(id);
            if (Rechazo == null)
            {
                return HttpNotFound();
            }
            return View(Rechazo);
        }

        // POST: Warehouse/Rechazo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rechazo Rechazo = db.Rechazo.Find(id);
            db.Rechazo.Remove(Rechazo);
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
