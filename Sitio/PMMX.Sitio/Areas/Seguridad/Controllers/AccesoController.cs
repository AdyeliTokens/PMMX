using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Seguridad;
using PMMX.Modelo.Vistas;
using Sitio.Helpers;

namespace Sitio.Areas.Seguridad.Controllers
{
    public class AccesoController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Acceso
        public ActionResult Index()
        {
            return View(db.Accesos.ToList());
        }

        // GET: Seguridad/Acceso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acceso acceso = db.Accesos.Find(id);
            if (acceso == null)
            {
                return HttpNotFound();
            }
            return View(acceso);
        }

        // GET: Seguridad/Acceso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seguridad/Acceso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Area,Menu,SubMenu,Programa,Ruta,Activo")] Acceso acceso)
        {
            if (ModelState.IsValid)
            {
                db.Accesos.Add(acceso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acceso);
        }

        // GET: Seguridad/Acceso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acceso acceso = db.Accesos.Find(id);
            if (acceso == null)
            {
                return HttpNotFound();
            }
            return View(acceso);
        }

        // POST: Seguridad/Acceso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Area,Menu,SubMenu,Programa,Ruta,Activo")] Acceso acceso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acceso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acceso);
        }

        [RenderAjaxPartialScripts]
        public ActionResult AccesoByPersona(int IdPersona)
        {
            List<AccesoView> list = new List<AccesoView>();

            list = db.Personas
                .Where(p => p.Id == IdPersona)
                .Select(p => p.Accesos.Where(a => a.Activo==true).Select(v => new AccesoView
                {
                    Area = v.Area,
                    Menu = v.Menu,
                    SubMenu = v.SubMenu,
                    Programa = v.Programa,
                    Ruta = v.Ruta
                }).ToList()
                ).FirstOrDefault();

            return Json(new { list }, JsonRequestBehavior.AllowGet);
        }

        // GET: Seguridad/Acceso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acceso acceso = db.Accesos.Find(id);
            if (acceso == null)
            {
                return HttpNotFound();
            }
            return View(acceso);
        }

        // POST: Seguridad/Acceso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Acceso acceso = db.Accesos.Find(id);
            db.Accesos.Remove(acceso);
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
