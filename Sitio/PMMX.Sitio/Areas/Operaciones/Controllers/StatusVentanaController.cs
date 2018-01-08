using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Operaciones;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class StatusVentanaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/StatusVentana
        public ActionResult Index()
        {
            var statusVentana = db.StatusVentana.Include(s => s.Responsable).Include(s => s.Status).Include(s => s.Ventana);
            return View(statusVentana.ToList());
        }

        // GET: Operaciones/StatusVentana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            if (statusVentana == null)
            {
                return HttpNotFound();
            }
            return View(statusVentana);
        }

        // GET: Operaciones/StatusVentana/Create
        public ActionResult Create()
        {
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdStatus = new SelectList(db.Status, "Id", "Nombre");
            ViewBag.IdVentana = new SelectList(db.Ventana, "Id", "PO");
            return View();
        }

        // POST: Operaciones/StatusVentana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdVentana,IdStatus,IdResponsable,Fecha")] StatusVentana statusVentana)
        {
            if (ModelState.IsValid)
            {
                db.StatusVentana.Add(statusVentana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", statusVentana.IdResponsable);
            ViewBag.IdStatus = new SelectList(db.Status, "Id", "Nombre", statusVentana.IdStatus);
            ViewBag.IdVentana = new SelectList(db.Ventana, "Id", "PO", statusVentana.IdVentana);
            return View(statusVentana);
        }

        // GET: Operaciones/StatusVentana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            if (statusVentana == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", statusVentana.IdResponsable);
            ViewBag.IdStatus = new SelectList(db.Status, "Id", "Nombre", statusVentana.IdStatus);
            ViewBag.IdVentana = new SelectList(db.Ventana, "Id", "PO", statusVentana.IdVentana);
            return View(statusVentana);
        }

        // POST: Operaciones/StatusVentana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdVentana,IdStatus,IdResponsable,Fecha")] StatusVentana statusVentana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusVentana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", statusVentana.IdResponsable);
            ViewBag.IdStatus = new SelectList(db.Status, "Id", "Nombre", statusVentana.IdStatus);
            ViewBag.IdVentana = new SelectList(db.Ventana, "Id", "PO", statusVentana.IdVentana);
            return View(statusVentana);
        }

        // GET: Operaciones/StatusVentana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            if (statusVentana == null)
            {
                return HttpNotFound();
            }
            return View(statusVentana);
        }

        // POST: Operaciones/StatusVentana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            db.StatusVentana.Remove(statusVentana);
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
