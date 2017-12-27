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
    public class PersonasController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Personas
        public ActionResult Index()
        {
            var personas = db.Personas.Include(p => p.Puesto);
            return View(personas.ToList());
        }

        // GET: Seguridad/Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Seguridad/Personas/Create
        public ActionResult Create()
        {
            ViewBag.IdPuesto = new SelectList(db.Puestos, "Id", "Nombre");
            return View();
        }

        // POST: Seguridad/Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPuesto,IdWorkCenter,IdBussinesUnit,Nombre,Apellido1,Apellido2,Activo")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.IdPuesto = new SelectList(db.Puestos, "Id", "Nombre", persona.IdPuesto);
            return View(persona);
        }

        // GET: Seguridad/Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPuesto = new SelectList(db.Puestos, "Id", "Nombre", persona.IdPuesto);
            return View(persona);
        }

        // POST: Seguridad/Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPuesto,IdWorkCenter,IdBussinesUnit,Nombre,Apellido1,Apellido2,Activo")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPuesto = new SelectList(db.Puestos, "Id", "Nombre", persona.IdPuesto);
            return View(persona);
        }

        // GET: Seguridad/Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Seguridad/Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Personas.Find(id);
            db.Personas.Remove(persona);
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
