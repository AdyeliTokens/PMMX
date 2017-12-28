using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo;

namespace Sitio.Controllers
{
    public class RequisicionDeDescargasController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: RequisicionDeDescargas
        public ActionResult Index()
        {
            return View(db.RequisicionDeDescargas.ToList());
        }

        // GET: RequisicionDeDescargas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisicionDeDescarga requisicionDeDescarga = db.RequisicionDeDescargas.Find(id);
            if (requisicionDeDescarga == null)
            {
                return HttpNotFound();
            }
            return View(requisicionDeDescarga);
        }

        // GET: RequisicionDeDescargas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequisicionDeDescargas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Comentario,Email,Solicitud,Aprobacion")] RequisicionDeDescarga requisicionDeDescarga)
        {
            if (ModelState.IsValid)
            {
                db.RequisicionDeDescargas.Add(requisicionDeDescarga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requisicionDeDescarga);
        }

        // GET: RequisicionDeDescargas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisicionDeDescarga requisicionDeDescarga = db.RequisicionDeDescargas.Find(id);
            if (requisicionDeDescarga == null)
            {
                return HttpNotFound();
            }
            return View(requisicionDeDescarga);
        }

        // POST: RequisicionDeDescargas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Comentario,Email,Solicitud,Aprobacion")] RequisicionDeDescarga requisicionDeDescarga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisicionDeDescarga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requisicionDeDescarga);
        }

        // GET: RequisicionDeDescargas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisicionDeDescarga requisicionDeDescarga = db.RequisicionDeDescargas.Find(id);
            if (requisicionDeDescarga == null)
            {
                return HttpNotFound();
            }
            return View(requisicionDeDescarga);
        }

        // POST: RequisicionDeDescargas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisicionDeDescarga requisicionDeDescarga = db.RequisicionDeDescargas.Find(id);
            db.RequisicionDeDescargas.Remove(requisicionDeDescarga);
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
