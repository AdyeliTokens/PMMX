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


namespace Sitio.Areas.Operaciones
{
    public class ListaDistribucionController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/ListaDistribucion
        public ActionResult Index()
        {
            var ListaDistribucion = db.ListaDistribucion.Include(l => l.Remitente).Include( l => l.SubArea);
            return View(ListaDistribucion.ToList());
        }

        // GET: Operaciones/ListaDistribucion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDistribucion listaDistribucion = db.ListaDistribucion.Find(id);
            if (listaDistribucion == null)
            {
                return HttpNotFound();
            }
            return View(listaDistribucion);
        }

        // GET: Operaciones/ListaDistribucion/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.Area, "Id", "Nombre");
            ViewBag.IdSubarea = new SelectList(db.SubArea, "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/ListaDistribucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListaDistribucion listaDistribucion)
        {
            if (ModelState.IsValid)
            {
                db.ListaDistribucion.Add(listaDistribucion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.IdSubarea = new SelectList(db.SubArea, "Id", "Nombre", listaDistribucion.IdSubarea);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", listaDistribucion.IdPersona);
            return View(listaDistribucion);
        }

        // GET: Operaciones/ListaDistribucion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDistribucion listaDistribucion = db.ListaDistribucion.Find(id);
            if (listaDistribucion == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.IdSubarea = new SelectList(db.SubArea, "Id", "Nombre", listaDistribucion.IdSubarea);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", listaDistribucion.IdPersona);
            return View(listaDistribucion);
        }

        // POST: Operaciones/ListaDistribucion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ListaDistribucion listaDistribucion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listaDistribucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSubarea = new SelectList(db.SubArea, "Id", "Nombre", listaDistribucion.IdSubarea);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", listaDistribucion.IdPersona);
            return View(listaDistribucion);
        }

        // GET: Operaciones/ListaDistribucion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaDistribucion listaDistribucion = db.ListaDistribucion.Find(id);
            if (listaDistribucion == null)
            {
                return HttpNotFound();
            }
            return View(listaDistribucion);
        }

        // POST: Operaciones/ListaDistribucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListaDistribucion listaDistribucion = db.ListaDistribucion.Find(id);
            db.ListaDistribucion.Remove(listaDistribucion);
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
