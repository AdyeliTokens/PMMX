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
    public class BitacoraUnidadController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: SeguridadFisica/BitacoraUnidad
        public ActionResult Index()
        {
            var bitacoraUnidad = db.BitacoraUnidad.Include(b => b.Persona).Include(b => b.RegistroUnidad);
            return View(bitacoraUnidad.ToList());
        }

        // GET: SeguridadFisica/BitacoraUnidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraUnidad bitacoraUnidad = db.BitacoraUnidad.Find(id);
            if (bitacoraUnidad == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraUnidad);
        }

        // GET: SeguridadFisica/BitacoraUnidad/Create
        public ActionResult Create()
        {
            ViewBag.IdGuardia = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa");
            return View();
        }

        // POST: SeguridadFisica/BitacoraUnidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdGuardia,Puerta,Fecha,IdRegistroUnidad")] BitacoraUnidad bitacoraUnidad)
        {
            if (ModelState.IsValid)
            {
                db.BitacoraUnidad.Add(bitacoraUnidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGuardia = new SelectList(db.Personas, "Id", "Nombre", bitacoraUnidad.IdPersona);
            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa", bitacoraUnidad.IdRegistroUnidad);
            return View(bitacoraUnidad);
        }

        // GET: SeguridadFisica/BitacoraUnidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraUnidad bitacoraUnidad = db.BitacoraUnidad.Find(id);
            if (bitacoraUnidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGuardia = new SelectList(db.Personas, "Id", "Nombre", bitacoraUnidad.IdPersona);
            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa", bitacoraUnidad.IdRegistroUnidad);
            return View(bitacoraUnidad);
        }

        // POST: SeguridadFisica/BitacoraUnidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdGuardia,Puerta,Fecha,IdRegistroUnidad")] BitacoraUnidad bitacoraUnidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bitacoraUnidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGuardia = new SelectList(db.Personas, "Id", "Nombre", bitacoraUnidad.IdPersona);
            ViewBag.IdRegistroUnidad = new SelectList(db.RegistroUnidad, "Id", "Empresa", bitacoraUnidad.IdRegistroUnidad);
            return View(bitacoraUnidad);
        }

        // GET: SeguridadFisica/BitacoraUnidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraUnidad bitacoraUnidad = db.BitacoraUnidad.Find(id);
            if (bitacoraUnidad == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraUnidad);
        }

        // POST: SeguridadFisica/BitacoraUnidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BitacoraUnidad bitacoraUnidad = db.BitacoraUnidad.Find(id);
            db.BitacoraUnidad.Remove(bitacoraUnidad);
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
