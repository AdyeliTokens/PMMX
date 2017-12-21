using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Paros;
using MySql.Data.MySqlClient;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class ParosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/Paros
        public ActionResult Index()
        {
            var paro = db.Paros.Include(p => p.Origen)
                .Include(p => p.Reportador)
                .Include(p => p.ActividadesEnParo)
                .Include(p => p.Mecanico)
                .Include(p => p.Origen)
                .Include(p => p.Origen.WorkCenter)
                .Include(p => p.Origen.Modulo)
                .Include(p => p.Origen.WorkCenter.BussinesUnit);
            return View(paro.ToList());
        }

        // GET: Maquinaria/Paros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paro paro = db.Paros.Find(id);
            if (paro == null)
            {
                return HttpNotFound();
            }
            return View(paro);
        }

        // GET: Maquinaria/Paros/Create
        public ActionResult Create()
        {
            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id");
            //ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdReportador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Maquinaria/Paros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdOrigen,IdReportador,Descripcion,FechaReporte,Activo")] Paro paro)
        {
            if (ModelState.IsValid)
            {
                db.Paros.Add(paro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", paro.IdOrigen);
            ViewBag.IdReportador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(paro);
        }

        // GET: Maquinaria/Paros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paro paro = db.Paros.Find(id);
            if (paro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", paro.IdOrigen);
            ViewBag.IdReportador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(paro);
        }

        // POST: Maquinaria/Paros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdOrigen,IdReportador,Descripcion,FechaReporte,Activo")] Paro paro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", paro.IdOrigen);
            ViewBag.IdReportador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(paro);
        }

        // GET: Maquinaria/Paros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paro paro = db.Paros.Find(id);
            if (paro == null)
            {
                return HttpNotFound();
            }
            return View(paro);
        }

        // POST: Maquinaria/Paros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paro paro = db.Paros.Find(id);
            db.Paros.Remove(paro);
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
