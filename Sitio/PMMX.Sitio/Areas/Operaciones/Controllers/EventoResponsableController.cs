using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.GembaWalks;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class EventoResponsableController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/EventoResponsable
        public ActionResult Index()
        {
            var eventoResponsable = db.EventoResponsable.Include(e => e.Responsable);
            return View(eventoResponsable.ToList());
        }

        // GET: Operaciones/EventoResponsable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventoResponsable eventoResponsable = db.EventoResponsable.Find(id);
            if (eventoResponsable == null)
            {
                return HttpNotFound();
            }
            return View(eventoResponsable);
        }

        // GET: Operaciones/EventoResponsable/Create
        public ActionResult Create()
        {
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/EventoResponsable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdEvento,IdResponsable")] EventoResponsable eventoResponsable)
        {
            if (ModelState.IsValid)
            {
                db.EventoResponsable.Add(eventoResponsable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", eventoResponsable.IdResponsable);
            return View(eventoResponsable);
        }

        // GET: Operaciones/EventoResponsable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventoResponsable eventoResponsable = db.EventoResponsable.Find(id);
            if (eventoResponsable == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", eventoResponsable.IdResponsable);
            return View(eventoResponsable);
        }

        // POST: Operaciones/EventoResponsable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdEvento,IdResponsable")] EventoResponsable eventoResponsable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventoResponsable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", eventoResponsable.IdResponsable);
            return View(eventoResponsable);
        }

        // GET: Operaciones/EventoResponsable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventoResponsable eventoResponsable = db.EventoResponsable.Find(id);
            if (eventoResponsable == null)
            {
                return HttpNotFound();
            }
            return View(eventoResponsable);
        }

        // POST: Operaciones/EventoResponsable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventoResponsable eventoResponsable = db.EventoResponsable.Find(id);
            db.EventoResponsable.Remove(eventoResponsable);
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
