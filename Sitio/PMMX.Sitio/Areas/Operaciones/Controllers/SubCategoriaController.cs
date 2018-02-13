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
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class SubCategoriaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Eventos/SubCategoria
        public ActionResult Index()
        {
            var subCategoria = db.SubCategoria.Include(s => s.Responsable).Include(s => s.Categoria);
            return View(subCategoria.ToList());
        }

        public ActionResult GetSubCategoriasByCategoria(int IdCategoria)
        {
            if (ModelState.IsValid)
            {
                var lista = db.SubCategoria.Where(w => (w.IdCategoria == IdCategoria)).Select(w => new { Id = w.Id, Nombre = w.Nombre }).OrderBy(w => w.Id).ToList();
                return Json(new { lista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Eventos/SubCategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            return View(subCategoria);
        }

        // GET: Eventos/SubCategoria/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Eventos/SubCategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategoria subCategoria)
        {
            if (ModelState.IsValid)
            {
                db.SubCategoria.Add(subCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(subCategoria);
        }

        // GET: Eventos/SubCategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(subCategoria);
        }

        // POST: Eventos/SubCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategoria subCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(subCategoria);
        }

        // GET: Eventos/SubCategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            return View(subCategoria);
        }

        // POST: Eventos/SubCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategoria subCategoria = db.SubCategoria.Find(id);
            db.SubCategoria.Remove(subCategoria);
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
