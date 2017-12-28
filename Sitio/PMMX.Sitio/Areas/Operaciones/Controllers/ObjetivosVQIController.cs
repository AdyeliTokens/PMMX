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
    public class ObjetivosVQIController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/ObjetivosVQI
        public ActionResult Index()
        {
            var objetivosVQI = db.ObjetivosVQI.Include(o => o.WorkCenter);
            return View(objetivosVQI.ToList());
        }

        // GET: Operaciones/ObjetivosVQI/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoVQI objetivoVQI = db.ObjetivosVQI.Find(id);
            if (objetivoVQI == null)
            {
                return HttpNotFound();
            }
            return View(objetivoVQI);
        }

        // GET: Operaciones/ObjetivosVQI/Create
        public ActionResult Create()
        {
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/ObjetivosVQI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdWorkCenter,Objetivo,FechaInicial")] ObjetivoVQI objetivoVQI)
        {
            if (ModelState.IsValid)
            {
                db.ObjetivosVQI.Add(objetivoVQI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoVQI.IdWorkCenter);
            return View(objetivoVQI);
        }

        // GET: Operaciones/ObjetivosVQI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoVQI objetivoVQI = db.ObjetivosVQI.Find(id);
            if (objetivoVQI == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoVQI.IdWorkCenter);
            return View(objetivoVQI);
        }

        // POST: Operaciones/ObjetivosVQI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdWorkCenter,Objetivo,FechaInicial")] ObjetivoVQI objetivoVQI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivoVQI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoVQI.IdWorkCenter);
            return View(objetivoVQI);
        }

        // GET: Operaciones/ObjetivosVQI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoVQI objetivoVQI = db.ObjetivosVQI.Find(id);
            if (objetivoVQI == null)
            {
                return HttpNotFound();
            }
            return View(objetivoVQI);
        }

        // POST: Operaciones/ObjetivosVQI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObjetivoVQI objetivoVQI = db.ObjetivosVQI.Find(id);
            db.ObjetivosVQI.Remove(objetivoVQI);
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
