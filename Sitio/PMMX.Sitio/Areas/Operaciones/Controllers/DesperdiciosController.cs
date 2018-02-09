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
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using Microsoft.AspNet.Identity;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class DesperdiciosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        
        public ActionResult Index()
        {
            var desperdicios = db.Desperdicios.Include(d => d.MarcaDelCigarrillo).Include(d => d.Reportante).Include(d => d.Seccion).Include(d => d.WorkCenter);
            return View(desperdicios.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            return View(desperdicio);
        }

        
        public ActionResult Create()
        {
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion");
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Desperdicio desperdicio)
        {
            PersonaServicio personaServicio = new PersonaServicio();
            IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
            desperdicio.IdPersona = persona.Respuesta.Id;
            desperdicio.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {

                db.Desperdicios.Add(desperdicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", desperdicio.Code_FA);
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", desperdicio.IdWorkCenter);
            return View(desperdicio);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", desperdicio.Code_FA);
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", desperdicio.IdWorkCenter);
            return View(desperdicio);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Desperdicio desperdicio)
        {
            PersonaServicio personaServicio = new PersonaServicio();
            IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
            desperdicio.IdPersona = persona.Respuesta.Id;
            desperdicio.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(desperdicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", desperdicio.Code_FA);
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", desperdicio.IdWorkCenter);
            return View(desperdicio);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            return View(desperdicio);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            db.Desperdicios.Remove(desperdicio);
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
