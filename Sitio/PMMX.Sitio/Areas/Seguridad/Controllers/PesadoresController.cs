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
    public class PesadoresController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Pesadores
        public ActionResult Index()
        {
            var pesadores = db.Pesadores.Include(p => p.Area).Include(p => p.Operador_Pesador);
            return View(pesadores.ToList());
        }

        // GET: Seguridad/Pesadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pesador pesador = db.Pesadores.Find(id);
            if (pesador == null)
            {
                return HttpNotFound();
            }
            return View(pesador);
        }

        // GET: Seguridad/Pesadores/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.Area, "Id", "Nombre");
            ViewBag.IdPesador = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Seguridad/Pesadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPesador,IdArea,Activo")] Pesador pesador)
        {
            if (ModelState.IsValid)
            {
                db.Pesadores.Add(pesador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArea = new SelectList(db.Area, "Id", "Nombre", pesador.IdArea);
            ViewBag.IdPesador = new SelectList(db.Personas, "Id", "Nombre", pesador.IdPesador);
            return View(pesador);
        }

        // GET: Seguridad/Pesadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pesador pesador = db.Pesadores.Find(id);
            if (pesador == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.Area, "Id", "Nombre", pesador.IdArea);
            ViewBag.IdPesador = new SelectList(db.Personas, "Id", "Nombre", pesador.IdPesador);
            return View(pesador);
        }

        // POST: Seguridad/Pesadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPesador,IdArea,Activo")] Pesador pesador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pesador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.Area, "Id", "Nombre", pesador.IdArea);
            ViewBag.IdPesador = new SelectList(db.Personas, "Id", "Nombre", pesador.IdPesador);
            return View(pesador);
        }

        // GET: Seguridad/Pesadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pesador pesador = db.Pesadores.Find(id);
            if (pesador == null)
            {
                return HttpNotFound();
            }
            return View(pesador);
        }

        // POST: Seguridad/Pesadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pesador pesador = db.Pesadores.Find(id);
            db.Pesadores.Remove(pesador);
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
