using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class ElectricosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/Electricos
        public ActionResult Index()
        {
            var electricos = db.Electricos.Include(e => e.BusinessUnit).Include(e => e.Electrico_Persona);
            return View(electricos.ToList());
        }

        // GET: Operaciones/Electricos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electricos electricos = db.Electricos.Find(id);
            if (electricos == null)
            {
                return HttpNotFound();
            }
            return View(electricos);
        }

        // GET: Operaciones/Electricos/Create
        public ActionResult Create()
        {
            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre");
            ViewBag.IdElectrico = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/Electricos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdElectrico,IdBusinessUnit,Activo")] Electricos electricos)
        {
            if (ModelState.IsValid)
            {
                db.Electricos.Add(electricos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", electricos.IdBusinessUnit);
            ViewBag.IdElectrico = new SelectList(db.Personas, "Id", "Nombre", electricos.IdElectrico);
            return View(electricos);
        }

        // GET: Operaciones/Electricos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electricos electricos = db.Electricos.Find(id);
            if (electricos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", electricos.IdBusinessUnit);
            ViewBag.IdElectrico = new SelectList(db.Personas, "Id", "Nombre", electricos.IdElectrico);
            return View(electricos);
        }

        // POST: Operaciones/Electricos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdElectrico,IdBusinessUnit,Activo")] Electricos electricos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(electricos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", electricos.IdBusinessUnit);
            ViewBag.IdElectrico = new SelectList(db.Personas, "Id", "Nombre", electricos.IdElectrico);
            return View(electricos);
        }

        // GET: Operaciones/Electricos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electricos electricos = db.Electricos.Find(id);
            if (electricos == null)
            {
                return HttpNotFound();
            }
            return View(electricos);
        }

        // POST: Operaciones/Electricos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Electricos electricos = db.Electricos.Find(id);
            db.Electricos.Remove(electricos);
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
