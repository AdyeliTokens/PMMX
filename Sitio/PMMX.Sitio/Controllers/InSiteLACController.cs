using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.InsiteLAC;

namespace Sitio.Controllers
{
    public class InSiteLACController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: InSiteLAC
        public ActionResult Index()
        {
            return View(db.KPIs.ToList());
        }

        // GET: InSiteLAC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return HttpNotFound();
            }
            return View(kPI);
        }

        // GET: InSiteLAC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InSiteLAC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,YTD,Mes_Efectivo,Anio_Efectivo")] KPI kPI)
        {
            if (ModelState.IsValid)
            {
                db.KPIs.Add(kPI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kPI);
        }

        // GET: InSiteLAC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return HttpNotFound();
            }
            return View(kPI);
        }

        // POST: InSiteLAC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,YTD,Mes_Efectivo,Anio_Efectivo")] KPI kPI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kPI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kPI);
        }

        // GET: InSiteLAC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return HttpNotFound();
            }
            return View(kPI);
        }

        // POST: InSiteLAC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KPI kPI = db.KPIs.Find(id);
            db.KPIs.Remove(kPI);
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
