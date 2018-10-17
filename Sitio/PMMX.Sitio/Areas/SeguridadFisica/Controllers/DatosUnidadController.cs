using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.SeguridadFisica;

namespace Sitio.Areas.SeguridadFisica.Controllers
{
    public class DatosUnidadController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: SeguridadFisica/DatosUnidad
        public ActionResult Index()
        {
            var datosUnidad = db.DatosUnidad.Include(d => d.RegistroUnidad);
            return View(datosUnidad.ToList());
        }

        // GET: SeguridadFisica/DatosUnidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosUnidad datosUnidad = db.DatosUnidad.Find(id);
            if (datosUnidad == null)
            {
                return HttpNotFound();
            }
            return View(datosUnidad);
        }

        // GET: SeguridadFisica/DatosUnidad/Create
        public ActionResult Create()
        {
            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa");
            return View();
        }

        // POST: SeguridadFisica/DatosUnidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Placas,NoEco,NoCaja,TipoRemolque,IdRegistroUnidad")] DatosUnidad datosUnidad)
        {
            if (ModelState.IsValid)
            {
                db.DatosUnidad.Add(datosUnidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa", datosUnidad.IdRegistroUnidad);
            return View(datosUnidad);
        }

        // GET: SeguridadFisica/DatosUnidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosUnidad datosUnidad = db.DatosUnidad.Find(id);
            if (datosUnidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa", datosUnidad.IdRegistroUnidad);
            return View(datosUnidad);
        }

        // POST: SeguridadFisica/DatosUnidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Placas,NoEco,NoCaja,TipoRemolque,IdRegistroUnidad")] DatosUnidad datosUnidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datosUnidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa", datosUnidad.IdRegistroUnidad);
            return View(datosUnidad);
        }

        // GET: SeguridadFisica/DatosUnidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosUnidad datosUnidad = db.DatosUnidad.Find(id);
            if (datosUnidad == null)
            {
                return HttpNotFound();
            }
            return View(datosUnidad);
        }

        // POST: SeguridadFisica/DatosUnidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatosUnidad datosUnidad = db.DatosUnidad.Find(id);
            db.DatosUnidad.Remove(datosUnidad);
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
