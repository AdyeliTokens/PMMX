using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Warehouse;

namespace Sitio.Areas.Warehouse.Controllers
{
    public class RecursosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/Recursos
        public ActionResult Index()
        {
            return View(db.Recursos.ToList());
        }

        // GET: Warehouse/Recursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recursos recursos = db.Recursos.Find(id);
            if (recursos == null)
            {
                return HttpNotFound();
            }
            return View(recursos);
        }

        // GET: Warehouse/Recursos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Recursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Recursos recursos)
        {
            if (ModelState.IsValid)
            {
                db.Recursos.Add(recursos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recursos);
        }

        // GET: Warehouse/Recursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recursos recursos = db.Recursos.Find(id);
            if (recursos == null)
            {
                return HttpNotFound();
            }
            return View(recursos);
        }

        // POST: Warehouse/Recursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NombreCorto,Activo")] Recursos recursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recursos);
        }

        // GET: Warehouse/Recursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recursos recursos = db.Recursos.Find(id);
            if (recursos == null)
            {
                return HttpNotFound();
            }
            return View(recursos);
        }

        // POST: Warehouse/Recursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recursos recursos = db.Recursos.Find(id);
            db.Recursos.Remove(recursos);
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
