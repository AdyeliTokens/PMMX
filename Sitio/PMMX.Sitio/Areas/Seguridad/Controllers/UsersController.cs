using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;

namespace Sitio.Areas.Seguridad.Controllers
{
    public class UsersController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Persona);
            return View(users.ToList());
        }

        // GET: Seguridad/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Seguridad/Users/Create
        public ActionResult Create()
        {
            ViewBag.IdEntorno = new SelectList(db.Entornos, "Id", "Nombre");
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Seguridad/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPersona,IdEntorno,UserName,Email,Password,Reset_password,Key_password,Activo")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.IdEntorno = new SelectList(db.Entornos, "Id", "Nombre", user.IdEntorno);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", user.IdPersona);
            return View(user);
        }

        // GET: Seguridad/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IdEntorno = new SelectList(db.Entornos, "Id", "Nombre", user.IdEntorno);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", user.IdPersona);
            return View(user);
        }

        // POST: Seguridad/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPersona,IdEntorno,UserName,Email,Password,Reset_password,Key_password,Activo")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IdEntorno = new SelectList(db.Entornos, "Id", "Nombre", user.IdEntorno);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", user.IdPersona);
            return View(user);
        }

        // GET: Seguridad/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Seguridad/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
