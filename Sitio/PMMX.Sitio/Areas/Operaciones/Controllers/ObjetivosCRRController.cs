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
    public class ObjetivosCRRController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/ObjetivosCRR
        public ActionResult Index()
        {
            var objetivosCRR = db.ObjetivosCRR.Include(o => o.WorkCenter);
            return View(objetivosCRR.ToList());
        }

        // GET: Operaciones/ObjetivosCRR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoCRR objetivoCRR = db.ObjetivosCRR.Find(id);
            if (objetivoCRR == null)
            {
                return HttpNotFound();
            }
            return View(objetivoCRR);
        }

        // GET: Operaciones/ObjetivosCRR/Create
        public ActionResult Create()
        {
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters.OrderBy(x => x.NombreCorto), "Id", "NombreCorto");
            return View();
        }

        // POST: Operaciones/ObjetivosCRR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ObjetivoCRR objetivoCRR)
        {
            if (ModelState.IsValid)
            {
                db.ObjetivosCRR.Add(objetivoCRR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters.OrderBy(x=> x.NombreCorto), "Id", "NombreCorto", objetivoCRR.IdWorkCenter);
            return View(objetivoCRR);
        }

        // GET: Operaciones/ObjetivosCRR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoCRR objetivoCRR = db.ObjetivosCRR.Find(id);
            if (objetivoCRR == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoCRR.IdWorkCenter);
            return View(objetivoCRR);
        }

        // POST: Operaciones/ObjetivosCRR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdWorkCenter,Objetivo,FechaInicial")] ObjetivoCRR objetivoCRR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivoCRR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", objetivoCRR.IdWorkCenter);
            return View(objetivoCRR);
        }

        // GET: Operaciones/ObjetivosCRR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObjetivoCRR objetivoCRR = db.ObjetivosCRR.Find(id);
            if (objetivoCRR == null)
            {
                return HttpNotFound();
            }
            return View(objetivoCRR);
        }

        // POST: Operaciones/ObjetivosCRR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObjetivoCRR objetivoCRR = db.ObjetivosCRR.Find(id);
            db.ObjetivosCRR.Remove(objetivoCRR);
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
