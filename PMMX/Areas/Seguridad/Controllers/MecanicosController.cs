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
    public class MecanicosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Mecanicos
        public ActionResult Index()
        {
            var mecanicos = db.Mecanicos.Include(m => m.BusinessUnit).Include(m => m.Mecanico);
            return View(mecanicos.ToList());
        }

        // GET: Seguridad/Mecanicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecanicos mecanicoPorBusinessUnit = db.Mecanicos.Find(id);
            if (mecanicoPorBusinessUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", mecanicoPorBusinessUnit.IdBusinessUnit);
            ViewBag.IdMecanico = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(mecanicoPorBusinessUnit);
        }

        // GET: Seguridad/Mecanicos/Create
        public ActionResult Create()
        {
            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre");
            ViewBag.IdMecanico = new SelectList(db.Personas.Select(x => new { Id = x.Id , Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2  }).OrderBy(x=> x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Seguridad/Mecanicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdMecanico,IdBusinessUnit,Activo")] Mecanicos mecanicoPorBusinessUnit)
        {
            if (ModelState.IsValid)
            {
                db.Mecanicos.Add(mecanicoPorBusinessUnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", mecanicoPorBusinessUnit.IdBusinessUnit);
            ViewBag.IdMecanico = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(mecanicoPorBusinessUnit);
        }

        // GET: Seguridad/Mecanicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecanicos mecanicoPorBusinessUnit = db.Mecanicos.Find(id);
            if (mecanicoPorBusinessUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", mecanicoPorBusinessUnit.IdBusinessUnit);
            ViewBag.IdMecanico = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(mecanicoPorBusinessUnit);
        }

        // POST: Seguridad/Mecanicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdMecanico,IdBusinessUnit,Activo")] Mecanicos mecanicoPorBusinessUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mecanicoPorBusinessUnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBusinessUnit = new SelectList(db.BussinesUnits, "Id", "Nombre", mecanicoPorBusinessUnit.IdBusinessUnit);
            //ViewBag.IdMecanico = new SelectList(db.Personas, "Id", "Nombre", mecanicoPorBusinessUnit.IdMecanico);
            ViewBag.IdMecanico = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");

            return View(mecanicoPorBusinessUnit);
        }

        // GET: Seguridad/Mecanicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecanicos mecanicoPorBusinessUnit = db.Mecanicos.Find(id);
            if (mecanicoPorBusinessUnit == null)
            {
                return HttpNotFound();
            }
            return View(mecanicoPorBusinessUnit);
        }

        // POST: Seguridad/Mecanicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mecanicos mecanicoPorBusinessUnit = db.Mecanicos.Find(id);
            db.Mecanicos.Remove(mecanicoPorBusinessUnit);
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
