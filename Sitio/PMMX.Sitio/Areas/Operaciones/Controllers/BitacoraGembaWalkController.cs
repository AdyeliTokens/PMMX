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

        // GET: Operaciones/BitacoraGembaWalk
        public ActionResult Index()
        {
            var BitacoraGembaWalks = db.BitacoraGembaWalks.Include(b => b.Estatus).Include(b => b.GembaWalk).Include(b => b.Responsable);
            return View(BitacoraGembaWalks.ToList());
        }

        // GET: Operaciones/BitacoraGembaWalk/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraGembaWalk BitacoraGembaWalk = db.BitacoraGembaWalks.Find(id);
            if (BitacoraGembaWalk == null)
            {
                return HttpNotFound();
            }
            return View(BitacoraGembaWalk);
        }

        // GET: Operaciones/BitacoraGembaWalk/Create
        public ActionResult Create()
        {
            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre");
            ViewBag.IdGembaWalk = new SelectList(db.GembaWalk, "Id", "Descripcion");
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Operaciones/BitacoraGembaWalk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdGembaWalk,IdStatus,IdResponsable,IdRechazo,Fecha,Comentario")] BitacoraGembaWalk BitacoraGembaWalk)
        {
            if (ModelState.IsValid)
            {
                db.BitacoraGembaWalks.Add(BitacoraGembaWalk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre", BitacoraGembaWalk.IdStatus);
            ViewBag.IdGembaWalk = new SelectList(db.GembaWalk, "Id", "Descripcion", BitacoraGembaWalk.IdGembaWalk);
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", BitacoraGembaWalk.IdResponsable);
            return View(BitacoraGembaWalk);
        }

        // GET: Operaciones/BitacoraGembaWalk/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraGembaWalk BitacoraGembaWalk = db.BitacoraGembaWalks.Find(id);
            if (BitacoraGembaWalk == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre", BitacoraGembaWalk.IdStatus);
            ViewBag.IdGembaWalk = new SelectList(db.GembaWalk, "Id", "Descripcion", BitacoraGembaWalk.IdGembaWalk);
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", BitacoraGembaWalk.IdResponsable);
            return View(BitacoraGembaWalk);
        }

        // POST: Operaciones/BitacoraGembaWalk/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdGembaWalk,IdStatus,IdResponsable,IdRechazo,Fecha,Comentario")] BitacoraGembaWalk BitacoraGembaWalk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(BitacoraGembaWalk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdStatus = new SelectList(db.Estatus, "Id", "Nombre", BitacoraGembaWalk.IdStatus);
            ViewBag.IdGembaWalk = new SelectList(db.GembaWalk, "Id", "Descripcion", BitacoraGembaWalk.IdGembaWalk);
            ViewBag.IdResponsable = new SelectList(db.Personas, "Id", "Nombre", BitacoraGembaWalk.IdResponsable);
            return View(BitacoraGembaWalk);
        }

        // GET: Operaciones/BitacoraGembaWalk/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraGembaWalk BitacoraGembaWalk = db.BitacoraGembaWalks.Find(id);
            if (BitacoraGembaWalk == null)
            {
                return HttpNotFound();
            }
            return View(BitacoraGembaWalk);
        }

        // POST: Operaciones/BitacoraGembaWalk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BitacoraGembaWalk BitacoraGembaWalk = db.BitacoraGembaWalks.Find(id);
            db.BitacoraGembaWalks.Remove(BitacoraGembaWalk);
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
