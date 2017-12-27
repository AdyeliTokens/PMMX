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
    public class BussinesUnitsController : Controller
    {

        private PMMXContext db = new PMMXContext();

        // GET: Maquinaria/BussinesUnits
        public ActionResult Index()
        {
            var bussinesUnits = db.BussinesUnits.Include(b => b.Responsable);
            return View(bussinesUnits.ToList());
        }

        // GET: Maquinaria/BussinesUnits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinesUnit bussinesUnit = db.BussinesUnits.Find(id);
            if (bussinesUnit == null)
            {
                return HttpNotFound();
            }
            return View(bussinesUnit);
        }

        // GET: Maquinaria/BussinesUnits/Create
        public ActionResult Create()
        {
            //ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Maquinaria/BussinesUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdResponsable,Nombre,NombreCorto,Activo")] BussinesUnit bussinesUnit)
        {
            if (ModelState.IsValid)
            {
                db.BussinesUnits.Add(bussinesUnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(bussinesUnit);
        }

        // GET: Maquinaria/BussinesUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinesUnit bussinesUnit = db.BussinesUnits.Find(id);
            if (bussinesUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(bussinesUnit);
        }

        // POST: Maquinaria/BussinesUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdResponsable,Nombre,NombreCorto,Activo")] BussinesUnit bussinesUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bussinesUnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(bussinesUnit);
        }

        // GET: Maquinaria/BussinesUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BussinesUnit bussinesUnit = db.BussinesUnits.Find(id);
            if (bussinesUnit == null)
            {
                return HttpNotFound();
            }
            return View(bussinesUnit);
        }

        // POST: Maquinaria/BussinesUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BussinesUnit bussinesUnit = db.BussinesUnits.Find(id);
            db.BussinesUnits.Remove(bussinesUnit);
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
