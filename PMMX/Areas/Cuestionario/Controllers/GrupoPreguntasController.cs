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
    public class GrupoPreguntasController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Account/GrupoPreguntas
        public ActionResult Index()
        {
            return View(db.GrupoPreguntas.ToList());
        }

        // GET: Account/GrupoPreguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoPreguntas GrupoPreguntas = db.GrupoPreguntas.Find(id);
            if (GrupoPreguntas == null)
            {
                return HttpNotFound();
            }
            return View(GrupoPreguntas);
        }

        // GET: Account/GrupoPreguntas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/GrupoPreguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,DDS,IdCategoria,Activo")] GrupoPreguntas GrupoPreguntas)
        {
            if (ModelState.IsValid)
            {
                db.GrupoPreguntas.Add(GrupoPreguntas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(GrupoPreguntas);
        }

        // GET: Account/GrupoPreguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoPreguntas GrupoPreguntas = db.GrupoPreguntas.Find(id);
            if (GrupoPreguntas == null)
            {
                return HttpNotFound();
            }
            return View(GrupoPreguntas);
        }

        // POST: Account/GrupoPreguntas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,DDS,IdCategoria,Activo")] Respuesta GrupoPreguntas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(GrupoPreguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(GrupoPreguntas);
        }

        // GET: Account/GrupoPreguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoPreguntas GrupoPreguntas = db.GrupoPreguntas.Find(id);
            if (GrupoPreguntas == null)
            {
                return HttpNotFound();
            }
            return View(GrupoPreguntas);
        }

        // POST: Account/GrupoPreguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrupoPreguntas GrupoPreguntas = db.GrupoPreguntas.Find(id);
            db.GrupoPreguntas.Remove(GrupoPreguntas);
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
