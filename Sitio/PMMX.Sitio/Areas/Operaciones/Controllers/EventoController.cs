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
using PMMX.Modelo.Vistas;
using Sitio.Helpers;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Entidades;
using Microsoft.AspNet.Identity;
using PMMX.Modelo.Entidades.JustDoIts;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class EventoController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Eventos/Evento
        public ActionResult Index()
        {
            var evento = db.EventoResponsable.Include(e => e.Evento).Include(e => e.Evento.Asignador).Include(e => e.Responsable).ToList();
            return View(evento);
        }

        
        public ActionResult GetEvents()
        {
            if (ModelState.IsValid)
            {
                var events = db.Evento.ToList();
                return Json(new {events}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Eventos/Evento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            var evento = db.Evento
                .Where(e => e.Id == id).
                Select(e => new Evento
                {
                    Id = e.Id,
                    Descripcion = e.Descripcion,
                    IdAsignador = e.IdAsignador,
                    FechaInicio = e.FechaInicio,
                    FechaFin = e.FechaFin,
                    Nota = e.Nota,
                    EsRecurrente = e.EsRecurrente,
                    Activo = e.Activo,
                    EventoOrigen = e.EventoOrigen.Where(o => o.IdEvento == e.Id)
                    .Select(o => new EventoOrigen
                    {
                        Id = o.Id,
                        IdEvento = o.IdEvento,
                        IdOrigen = o.IdOrigen
                    }).ToList(),
                    EventoResponsable = e.EventoResponsable.Where(r => r.IdEvento == e.Id)
                    .Select(r => new EventoResponsable
                    {
                        Id = r.Id,
                        IdEvento = r.IdEvento,
                        IdResponsable = r.IdResponsable
                    }).ToList(),
                    JustDoIt = e.JustDoIt.Where(j => j.IdEvento == e.Id)
                    .Select(j => new JustDoIt
                    {
                        Id = j.Id,
                        IdCategoria = j.IdCategoria
                    }).ToList(),
                }).FirstOrDefault();
            
            if (evento == null)
            {
                return HttpNotFound();
            }

            ViewBag.NombreAsignador = db.Personas.Where(x => x.Id == evento.IdAsignador).Select(x =>  x.Nombre + " " + x.Apellido1 + " " + x.Apellido2).FirstOrDefault().ToString();
            //ViewBag.NombreResponsable = db.Personas.Where(x => x.Id == evento.EventoResponsable.IdResponsable).Select(x => x.Nombre + " " + x.Apellido1 + " " + x.Apellido2).FirstOrDefault().ToString();
            //ViewBag.NombreCategoria = db.Categoria.Where(x => x.Id == evento.JustDoIt.IdCategoria).Select(x => x.Nombre ).FirstOrDefault().ToString();
            //ViewBag.Ubicacion = db.Origens.Where(x => x.Id == evento.IdOrigen).Select(x => x.WorkCenter.BussinesUnit.Area.Nombre + " " + x.WorkCenter.Nombre + " " + x.Modulo.Nombre).FirstOrDefault().ToString();

            return View(evento);
        }

        public ActionResult GetArea()
        {
            if (ModelState.IsValid)
            {
                var areas = db.Area.Select(w => new { Id = w.Id, NombreCorto = w.NombreCorto }).OrderBy(w => w.Id).ToList();
                return Json(new { areas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetWorkCenterbyArea(int idArea)
        {
            if (ModelState.IsValid)
            {
                var wlist = db.WorkCenters.Where(w => w.BussinesUnit.IdArea == idArea).Select( w=> new { Id = w.Id, NombreCorto = w.NombreCorto}).OrderBy( w => w.Id ).ToList();
                return Json(new { wlist }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetModulobyWorkCenter(int idWorkCenter)
        {
            if (ModelState.IsValid)
            {
                var modulo = db.Origens.Where(o => (o.IdWorkCenter == idWorkCenter) && (o.IdModulo != null)).Select(o => new { Id = o.Modulo.Id, NombreCorto = o.Modulo.NombreCorto }).ToList();
                return Json(new { modulo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetIdOrigen(int idWorkCenter, int idModulo)
        {
            if (ModelState.IsValid)
            {
                var origen = db.Origens.Where(o => (o.IdWorkCenter == idWorkCenter) && (o.IdModulo == idModulo)).Select(o => new { Id = o.Id }).FirstOrDefault();
                return Json(new { origen }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Eventos/Evento/Create
        public ActionResult Create()
        {
             ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, NombreCorto = x.NombreCorto }).OrderBy(x => x.NombreCorto), "Id", "NombreCorto");
             ViewBag.IdAsignador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
             ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            
            return View();
        }

        // POST: Eventos/Evento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evento evento)
       // public ActionResult Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                if (evento.Nota == null)
                    evento.Nota = " ";

                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());

                if (persona.EjecucionCorrecta)
                {
                   evento.IdAsignador = persona.Respuesta.Id;
                }
                
                db.Evento.Add(evento);
                db.SaveChanges();

                NotificationService notify = new NotificationService();
                UsuarioServicio usuarioServicio = new UsuarioServicio();

                List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(evento.Id);
                List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                foreach (string notificacion in llaves)
                {
                    notify.SendPushNotification(notificacion, "Se le ha asignado un nuevo evento: " + evento.Descripcion + ". ", "");
                }

                return RedirectToAction("Index");
            }
            
            return View(evento);
        }

        // GET: Eventos/Evento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdAsignador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre", evento.IdAsignador);
            //ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre", evento.IdResponsable);
            //ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre}).OrderBy(x => x.Nombre), "Id", "Nombre", evento.IdCategoria);
            //ViewBag.IdOrigen = new SelectList(db.Origens.Select(x => new { Id = x.Id, Nombre = x.WorkCenter.BussinesUnit.Area.Nombre + " " + x.WorkCenter.Nombre + " " + x.Modulo.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", evento.IdOrigen);
            return View(evento);
        }

        // POST: Eventos/Evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,IdOrigen,IdCategoria,IdAsignador,IdResponsable,FechaInicio,FechaFin,Nota,EsRecurrente,Activo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.IdAsignador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View(evento);
        }

        // GET: Eventos/Evento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }

            ViewBag.NombreAsignador = db.Personas.Where(x => x.Id == evento.IdAsignador).Select(x => x.Nombre + " " + x.Apellido1 + " " + x.Apellido2).FirstOrDefault().ToString();
            //ViewBag.NombreResponsable = db.Personas.Where(x => x.Id == evento.IdResponsable).Select(x => x.Nombre + " " + x.Apellido1 + " " + x.Apellido2).FirstOrDefault().ToString();
            //ViewBag.NombreCategoria = db.Categoria.Where(x => x.Id == evento.IdCategoria).Select(x => x.Nombre).FirstOrDefault().ToString();
            //ViewBag.Ubicacion = db.Origens.Where(x => x.Id == evento.IdOrigen).Select(x => x.WorkCenter.BussinesUnit.Area.Nombre + " " + x.WorkCenter.Nombre + " " + x.Modulo.Nombre).FirstOrDefault().ToString();

            return View(evento);
        }

        // POST: Eventos/Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Evento.Find(id);
            db.Evento.Remove(evento);
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
