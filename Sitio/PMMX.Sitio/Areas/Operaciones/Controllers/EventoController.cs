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
            var evento = db.Evento.Include(e => e.Asignador).Include(e => e.Categoria);
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
                .Where(e => e.Id == id)
                .Select(e => new EventoView
                {
                    Id = e.Id,
                    Descripcion = e.Descripcion,
                    IdAsignador = e.IdAsignador,
                    IdCategoria = e.IdCategoria,
                    FechaInicio = e.FechaInicio,
                    FechaFin = e.FechaFin,
                    Nota = e.Nota,
                    EsRecurrente = e.EsRecurrente,
                    Activo = e.Activo,
                    JustDoIt = e.JustDoIt.Where(j => j.IdEvento == e.Id)
                    .Select(j => new JustDoItView
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
            ViewBag.IdCategoria = db.Categoria.Where(x => x.Id == evento.IdCategoria).Select(x => x.Nombre ).FirstOrDefault().ToString();

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
        public ActionResult GetResponsables()
        {
            if (ModelState.IsValid)
            {
                var responsables = db.Personas.Select(w => new { Id = w.Id, Nombre = w.Nombre +" "+ w.Apellido1 +" "+ w.Apellido2 }).OrderBy(w => w.Id).ToList();
                return Json(new { responsables }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListaDistribucion()
        {
            if (ModelState.IsValid)
            {
                var lista = db.SubArea.Select(w => new { Id = w.Id, Nombre = w.Nombre }).OrderBy(w => w.Id).ToList();
                return Json(new { lista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public PartialViewResult Responsables()
        {
           return PartialView("_Responsables");
        }

        public PartialViewResult Ubicaciones()
        {
            var areas = db.Area.Select(w => new { Id = w.Id, NombreCorto = w.NombreCorto }).OrderBy(w => w.Id).ToList();
            return PartialView("_Origen", new { areas = areas }); 
        }

        public PartialViewResult ListaDistribucion()
        {
            return PartialView("_ListaDistribucion");
        }

        // GET: Eventos/Evento/Create
        public ActionResult Create()
        {
            ViewBag.IdAsignador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre}).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Eventos/Evento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evento evento, int IdOrigen, string IdResponsables)
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
                    db.Evento.Add(evento);
                    db.SaveChanges();
                }
                
                if(IdOrigen > 0)
                {
                    EventoOrigen eOrigen = new EventoOrigen();
                    eOrigen.IdEvento = evento.Id;
                    eOrigen.IdOrigen = IdOrigen;
                    db.EventoOrigen.Add(eOrigen);
                    db.SaveChanges();
                }
                
                if(IdResponsables != null && IdResponsables != "")
                {
                    Char delimiter = ',';
                    String[] substrings = IdResponsables.Split(delimiter);
                    foreach (var substring in substrings)
                    {
                        EventoResponsable eResponsable = new EventoResponsable();
                        eResponsable.IdEvento = evento.Id;
                        eResponsable.IdResponsable = int.Parse(substring);
                        db.EventoResponsable.Add(eResponsable);
                        db.SaveChanges();
                    }

                    NotificationService notify = new NotificationService();
                    UsuarioServicio usuarioServicio = new UsuarioServicio();

                    List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(evento.Id);
                    List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                    foreach (string notificacion in llaves)
                    {
                        notify.SendPushNotification(notificacion, "Se le ha asignado un nuevo evento: " + evento.Descripcion + ". ", "");
                    }

                    List<UserView> users = usuarioServicio.GetEmailByEvento(evento.Id);
                    List<string> emails = users.Select(x => x.Email).ToList();
                    
                    EmailService emailService = new EmailService();
                    emailService.SendMail(emails);
                }
                
                switch(evento.IdCategoria)
                {
                    case 10:
                        return RedirectToAction("Create", "Ventana", new {IdEvento = evento.Id, Area="Warehouse"});
                        break;
                    default:
                        return RedirectToAction("Index");
                        break;
                }
                
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
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", evento.IdCategoria);
            return View(evento);
        }

        // POST: Eventos/Evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.IdAsignador = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido2 }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(db.Categoria.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", evento.IdCategoria);
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
            ViewBag.IdCategoria = db.Categoria.Where(x => x.Id == evento.IdCategoria).Select(x => x.Nombre).FirstOrDefault().ToString();
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
