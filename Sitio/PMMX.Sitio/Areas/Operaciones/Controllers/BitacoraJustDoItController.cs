using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Operaciones;

namespace Sitio.Areas.Operaciones
{
    public class BitacoraJustDoItController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/BitacoraJustDoIt
        public ActionResult Index()
        {
            var bitacoraJustDoIts = db.BitacoraJustDoIts.Include(b => b.Estatus).Include(b => b.JustDoIt).Include(b => b.Rechazo).Include(b => b.Responsable);
            return View(bitacoraJustDoIts.ToList());
        }

        // GET: Operaciones/BitacoraJustDoIt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraJustDoIt bitacoraJustDoIt = db.BitacoraJustDoIts.Find(id);
            if (bitacoraJustDoIt == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraJustDoIt);
        }

        // GET: Operaciones/BitacoraJustDoIt/Create
        public ActionResult Create()
        {
            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre");
            ViewBag.IdJustDoIt = new SelectList(db.JustDoIt, "Id", "Descripcion");
            ViewBag.IdRechazo = new SelectList(db.Rechazo, "Id", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/BitacoraJustDoIt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdJustDoIt,IdStatus,IdResponsable,IdRechazo,Fecha,Comentario")] BitacoraJustDoIt bitacoraJustDoIt)
        {
            if (ModelState.IsValid)
            {
                db.BitacoraJustDoIts.Add(bitacoraJustDoIt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre", bitacoraJustDoIt.IdStatus);
            ViewBag.IdJustDoIt = new SelectList(db.JustDoIt, "Id", "Descripcion", bitacoraJustDoIt.IdJustDoIt);
            ViewBag.IdRechazo = new SelectList(db.Rechazo, "Id", "Nombre", bitacoraJustDoIt.IdRechazo);
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", bitacoraJustDoIt.IdResponsable);
            return View(bitacoraJustDoIt);
        }

        // GET: Operaciones/BitacoraJustDoIt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraJustDoIt bitacoraJustDoIt = db.BitacoraJustDoIts.Find(id);
            if (bitacoraJustDoIt == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre", bitacoraJustDoIt.IdStatus);
            ViewBag.IdJustDoIt = new SelectList(db.JustDoIt, "Id", "Descripcion", bitacoraJustDoIt.IdJustDoIt);
            ViewBag.IdRechazo = new SelectList(db.Rechazo, "Id", "Nombre", bitacoraJustDoIt.IdRechazo);
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", bitacoraJustDoIt.IdResponsable);
            return View(bitacoraJustDoIt);
        }

        // POST: Operaciones/BitacoraJustDoIt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdJustDoIt,IdStatus,IdResponsable,IdRechazo,Fecha,Comentario")] BitacoraJustDoIt bitacoraJustDoIt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bitacoraJustDoIt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre", bitacoraJustDoIt.IdStatus);
            ViewBag.IdJustDoIt = new SelectList(db.JustDoIt, "Id", "Descripcion", bitacoraJustDoIt.IdJustDoIt);
            ViewBag.IdRechazo = new SelectList(db.Rechazo, "Id", "Nombre", bitacoraJustDoIt.IdRechazo);
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", bitacoraJustDoIt.IdResponsable);
            return View(bitacoraJustDoIt);
        }

        // GET: Operaciones/BitacoraJustDoIt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraJustDoIt bitacoraJustDoIt = db.BitacoraJustDoIts.Find(id);
            if (bitacoraJustDoIt == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraJustDoIt);
        }

        // POST: Operaciones/BitacoraJustDoIt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BitacoraJustDoIt bitacoraJustDoIt = db.BitacoraJustDoIts.Find(id);
            db.BitacoraJustDoIts.Remove(bitacoraJustDoIt);
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
