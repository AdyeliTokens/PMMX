using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Defectos;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class ComentariosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Defecto);
            return View(comentarios.ToList());
        }

        // GET: Maquinaria/Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Maquinaria/Comentarios/Create
        public ActionResult Create()
        {
            ViewBag.IdDefecto = new SelectList(db.Defectos, "Id", "Descripcion");
            return View();
        }

        // POST: Maquinaria/Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdComentador,IdDefecto,Opinion,Fecha")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDefecto = new SelectList(db.Defectos, "Id", "Descripcion", comentario.IdDefecto);
            return View(comentario);
        }

        // GET: Maquinaria/Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDefecto = new SelectList(db.Defectos, "Id", "Descripcion", comentario.IdDefecto);
            return View(comentario);
        }

        // POST: Maquinaria/Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdComentador,IdDefecto,Opinion,Fecha")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDefecto = new SelectList(db.Defectos, "Id", "Descripcion", comentario.IdDefecto);
            return View(comentario);
        }

        // GET: Maquinaria/Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Maquinaria/Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
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
