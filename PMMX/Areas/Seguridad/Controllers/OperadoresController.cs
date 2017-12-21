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

namespace Sitio.Areas.Seguridad.Controllers
{
    [Authorize]
    public class OperadoresController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Operadores
        public ActionResult Index()
        {
            var operadores = db.Operadores.Include(o => o.Operador).Include(o => o.WorkCenter);
            return View(operadores.ToList());
        }

        // GET: Seguridad/Operadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operadores operadoresPorWorkCenter = db.Operadores.Find(id);
            if (operadoresPorWorkCenter == null)
            {
                return HttpNotFound();
            }
            return View(operadoresPorWorkCenter);
        }

        // GET: Seguridad/Operadores/Create
        public ActionResult Create()
        {
            //ViewBag.IdOperador = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdOperador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        // POST: Seguridad/Operadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdOperador,IdWorkCenter,Activo")] Operadores operadoresPorWorkCenter)
        {
            if (ModelState.IsValid)
            {
                db.Operadores.Add(operadoresPorWorkCenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOperador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", operadoresPorWorkCenter.IdWorkCenter);
            return View(operadoresPorWorkCenter);
        }

        // GET: Seguridad/Operadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operadores operadoresPorWorkCenter = db.Operadores.Find(id);
            if (operadoresPorWorkCenter == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOperador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", operadoresPorWorkCenter.IdWorkCenter);
            return View(operadoresPorWorkCenter);
        }

        // POST: Seguridad/Operadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdOperador,IdWorkCenter,Activo")] Operadores operadoresPorWorkCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operadoresPorWorkCenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOperador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", operadoresPorWorkCenter.IdWorkCenter);
            return View(operadoresPorWorkCenter);
        }

        // GET: Seguridad/Operadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operadores operadoresPorWorkCenter = db.Operadores.Find(id);
            if (operadoresPorWorkCenter == null)
            {
                return HttpNotFound();
            }
            return View(operadoresPorWorkCenter);
        }

        // POST: Seguridad/Operadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operadores operadoresPorWorkCenter = db.Operadores.Find(id);
            db.Operadores.Remove(operadoresPorWorkCenter);
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
