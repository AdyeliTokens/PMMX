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
    public class ObjetivosPlanAttainmentController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/ObjetivosPlanAttainment
        public ActionResult Index()
        {
            var objetivosPlanAttainment = db.ObjetivosPlanAttainment.Include(o => o.WorkCenter);
            return View(objetivosPlanAttainment.ToList());
        }

        // GET: Operaciones/ObjetivosPlanAttainment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoPlanAttainment objetivoPlanAttainment = db.ObjetivosPlanAttainment.Find(id);
            if (objetivoPlanAttainment == null)
            {
                return HttpNotFound();
            }
            return View(objetivoPlanAttainment);
        }

        // GET: Operaciones/ObjetivosPlanAttainment/Create
        public ActionResult Create()
        {
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/ObjetivosPlanAttainment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdWorkCenter,Objetivo,FechaInicial")] ObjetivoPlanAttainment objetivoPlanAttainment)
        {
            if (ModelState.IsValid)
            {
                db.ObjetivosPlanAttainment.Add(objetivoPlanAttainment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoPlanAttainment.IdWorkCenter);
            return View(objetivoPlanAttainment);
        }

        // GET: Operaciones/ObjetivosPlanAttainment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoPlanAttainment objetivoPlanAttainment = db.ObjetivosPlanAttainment.Find(id);
            if (objetivoPlanAttainment == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoPlanAttainment.IdWorkCenter);
            return View(objetivoPlanAttainment);
        }

        // POST: Operaciones/ObjetivosPlanAttainment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdWorkCenter,Objetivo,FechaInicial")] ObjetivoPlanAttainment objetivoPlanAttainment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivoPlanAttainment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoPlanAttainment.IdWorkCenter);
            return View(objetivoPlanAttainment);
        }

        // GET: Operaciones/ObjetivosPlanAttainment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoPlanAttainment objetivoPlanAttainment = db.ObjetivosPlanAttainment.Find(id);
            if (objetivoPlanAttainment == null)
            {
                return HttpNotFound();
            }
            return View(objetivoPlanAttainment);
        }

        // POST: Operaciones/ObjetivosPlanAttainment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObjetivoPlanAttainment objetivoPlanAttainment = db.ObjetivosPlanAttainment.Find(id);
            db.ObjetivosPlanAttainment.Remove(objetivoPlanAttainment);
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
