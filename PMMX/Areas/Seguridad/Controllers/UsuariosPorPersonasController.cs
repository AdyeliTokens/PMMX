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
    public class UsuariosPorPersonasController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/UsuariosPorPersonas
        public ActionResult Index()
        {
            var usuariosPorPersonas = db.UsuariosPorPersonas.Include(u => u.Persona).Include(u => u.Usuario);
            return View(usuariosPorPersonas.ToList());
        }

        // GET: Seguridad/UsuariosPorPersonas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosPorPersona usuariosPorPersona = db.UsuariosPorPersonas.Find(id);
            if (usuariosPorPersona == null)
            {
                return HttpNotFound();
            }
            return View(usuariosPorPersona);
        }

        // GET: Seguridad/UsuariosPorPersonas/Create
        public ActionResult Create()
        {
            
            ViewBag.IdPersona = new SelectList(db.Personas.Select(x => new {Id = x.Id , Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdAspNetUser = new SelectList(db.AspNetUser, "Id", "Email");
            return View();
        }

        // POST: Seguridad/UsuariosPorPersonas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdAspNetUser,IdPersona")] UsuariosPorPersona usuariosPorPersona)
        {
            if (ModelState.IsValid)
            {
                db.UsuariosPorPersonas.Add(usuariosPorPersona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPersona = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }), "Id", "Nombre");
            ViewBag.IdAspNetUser = new SelectList(db.AspNetUser, "Id", "Email", usuariosPorPersona.IdAspNetUser);
            return View(usuariosPorPersona);
        }

        // GET: Seguridad/UsuariosPorPersonas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosPorPersona usuariosPorPersona = db.UsuariosPorPersonas.Find(id);
            if (usuariosPorPersona == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", usuariosPorPersona.IdPersona);
            ViewBag.IdAspNetUser = new SelectList(db.AspNetUser, "Id", "Email", usuariosPorPersona.IdAspNetUser);
            return View(usuariosPorPersona);
        }

        // POST: Seguridad/UsuariosPorPersonas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAspNetUser,IdPersona")] UsuariosPorPersona usuariosPorPersona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuariosPorPersona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", usuariosPorPersona.IdPersona);
            ViewBag.IdAspNetUser = new SelectList(db.AspNetUser, "Id", "Email", usuariosPorPersona.IdAspNetUser);
            return View(usuariosPorPersona);
        }

        // GET: Seguridad/UsuariosPorPersonas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuariosPorPersona usuariosPorPersona = db.UsuariosPorPersonas.Find(id);
            if (usuariosPorPersona == null)
            {
                return HttpNotFound();
            }
            return View(usuariosPorPersona);
        }

        // POST: Seguridad/UsuariosPorPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuariosPorPersona usuariosPorPersona = db.UsuariosPorPersonas.Find(id);
            db.UsuariosPorPersonas.Remove(usuariosPorPersona);
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
