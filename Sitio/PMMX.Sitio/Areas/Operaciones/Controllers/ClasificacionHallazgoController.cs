using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Data;
using System.Data.Entity;
using System.Net;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Operaciones;

namespace Sitio.Controllers
{
    public class ClasificacionHallazgoController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/ClasificacionHallazgos
        public ActionResult Index()
        {
            return View(db.ClasificacionHallazgo.ToList());
        }

        // GET: Operaciones/ClasificacionHallazgos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClasificacionHallazgo clasificacion = db.ClasificacionHallazgo.Find(id);
            if (clasificacion == null)
            {
                return HttpNotFound();
            }
            return View(clasificacion);
        }

        // GET: Operaciones/ClasificacionHallazgos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Operaciones/ClasificacionHallazgos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClasificacionHallazgo clasificacion)
        {
            if (ModelState.IsValid)
            {
                db.ClasificacionHallazgo.Add(clasificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clasificacion);
        }

        // GET: Operaciones/ClasificacionHallazgos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClasificacionHallazgo clasificacion = db.ClasificacionHallazgo.Find(id);
            if (clasificacion == null)
            {
                return HttpNotFound();
            }
            return View(clasificacion);
        }

        // POST: Operaciones/ClasificacionHallazgos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClasificacionHallazgo clasificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clasificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clasificacion);
        }

        // GET: Operaciones/ClasificacionHallazgos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClasificacionHallazgo clasificacion = db.ClasificacionHallazgo.Find(id);
            if (clasificacion == null)
            {
                return HttpNotFound();
            }
            return View(clasificacion);
        }

        // POST: Operaciones/ClasificacionHallazgos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClasificacionHallazgo clasificacion = db.ClasificacionHallazgo.Find(id);
            db.ClasificacionHallazgo.Remove(clasificacion);
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
