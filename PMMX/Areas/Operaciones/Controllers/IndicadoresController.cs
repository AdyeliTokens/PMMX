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

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class IndicadoresController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/Indicador
        public ActionResult Index()
        {
            return View(db.Indicadores.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicadores = db.Indicadores.Find(id);
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        // GET: Maquinaria/Indicador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maquinaria/Indicador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Activo")] Indicador indicadores)
        {
            if (ModelState.IsValid)
            {
                db.Indicadores.Add(indicadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indicadores);
        }

        // GET: Maquinaria/Indicador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicadores = db.Indicadores.Find(id);
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        // POST: Maquinaria/Indicador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Activo")] Indicador indicadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indicadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(indicadores);
        }

        // GET: Maquinaria/Indicador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicadores = db.Indicadores.Find(id);
            if (indicadores == null)
            {
                return HttpNotFound();
            }
            return View(indicadores);
        }

        // POST: Maquinaria/Indicador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indicador indicadores = db.Indicadores.Find(id);
            db.Indicadores.Remove(indicadores);
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
