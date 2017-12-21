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
using PMMX.Modelo;

namespace Sitio.Areas.Cuestionario.Controllers
{
    [Authorize]
    public class PreguntaTurnoController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Account/PreguntaTurnoss
        public ActionResult Index()
        {
            return View(db.PreguntaTurno.ToList());
        }

        // GET: Account/PreguntaTurnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaTurno PreguntaTurnos = db.PreguntaTurno.Find(id);
            if (PreguntaTurnos == null)
            {
                return HttpNotFound();
            }
            return View(PreguntaTurnos);
        }

        // GET: Account/PreguntaTurnoss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/PreguntaTurnoss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Activo")] PreguntaTurno PreguntaTurnos)
        {
            if (ModelState.IsValid)
            {
                db.PreguntaTurno.Add(PreguntaTurnos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(PreguntaTurnos);
        }

        // GET: Account/PreguntaTurnoss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaTurno PreguntaTurnos = db.PreguntaTurno.Find(id);
            if (PreguntaTurnos == null)
            {
                return HttpNotFound();
            }
            return View(PreguntaTurnos);
        }

        // POST: Account/PreguntaTurnoss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Activo")] PreguntaTurno PreguntaTurnos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(PreguntaTurnos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(PreguntaTurnos);
        }

        // GET: Account/PreguntaTurnoss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaTurno PreguntaTurnos = db.PreguntaTurno.Find(id);
            if (PreguntaTurnos == null)
            {
                return HttpNotFound();
            }
            return View(PreguntaTurnos);
        }

        // POST: Account/PreguntaTurnoss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PreguntaTurno PreguntaTurnos = db.PreguntaTurno.Find(id);
            db.PreguntaTurno.Remove(PreguntaTurnos);
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
