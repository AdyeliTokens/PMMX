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

namespace Sitio.Areas.Seguridad.Controllers
{
    public class ShiftLeadersController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/ShiftLeaders
        public ActionResult Index()
        {
            var shiftLeaders = db.ShiftLeaders.Include(s => s.BussinesUnit).Include(s => s.ShiftLeader);
            return View(shiftLeaders.ToList());
        }

        // GET: Seguridad/ShiftLeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftLeaders shiftLeaders = db.ShiftLeaders.Find(id);
            if (shiftLeaders == null)
            {
                return HttpNotFound();
            }
            return View(shiftLeaders);
        }

        // GET: Seguridad/ShiftLeaders/Create
        public ActionResult Create()
        {
            ViewBag.IdCelula = new SelectList(db.BussinesUnits, "Id", "Nombre");
            ViewBag.IdShiftLeader = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Seguridad/ShiftLeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdShiftLeader,IdCelula,Activo")] ShiftLeaders shiftLeaders)
        {
            if (ModelState.IsValid)
            {
                db.ShiftLeaders.Add(shiftLeaders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCelula = new SelectList(db.BussinesUnits, "Id", "Nombre", shiftLeaders.IdCelula);
            ViewBag.IdShiftLeader = new SelectList(db.Personas, "Id", "Nombre", shiftLeaders.IdShiftLeader);
            return View(shiftLeaders);
        }

        // GET: Seguridad/ShiftLeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftLeaders shiftLeaders = db.ShiftLeaders.Find(id);
            if (shiftLeaders == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCelula = new SelectList(db.BussinesUnits, "Id", "Nombre", shiftLeaders.IdCelula);
            ViewBag.IdShiftLeader = new SelectList(db.Personas, "Id", "Nombre", shiftLeaders.IdShiftLeader);
            return View(shiftLeaders);
        }

        // POST: Seguridad/ShiftLeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdShiftLeader,IdCelula,Activo")] ShiftLeaders shiftLeaders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shiftLeaders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCelula = new SelectList(db.BussinesUnits, "Id", "Nombre", shiftLeaders.IdCelula);
            ViewBag.IdShiftLeader = new SelectList(db.Personas, "Id", "Nombre", shiftLeaders.IdShiftLeader);
            return View(shiftLeaders);
        }

        // GET: Seguridad/ShiftLeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftLeaders shiftLeaders = db.ShiftLeaders.Find(id);
            if (shiftLeaders == null)
            {
                return HttpNotFound();
            }
            return View(shiftLeaders);
        }

        // POST: Seguridad/ShiftLeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShiftLeaders shiftLeaders = db.ShiftLeaders.Find(id);
            db.ShiftLeaders.Remove(shiftLeaders);
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
