using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Warehouse;

namespace Sitio.Areas.Warehouse.Controllers
{
    public class VentanaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/Ventana
        public ActionResult Index()
        {
            var ventana = db.Ventana
                .Include(e => e.Evento)
                .Include(e => e.Proveedor)
                .Include(e => e.Carrier)
                .Include(e => e.Destino)
                .Include(e => e.Procedencia)
                .Include(e => e.SubCategoria)
                .ToList();

            return View(ventana);
        }
        
        // GET: Warehouse/Ventana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Ventana ventana = db.Ventana.Find(id);
            var ventana = db.Ventana.Where(v=> (v.Id == id))
                 .Include(e => e.Evento)
                 .Include(e => e.Proveedor)
                 .Include(e => e.Carrier)
                 .Include(e => e.Destino)
                 .Include(e => e.Procedencia)
                 .Include(e => e.SubCategoria)
                 .FirstOrDefault();
            if (ventana == null)
            {
                return HttpNotFound();
            }
            return View(ventana);
        }
        
        public ActionResult GetStatusActualVentana(int idVentana)
        {
            if (ModelState.IsValid)
            {
                var status = db.StatusVentana.Where(s => (s.IdVentana == idVentana)).OrderByDescending(s => s.Fecha).Select(s => s.Status.Nombre).FirstOrDefault();
                return Json(new { status }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetRechazoActualVentana(int idVentana)
        {
            if (ModelState.IsValid)
            {
                var rechazo = db.BitacoraVentana.Where(s => (s.IdVentana == idVentana)).OrderByDescending(s => s.Fecha).Select(s => s.Rechazo.Nombre).FirstOrDefault();
                return Json(new { rechazo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVentanabyEvento(int idEvento)
        {
            if (ModelState.IsValid)
            {
                var ventana = db.Ventana.Where(o => (o.IdEvento == idEvento)).Select(o => new { Id = o.Id, PO = o.PO }).FirstOrDefault();
                return Json(new { ventana }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Warehouse/Ventana/Create
        public ActionResult Create()
        {
            ViewBag.IdProveedor = new SelectList(db.Proveedores.Select(x => new { Id = x.Id, NombreCorto = x.NombreCorto }).OrderBy(x => x.NombreCorto), "Id", "NombreCorto");
            ViewBag.IdProcedencia = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto +" "+ x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdDestino = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdCarrier = new SelectList(db.Carrier.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdSubCategoria = new SelectList(db.SubCategoria.Where(x => (x.IdCategoria == 10)).Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");

            return View();
        }

        // POST: Warehouse/Ventana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ventana ventana)
        {
            ViewBag.IdProveedor = new SelectList(db.Proveedores.Select(x => new { Id = x.Id, NombreCorto = x.NombreCorto }).OrderBy(x => x.NombreCorto), "Id", "NombreCorto");
            ViewBag.IdProcedencia = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdDestino = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdCarrier = new SelectList(db.Carrier.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdSubCategoria = new SelectList(db.SubCategoria.Where(x => (x.IdCategoria == 10)).Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");

            if (ModelState.IsValid)
            {
                db.Ventana.Add(ventana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ventana);
        }

        // GET: Warehouse/Ventana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Ventana ventana = db.Ventana.Find(id);
           
            if (ventana == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdProveedor = new SelectList(db.Proveedores.Select(x => new { Id = x.Id, NombreCorto = x.NombreCorto }).OrderBy(x => x.NombreCorto), "Id", "NombreCorto", ventana.IdProveedor);
            ViewBag.IdProcedencia = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdProcedencia);
            ViewBag.IdDestino = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdDestino);
            ViewBag.IdCarrier = new SelectList(db.Carrier.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdCarrier);
            ViewBag.IdSubCategoria = new SelectList(db.SubCategoria.Where(x => (x.IdCategoria == 10)).Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdSubCategoria);

            return View(ventana);
        }

        // POST: Warehouse/Ventana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ventana ventana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProveedor = new SelectList(db.Proveedores.Select(x => new { Id = x.Id, NombreCorto = x.NombreCorto }).OrderBy(x => x.NombreCorto), "Id", "NombreCorto", ventana.IdProveedor);
            ViewBag.IdProcedencia = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdProcedencia);
            ViewBag.IdDestino = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdDestino);
            ViewBag.IdCarrier = new SelectList(db.Carrier.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdCarrier);
            ViewBag.IdSubCategoria = new SelectList(db.SubCategoria.Where(x => (x.IdCategoria == 10)).Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdSubCategoria);

            return View(ventana);
        }

        // GET: Warehouse/Ventana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ventana = db.Ventana.Where(v => (v.Id == id))
                 .Include(e => e.Evento)
                 .Include(e => e.Proveedor)
                 .Include(e => e.Carrier)
                 .Include(e => e.Destino)
                 .Include(e => e.Procedencia)
                 .Include(e => e.SubCategoria)
                 .FirstOrDefault();

            if (ventana == null)
            {
                return HttpNotFound();
            }
            return View(ventana);
        }

        // POST: Warehouse/Ventana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ventana ventana = db.Ventana.Find(id);
            db.Ventana.Remove(ventana);
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
