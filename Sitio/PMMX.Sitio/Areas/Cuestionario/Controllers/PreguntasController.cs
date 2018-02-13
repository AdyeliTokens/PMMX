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
using System.Web.Http.Description;

namespace Sitio.Areas.Cuestionario.Controllers
{
    [Authorize]
    public class PreguntasController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Account/Preguntass
        public ActionResult Index()
        {
            return View(db.Preguntas.ToList());
        }
        
        // GET: Account/Preguntass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta Preguntas = db.Preguntas.Find(id);
            if (Preguntas == null)
            {
                return HttpNotFound();
            }
            return View(Preguntas);
        }

        // GET: Account/Preguntass/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Preguntass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta Preguntas)
        {
            if (ModelState.IsValid)
            {
                db.Preguntas.Add(Preguntas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Preguntas);
        }

        // GET: Account/Preguntass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta Preguntas = db.Preguntas.Find(id);
            if (Preguntas == null)
            {
                return HttpNotFound();
            }
            return View(Preguntas);
        }

        // POST: Account/Preguntass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pregunta Preguntas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Preguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Preguntas);
        }

        // GET: Account/Preguntass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta Preguntas = db.Preguntas.Find(id);
            if (Preguntas == null)
            {
                return HttpNotFound();
            }
            return View(Preguntas);
        }

        // POST: Account/Preguntass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pregunta Preguntas = db.Preguntas.Find(id);
            db.Preguntas.Remove(Preguntas);
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
