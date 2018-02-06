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

        
        public ActionResult Index()
        {
            var volumenesDeProduccion = db.VolumenesDeProduccion.Include(v => v.MarcaDelCigarrillo).Include(v => v.Reportante).Include(v => v.WorkCenter);
            return View(volumenesDeProduccion.ToList());
        }

        
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

        
        public ActionResult Create()
        {
            ViewBag.IdMarca = new SelectList(db.Marcas, "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VolumenDeProduccion volumenDeProduccion)
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VolumenDeProduccion volumenDeProduccion)
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
