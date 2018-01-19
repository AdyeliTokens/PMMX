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
    public class EstatusController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/Estatus
        public ActionResult Index()
        {
            return View(db.Estatus.ToList());
        }

        // GET: Operaciones/Estatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatus Estatus = db.Estatus.Find(id);
            if (Estatus == null)
            {
                return HttpNotFound();
            }
            return View(Estatus);
        }

        // GET: Operaciones/Estatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Operaciones/Estatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Estatus Estatus)
        {
            if (ModelState.IsValid)
            {
                db.Estatus.Add(Estatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Estatus);
        }

        // GET: Operaciones/Estatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatus Estatus = db.Estatus.Find(id);
            if (Estatus == null)
            {
                return HttpNotFound();
            }
            return View(Estatus);
        }

        // POST: Operaciones/Estatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Estatus Estatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Estatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Estatus);
        }

        // GET: Operaciones/Estatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatus Estatus = db.Estatus.Find(id);
            if (Estatus == null)
            {
                return HttpNotFound();
            }
            return View(Estatus);
        }

        // POST: Operaciones/Estatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estatus Estatus = db.Estatus.Find(id);
            db.Estatus.Remove(Estatus);
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
