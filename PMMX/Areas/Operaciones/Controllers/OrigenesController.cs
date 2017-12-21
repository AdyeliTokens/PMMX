using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Maquinaria;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class OrigenesController : Controller
    {
        private PMMXContext db = new PMMXContext();
        
        public ActionResult Index()
        {
            var origens = db.Origens.Include(o => o.Modulo).Include(o => o.WorkCenter);
            return View(origens.ToList().OrderByDescending(x =>  x.IdWorkCenter  ));
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origen origen = db.Origens.Find(id);
            if (origen == null)
            {
                return HttpNotFound();
            }
            return View(origen);
        }
        
        public ActionResult Create()
        {
            ViewBag.IdModulo = new SelectList(db.Modulos, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdModulo,IdWorkCenter,Orden")] Origen origen)
        {
            if (ModelState.IsValid)
            {
                db.Origens.Add(origen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdModulo = new SelectList(db.Modulos, "Id", "Nombre", origen.IdModulo);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", origen.IdWorkCenter);
            return View(origen);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origen origen = db.Origens.Find(id);
            if (origen == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdModulo = new SelectList(db.Modulos, "Id", "Nombre", origen.IdModulo);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", origen.IdWorkCenter);
            return View(origen);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdModulo,IdWorkCenter,Orden")] Origen origen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(origen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdModulo = new SelectList(db.Modulos, "Id", "Nombre", origen.IdModulo);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", origen.IdWorkCenter);
            return View(origen);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Origen origen = db.Origens.Find(id);
            if (origen == null)
            {
                return HttpNotFound();
            }
            return View(origen);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Origen origen = db.Origens.Find(id);
            db.Origens.Remove(origen);
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
