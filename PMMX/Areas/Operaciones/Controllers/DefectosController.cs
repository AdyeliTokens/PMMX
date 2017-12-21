using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System.Web.Routing;
using PMMX.Modelo.Entidades;
using PMMX.Seguridad.Servicios;
using Microsoft.AspNet.Identity;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class DefectosController : Controller
    {
        

        // GET: Maquinaria/Defectos
        public ActionResult Index()
        {
            DefectoServicio servicio = new DefectoServicio();
            var defecto = servicio.GetDefectos();

            return View(defecto.Respuesta.ToList());
        }

        // GET: Maquinaria/Defectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idDefecto = (int) id;
            DefectoServicio servicio = new DefectoServicio();
            RespuestaServicio<DefectoView> defecto = servicio.GetDefecto(idDefecto);
            if (defecto == null)
            {
                return HttpNotFound();
            }
            return View(defecto.Respuesta);
        }

        // GET: Maquinaria/Defectos/Create
        public ActionResult Create()
        {
            //ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id");
            //ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Maquinaria/Defectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdReportador,IdOrigen,Descripcion,Activo,FechaReporte,FechaEstimada")] Defecto defecto)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Defectos.Add(defecto);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", defecto.IdOrigen);
            //ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre", defecto.IdReportador);
            return View(/*defecto*/);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComentario(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
                if (persona.EjecucionCorrecta)
                {
                    comentario.IdComentador = persona.Respuesta.Id;
                    ComentarioServicio servicio = new ComentarioServicio();
                    RespuestaServicio<ComentarioView> respuesta = servicio.PostComentario(comentario);
                    
                }
                else {

                }
                
            }

            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "Defectos", action = "Details", Id = comentario.IdDefecto }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNotificacionSAP(int id, string NotificacionSAP)
        {

            if (ModelState.IsValid)
            {
                DefectoServicio servicio = new DefectoServicio();
                RespuestaServicio<DefectoView> _defecto = servicio.PutDefecto(id, NotificacionSAP);

                if (_defecto == null)
                {
                    return HttpNotFound();
                }
            }
            
            return RedirectToAction("Details/", new RouteValueDictionary(new { controller = "Defectos", action = "Details", Id = id }));
        }

        // GET: Maquinaria/Defectos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int idDefecto = (int)id;
            DefectoServicio servicio = new DefectoServicio();
            RespuestaServicio<DefectoView> defecto = servicio.GetDefecto(idDefecto);
            if (defecto == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", defecto.IdOrigen);
            //ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre", defecto.IdReportador);
            return View(defecto.Respuesta);
        }

        // POST: Maquinaria/Defectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdReportador,IdOrigen,Descripcion,Activo,FechaReporte,FechaEstimada")] Defecto defecto)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(defecto).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.IdOrigen = new SelectList(db.Origens, "Id", "Id", defecto.IdOrigen);
            //ViewBag.IdReportador = new SelectList(db.Personas, "Id", "Nombre", defecto.IdReportador);
            return View(/*defecto*/);
        }

        // GET: Maquinaria/Defectos/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Defecto defecto = db.Defectos.Find(id);
            //if (defecto == null)
            //{
            //    return HttpNotFound();
            //}
            return View(/*defecto*/);
        }

        // POST: Maquinaria/Defectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Defecto defecto = db.Defectos.Find(id);
            //db.Defectos.Remove(defecto);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DefectoServicio servicio = new DefectoServicio();
                servicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
