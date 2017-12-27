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
    public class BitacoraVentanaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/BitacoraVentana
        public ActionResult Index()
        {
            return View(db.BitacoraVentana.ToList());
        }

        // GET: Warehouse/BitacoraVentana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraVentana);
        }

        // GET: Warehouse/BitacoraVentana/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/BitacoraVentana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, IdVentana, IdActividadVentana, FechaInicio, FechaFin, IdResponsable, Comentarios, Activo")] BitacoraVentana bitacoraVentana)
        {
            if (ModelState.IsValid)
            {
                db.BitacoraVentana.Add(bitacoraVentana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bitacoraVentana);
        }

        // GET: Warehouse/BitacoraVentana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraVentana);
        }

        // POST: Warehouse/BitacoraVentana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, IdVentana, IdActividadVentana, FechaInicio, FechaFin, IdResponsable, Comentarios, Activo")] BitacoraVentana bitacoraVentana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bitacoraVentana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bitacoraVentana);
        }

        // GET: Warehouse/BitacoraVentana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraVentana);
        }

        // POST: Warehouse/BitacoraVentana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            db.BitacoraVentana.Remove(bitacoraVentana);
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
