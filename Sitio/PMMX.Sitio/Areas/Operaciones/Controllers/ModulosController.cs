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
    public class ModulosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/Modulos
        public ActionResult Index()
        {
            var modulos = db.Modulos.Include(m => m.Seccion);
            return View(modulos.ToList());
        }

        // GET: Operaciones/Modulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Modulos.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // GET: Operaciones/Modulos/Create
        public ActionResult Create()
        {
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/Modulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,NombreCorto,IdSeccion,Activo")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Modulos.Add(modulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", modulo.IdSeccion);
            return View(modulo);
        }

        // GET: Operaciones/Modulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Modulos.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", modulo.IdSeccion);
            return View(modulo);
        }

        // POST: Operaciones/Modulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,NombreCorto,IdSeccion,Activo")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", modulo.IdSeccion);
            return View(modulo);
        }

        // GET: Operaciones/Modulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Modulos.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Operaciones/Modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modulo modulo = db.Modulos.Find(id);
            db.Modulos.Remove(modulo);
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
