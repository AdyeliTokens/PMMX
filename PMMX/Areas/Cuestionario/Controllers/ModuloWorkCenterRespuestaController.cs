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
using PMMX.Modelo;

namespace Sitio.Areas.Cuestionario.Controllers
{
    [Authorize]
    public class OrigenRespuestaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Account/PreguntaTurnoss
        public ActionResult Index()
        {
            return View(db.OrigenRespuesta.ToList());
        }

        // GET: Account/PreguntaTurnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrigenRespuesta MWCR = db.OrigenRespuesta.Find(id);
            if (MWCR == null)
            {
                return HttpNotFound();
            }
            return View(MWCR);
        }

        // GET: Account/PreguntaTurnoss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/PreguntaTurnoss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Activo")] OrigenRespuesta MWCR)
        {
            if (ModelState.IsValid)
            {
                db.OrigenRespuesta.Add(MWCR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(MWCR);
        }

        // GET: Account/PreguntaTurnoss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrigenRespuesta MWCR = db.OrigenRespuesta.Find(id);
            if (MWCR == null)
            {
                return HttpNotFound();
            }
            return View(MWCR);
        }

        // POST: Account/PreguntaTurnoss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Activo")] OrigenRespuesta MWCR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(MWCR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(MWCR);
        }

        // GET: Account/PreguntaTurnoss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrigenRespuesta MWCR = db.OrigenRespuesta.Find(id);
            if (MWCR == null)
            {
                return HttpNotFound();
            }
            return View(MWCR);
        }

        // POST: Account/PreguntaTurnoss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrigenRespuesta MWCR = db.OrigenRespuesta.Find(id);
            db.OrigenRespuesta.Remove(MWCR);
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
