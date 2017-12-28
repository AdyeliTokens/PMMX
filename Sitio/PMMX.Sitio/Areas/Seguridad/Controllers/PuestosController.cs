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
    [Authorize]
    public class PuestosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Puestos
        public ActionResult Index()
        {
            return View(db.Puestos.ToList());
        }

        // GET: Seguridad/Puestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // GET: Seguridad/Puestos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seguridad/Puestos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Activo")] Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                db.Puestos.Add(puesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(puesto);
        }

        // GET: Seguridad/Puestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // POST: Seguridad/Puestos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Activo")] Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(puesto);
        }

        // GET: Seguridad/Puestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puesto puesto = db.Puestos.Find(id);
            if (puesto == null)
            {
                return HttpNotFound();
            }
            return View(puesto);
        }

        // POST: Seguridad/Puestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Puesto puesto = db.Puestos.Find(id);
            db.Puestos.Remove(puesto);
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
