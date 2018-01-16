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
    public class VolumenesDeProduccionController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/VolumenesDeProduccion
        public ActionResult Index()
        {
            var volumenesDeProduccion = db.VolumenesDeProduccion.Include(v => v.MarcaDelCigarrillo).Include(v => v.Reportante).Include(v => v.WorkCenter);
            return View(volumenesDeProduccion.ToList());
        }

        // GET: Operaciones/VolumenesDeProduccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return HttpNotFound();
            }
            return View(volumenDeProduccion);
        }

        // GET: Operaciones/VolumenesDeProduccion/Create
        public ActionResult Create()
        {
            ViewBag.IdMarca = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/VolumenesDeProduccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cantidad,Fecha,IdPersona,IdWorkCenter,IdMarca")] VolumenDeProduccion volumenDeProduccion)
        {
            if (ModelState.IsValid)
            {
                db.VolumenesDeProduccion.Add(volumenDeProduccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMarca = new SelectList(db.Marcas, "Id", "Nombre", volumenDeProduccion.IdMarca);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", volumenDeProduccion.IdPersona);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", volumenDeProduccion.IdWorkCenter);
            return View(volumenDeProduccion);
        }

        // GET: Operaciones/VolumenesDeProduccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "Id", "Nombre", volumenDeProduccion.IdMarca);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", volumenDeProduccion.IdPersona);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", volumenDeProduccion.IdWorkCenter);
            return View(volumenDeProduccion);
        }

        // POST: Operaciones/VolumenesDeProduccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cantidad,Fecha,IdPersona,IdWorkCenter,IdMarca")] VolumenDeProduccion volumenDeProduccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volumenDeProduccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "Id", "Nombre", volumenDeProduccion.IdMarca);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", volumenDeProduccion.IdPersona);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", volumenDeProduccion.IdWorkCenter);
            return View(volumenDeProduccion);
        }

        // GET: Operaciones/VolumenesDeProduccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return HttpNotFound();
            }
            return View(volumenDeProduccion);
        }

        // POST: Operaciones/VolumenesDeProduccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            db.VolumenesDeProduccion.Remove(volumenDeProduccion);
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
