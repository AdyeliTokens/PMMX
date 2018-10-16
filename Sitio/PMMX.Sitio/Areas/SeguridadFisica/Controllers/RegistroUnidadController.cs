using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.SeguridadFisica;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Seguridad.Servicios;

namespace Sitio.Areas.SeguridadFisica.Controllers
{
    public class RegistroUnidadController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: SeguridadFisica/RegistroUnidad
        public ActionResult Index()
        {
            var registroUnidad = db.RegistroUnidad
                .Include(v => v.Formato)
                .Include(r => r.Formato)
                .Include(r => r.Datos)
                .Include(r => r.Bitacora)
                .Include(r => r.Bitacora.Select(b => b.Guardia));

            return View(registroUnidad.ToList());
        }

        // GET: SeguridadFisica/RegistroUnidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RegistroUnidad registroUnidad = db.RegistroUnidad
                .Where(v=> v.Id == id)
                .Include(v=> v.Formato)
                .Include(v => v.Datos)
                .Include(v => v.Bitacora)
                .Include(r => r.Bitacora.Select(b => b.Guardia))
                .FirstOrDefault();

            if (registroUnidad == null)
            {
                return HttpNotFound();
            }
            return View(registroUnidad);
        }

        // GET: SeguridadFisica/RegistroUnidad/Create
        public ActionResult Create()
        {
            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo");
            return View();
        }

        // POST: SeguridadFisica/RegistroUnidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegistroUnidad registroUnidad, DatosUnidad datos, string Puerta)
        {
            if (ModelState.IsValid)
            {
                db.RegistroUnidad.Add(registroUnidad);
                db.SaveChanges();
                datos.IdRegistroUnidad = registroUnidad.Id;
                db.DatosUnidad.Add(datos);
                
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());

                if (persona.EjecucionCorrecta)
                {
                    BitacoraUnidad bitacora = new BitacoraUnidad();
                    bitacora.IdGuardia = persona.Respuesta.Id;
                    bitacora.Puerta = Puerta;
                    bitacora.Fecha = DateTime.Now;
                    bitacora.IdRegistroUnidad = registroUnidad.Id;
                    db.BitacoraUnidad.Add(bitacora);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo", registroUnidad.IdFormato);
            return View(registroUnidad);
        }

        // GET: SeguridadFisica/RegistroUnidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RegistroUnidad registroUnidad = db.RegistroUnidad
                .Where(v => v.Id == id)
                .Include(v => v.Formato)
                .Include(v => v.Datos)
                .Include(v => v.Bitacora)
                .Include(r => r.Bitacora.Select(b => b.Guardia))
                .FirstOrDefault();

            if (registroUnidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo", registroUnidad.IdFormato);
            return View(registroUnidad);
        }

        // POST: SeguridadFisica/RegistroUnidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegistroUnidad registroUnidad, DatosUnidad datos, string Puerta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroUnidad).State = EntityState.Modified;
                db.SaveChanges();
                datos.IdRegistroUnidad = registroUnidad.Id;

                var _datos = db.DatosUnidad.Where(d => d.IdRegistroUnidad == registroUnidad.Id).FirstOrDefault();
                //CopyValues(datos, _datos);
                //db.Entry(_datos).Property(x => x.Id).IsModified = false;
                //db.Entry(_datos).CurrentValues.SetValues(datos);                
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo", registroUnidad.IdFormato);
            return View(registroUnidad);
        }

        public void CopyValues<T>(T source, T destination)
        {
            var props = typeof(T).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute))).ToArray();

            foreach (var prop in props)
            {
                var value = prop.GetValue(source);
                prop.SetValue(destination, value);
            }

            // string[] properties = new string[] { "NombreConductor", "Placas", "NoEco", "NoCaja", "TipoRemolque" };
            
            db.SaveChanges();
        }

        // GET: SeguridadFisica/RegistroUnidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RegistroUnidad registroUnidad = db.RegistroUnidad
                .Where(v => v.Id == id)
                .Include(v => v.Formato)
                .Include(v => v.Datos)
                .Include(v => v.Bitacora)
                .Include(r => r.Bitacora.Select(b => b.Guardia))
                .FirstOrDefault();

            if (registroUnidad == null)
            {
                return HttpNotFound();
            }
            return View(registroUnidad);
        }

        // POST: SeguridadFisica/RegistroUnidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistroUnidad registroUnidad = db.RegistroUnidad.Find(id);
            db.RegistroUnidad.Remove(registroUnidad);
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
