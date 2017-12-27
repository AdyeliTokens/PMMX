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
    public class DesperdiciosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/Desperdicios
        public ActionResult Index()
        {
            var desperdicios = db.Desperdicios.Include(d => d.Seccion);
            return View(desperdicios.ToList());
        }

        // GET: Operaciones/Desperdicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            return View(desperdicio);
        }

        // GET: Operaciones/Desperdicios/Create
        public ActionResult Create()
        {
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/Desperdicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cantidad,Fecha,IdPersona,IdWorkCenter,IdSeccion,IdMarca")] Desperdicio desperdicio)
        {
            if (ModelState.IsValid)
            {
                db.Desperdicios.Add(desperdicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            return View(desperdicio);
        }

        // GET: Operaciones/Desperdicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            return View(desperdicio);
        }

        // POST: Operaciones/Desperdicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cantidad,Fecha,IdPersona,IdWorkCenter,IdSeccion,IdMarca")] Desperdicio desperdicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(desperdicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            return View(desperdicio);
        }

        // GET: Operaciones/Desperdicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            return View(desperdicio);
        }

        // POST: Operaciones/Desperdicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            db.Desperdicios.Remove(desperdicio);
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
