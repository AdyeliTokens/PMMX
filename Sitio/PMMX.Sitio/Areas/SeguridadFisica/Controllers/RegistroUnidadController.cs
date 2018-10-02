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
    public class RegistroUnidadController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: SeguridadFisica/RegistroUnidad
        public ActionResult Index()
        {
            var registroUnidad = db.RegistroUnidad.Include(r => r.Formato);
            return View(registroUnidad.ToList());
        }

        // GET: SeguridadFisica/RegistroUnidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroUnidad registroUnidad = db.RegistroUnidad.Find(id);
            if (registroUnidad == null)
            {
                return HttpNotFound();
            }
            return View(registroUnidad);
        }

        // GET: SeguridadFisica/RegistroUnidad/Create
        public ActionResult Create()
        {
            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo");
            return View();
        }

        // POST: SeguridadFisica/RegistroUnidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Empresa,Asunto,NoGafette,IdFormato")] RegistroUnidad registroUnidad)
        {
            if (ModelState.IsValid)
            {
                db.RegistroUnidad.Add(registroUnidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo", registroUnidad.IdFormato);
            return View(registroUnidad);
        }

        // GET: SeguridadFisica/RegistroUnidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroUnidad registroUnidad = db.RegistroUnidad.Find(id);
            if (registroUnidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo", registroUnidad.IdFormato);
            return View(registroUnidad);
        }

        // POST: SeguridadFisica/RegistroUnidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Empresa,Asunto,NoGafette,IdFormato")] RegistroUnidad registroUnidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroUnidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo", registroUnidad.IdFormato);
            return View(registroUnidad);
        }

        // GET: SeguridadFisica/RegistroUnidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroUnidad registroUnidad = db.RegistroUnidad.Find(id);
            if (registroUnidad == null)
            {
                return HttpNotFound();
            }
            return View(registroUnidad);
        }

        // POST: SeguridadFisica/RegistroUnidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistroUnidad registroUnidad = db.RegistroUnidad.Find(id);
            db.RegistroUnidad.Remove(registroUnidad);
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
