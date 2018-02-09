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
            Estatus Estatus = db.Estatus.Where(x => (x.Id == id)).Include(x => x.Categoria).FirstOrDefault();
            if (Estatus == null)
            {
                return HttpNotFound();
            }
            return View(Estatus);
        }

        // GET: Operaciones/Estatus/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        public ActionResult GetEstatusbyCategoria(int IdCategoria)
        {
            if (ModelState.IsValid)
            {
                var estatus = db.Estatus.Where(w => (w.IdCategoria == IdCategoria)).Select(w => new { Id = w.Id, Nombre = w.Nombre }).OrderBy(w => w.Id).ToList();
                return Json(new { estatus }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Operaciones/Estatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estatus Estatus)
        {
            if (ModelState.IsValid)
            {
                db.Estatus.Add(Estatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(Estatus);
        }

        // GET: Operaciones/Estatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatus Estatus = db.Estatus.Where(x=> (x.Id ==id)).Include(x=> x.Categoria).FirstOrDefault();
            if (Estatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", Estatus.IdCategoria);
            return View(Estatus);
        }

        // POST: Operaciones/Estatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estatus Estatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Estatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", Estatus.IdCategoria);

            return View(Estatus);
        }

        // GET: Operaciones/Estatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estatus Estatus = db.Estatus.Where(x => (x.Id == id)).Include(x => x.Categoria).FirstOrDefault();
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
