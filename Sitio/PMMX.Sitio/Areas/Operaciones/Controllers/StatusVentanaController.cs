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
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Entidades;
using Microsoft.AspNet.Identity;
using PMMX.Modelo.Vistas;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.Entidades.Warehouse;
using Sitio.Helpers;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class StatusVentanaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/StatusVentana
        public ActionResult Index()
        {
            var statusVentana = db.StatusVentana.Include(s => s.Responsable).Include(s => s.Ventana).Include(s => s.Status).ToList();
            return View(statusVentana);
        }

        // GET: Operaciones/StatusVentana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var statusVentana = db.StatusVentana.Where(s => (s.Id == id)).
                Select(s => new StatusVentanaView
                {
                    Id = s.Id,
                    IdVentana = s.IdVentana,
                    IdStatus = s.IdStatus,
                    IdResponsable = s.IdResponsable,
                    Ventana = new VentanaView
                    {
                        Id = s.Ventana.Id,
                        PO = s.Ventana.PO
                    },
                    Status = new EstatusView
                    {
                        Id = s.Status.Id,
                        Nombre = s.Status.Nombre
                    },
                    Responsable = new PersonaView
                    {
                        Id = s.Responsable.Id,
                        Nombre = s.Responsable.Nombre,
                        Apellido1 = s.Responsable.Apellido1,
                        Apellido2 = s.Responsable.Apellido2
                    }
                }).FirstOrDefault();

            if (statusVentana == null)
            {
                return HttpNotFound();
            }

            return View(statusVentana);
        }
        
        public PartialViewResult getTimeline(int IdVentana)
        {
            var statusVentana = db.StatusVentana
                .Where(s => (s.IdVentana == IdVentana))
                .OrderByDescending(s => s.Fecha)
                .Select(s => new StatusVentanaView
                {
                    Id = s.Id,
                    IdVentana = s.IdVentana,
                    IdStatus = s.IdStatus,
                    IdResponsable = s.IdResponsable,
                    Fecha = s.Fecha,
                    Ventana = new VentanaView
                    {
                        Id = s.Ventana.Id,
                        PO = s.Ventana.PO,
                        Destino = new LocacionView
                        {
                            Id = s.Ventana.Destino.Id,
                            Nombre = s.Ventana.Destino.Nombre,
                            NombreCorto = s.Ventana.Destino.NombreCorto
                        },
                        Procedencia = new LocacionView
                        {
                            Id = s.Ventana.Procedencia.Id,
                            Nombre = s.Ventana.Procedencia.Nombre,
                            NombreCorto = s.Ventana.Procedencia.NombreCorto
                        },
                    },
                    Status = new EstatusView
                    {
                        Id = s.Status.Id,
                        Nombre = s.Status.Nombre,
                        BitacoraVentana = s.Status.BitacoraVentana
                        .Where(b => (b.IdVentana == s.IdVentana) && (b.IdStatus == s.IdStatus))
                        .Select(b => new BitacoraVentanaView
                        {
                            Id = b.Id,
                            Rechazo = new RechazoView
                            {
                                Id = b.Rechazo.Id,
                                Nombre = b.Rechazo.Nombre
                            }
                        }).ToList()

                    },
                    Responsable = new PersonaView
                    {
                        Id = s.Responsable.Id,
                        Nombre = s.Responsable.Nombre,
                        Apellido1 = s.Responsable.Apellido1,
                        Apellido2 = s.Responsable.Apellido2
                    }
                }).ToList();

            return PartialView("Timeline", statusVentana);
        }

        public int getEstatusActual(int IdVentana)
        {
            var idEstatus = db.StatusVentana
                .Where(s => s.IdVentana == IdVentana)
                .OrderByDescending(s => s.Fecha)
                .Select(s => s.IdStatus)
                .FirstOrDefault();

            return idEstatus;
        }
        
        // GET: Operaciones/StatusVentana/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Operaciones/StatusVentana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StatusVentana statusVentana)
        {
            if (ModelState.IsValid)
            {
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());

                if (persona.EjecucionCorrecta)
                {
                    statusVentana.IdResponsable = persona.Respuesta.Id;
                }

                var IdSubCategoria = db.Ventana.Where(x => x.Id == statusVentana.IdVentana).Select(x => x.IdSubCategoria).FirstOrDefault();

                WorkFlowServicio workflowServicio = new WorkFlowServicio();
                IRespuestaServicio<WorkFlowView> workFlow = workflowServicio.nextEstatus(IdSubCategoria, statusVentana.IdStatus, false);

                statusVentana.IdStatus = workFlow.Respuesta.EstatusSiguiente.Id;
                statusVentana.Fecha = DateTime.Now;
                db.StatusVentana.Add(statusVentana);
                db.SaveChanges();

                var ventana = db.Ventana
                            .Include(v => v.StatusVentana)
                            .Include(v => v.StatusVentana.Select(s => s.Status))
                            .Include(v => v.BitacoraVentana)
                            .Include(v => v.BitacoraVentana.Select(b => b.Estatus))
                            .Include(v => v.BitacoraVentana.Select(b => b.Rechazo))
                            .Include(v => v.Evento)
                            .Where(x => x.Id == statusVentana.IdVentana)
                            .FirstOrDefault();

                try
                {
                    UsuarioServicio usuarioServicio = new UsuarioServicio();
                    NotificationService notify = new NotificationService();

                    string senders = usuarioServicio.GetEmailByEvento(ventana.IdEvento);

                    if (senders != "")
                    {
                        EmailService emailService = new EmailService();
                        emailService.SendMail(senders, ventana);
                    }

                    List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(statusVentana.Ventana.IdEvento);
                    List<string> llaves = dispositivos.Select(x => x.Llave).ToList();
                    var estatus = ventana.StatusVentana.OrderByDescending(s => s.Fecha).Select(s => s.Status).FirstOrDefault();

                    if (llaves.Count > 0)
                    {
                        foreach (string notificacion in llaves)
                        {
                            notify.SendPushNotification(notificacion, " Cambio de estatus Ventana: " + ventana.Evento.Descripcion + ". ", " Cambio de estatus a " + estatus.Nombre);
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                return RedirectToAction("Index");
            }
            
            return View(statusVentana);
        }
        
        // GET: Operaciones/StatusVentana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            
            if (statusVentana == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdStatus = new SelectList(db.Estatus.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.IdVentana = new SelectList(db.Ventana.Select(x => new { Id = x.Id, PO = x.PO }).OrderBy(x => x.PO), "Id", "PO");
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre + " " + x.Apellido1 + " " + x.Apellido1 }).OrderBy(x => x.Nombre), "Id", "Nombre");

            return View(statusVentana);
        }

        // POST: Operaciones/StatusVentana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StatusVentana statusVentana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusVentana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdStatus = new SelectList(db.Estatus.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", statusVentana.IdStatus);
            ViewBag.IdVentana = new SelectList(db.Ventana.Select(x => new { Id = x.Id, PO = x.PO }).OrderBy(x => x.PO), "Id", "PO", statusVentana.IdVentana);
            ViewBag.IdResponsable = new SelectList(db.Personas.Select(x => new { Id = x.Id, Nombre = x.Nombre +" "+ x.Apellido1 +" "+ x.Apellido1 }).OrderBy(x => x.Nombre), "Id", "Nombre", statusVentana.IdResponsable);
            return View(statusVentana);
        }

        // GET: Operaciones/StatusVentana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var statusVentana = db.StatusVentana.Where(s => (s.Id == id)).
                Select(s => new StatusVentanaView
                {
                    Id = s.Id,
                    IdVentana = s.IdVentana,
                    IdStatus = s.IdStatus,
                    IdResponsable = s.IdResponsable,
                    Ventana = new VentanaView
                    {
                        Id = s.Ventana.Id,
                        PO = s.Ventana.PO
                    },
                    Status = new EstatusView
                    {
                        Id = s.Status.Id,
                        Nombre = s.Status.Nombre
                    },
                    Responsable = new PersonaView
                    {
                        Id = s.Responsable.Id,
                        Nombre = s.Responsable.Nombre,
                        Apellido1 = s.Responsable.Apellido1,
                        Apellido2 = s.Responsable.Apellido2
                    }
                }).FirstOrDefault();

            if (statusVentana == null)
            {
                return HttpNotFound();
            }
            return View(statusVentana);
        }

        // POST: Operaciones/StatusVentana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusVentana statusVentana = db.StatusVentana.Find(id);
            db.StatusVentana.Remove(statusVentana);
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
