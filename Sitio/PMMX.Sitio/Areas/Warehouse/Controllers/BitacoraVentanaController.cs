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
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Entidades;
using Microsoft.AspNet.Identity;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.Vistas;
using PMMX.Modelo.Entidades.Operaciones;
using Sitio.Helpers;

namespace Sitio.Areas.Warehouse.Controllers
{
    [Authorize]
    public class BitacoraVentanaController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Warehouse/BitacoraVentana
        public ActionResult Index()
        {
            return View(db.BitacoraVentana.Include(b => b.Rechazo).Include(b => b.Ventana).Include(b => b.Estatus).Include(b => b.Responsable).ToList());
        }

        // GET: Warehouse/BitacoraVentana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraVentana);
        }

        // GET: Warehouse/BitacoraVentana/Create
        public ActionResult Create()
        {
            ViewBag.IdRechazo = new SelectList(db.Rechazo.Select(x => new { Id = x.Id, Nombre = x.Nombre}).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Warehouse/BitacoraVentana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BitacoraVentana bitacoraVentana)
        {
            ViewBag.IdRechazo = new SelectList(db.Rechazo.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", bitacoraVentana.IdRechazo);

            if (ModelState.IsValid)
            {
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());

                if (persona.EjecucionCorrecta)
                {
                    bitacoraVentana.IdResponsable = persona.Respuesta.Id;
                }
                
                Ventana ventana = db.Ventana
                    .Include(v => v.StatusVentana)
                    .Where( v => (v.Id == bitacoraVentana.IdVentana))
                    .FirstOrDefault();

                Rechazo rechazo = db.Rechazo.Find(bitacoraVentana.IdRechazo);
                bitacoraVentana.IdStatus = rechazo.IdStatus;
                bitacoraVentana.Fecha = DateTime.Now;

                db.BitacoraVentana.Add(bitacoraVentana);
                db.SaveChanges();

                saveStatusVentana(bitacoraVentana);

                return RedirectToAction("Index", "Evento", new { Area = "Operaciones" });
            }
            else
                return View(bitacoraVentana);
        }

        public bool saveStatusVentana(BitacoraVentana bitacoraVentana)
        {
            StatusVentana statusVentana = new StatusVentana();
            statusVentana.IdResponsable = bitacoraVentana.IdResponsable;
            statusVentana.IdStatus = bitacoraVentana.IdStatus;
            statusVentana.IdVentana = bitacoraVentana.IdVentana;
            statusVentana.Fecha = bitacoraVentana.Fecha;
            statusVentana.Comentarios = bitacoraVentana.Comentarios;
            db.StatusVentana.Add(statusVentana);
            db.SaveChanges();

            Ventana ventana = db.Ventana
                                 .Include(v => v.StatusVentana)
                                 .Include(v => v.StatusVentana.Select(s => s.Status))
                                 .Include(v => v.BitacoraVentana)
                                 .Include(v => v.BitacoraVentana.Select(b => b.Estatus))
                                 .Include(v => v.BitacoraVentana.Select(b => b.Rechazo ))
                                 .Include(v => v.Evento)
                                 .Include(v => v.Proveedor)
                                 .SingleOrDefault(x => x.Id == statusVentana.IdVentana);

            try
            {
                UsuarioServicio usuarioServicio = new UsuarioServicio();
                NotificationService notify = new NotificationService();

                string senders = usuarioServicio.GetEmailByStatus(ventana);
                EmailService emailService = new EmailService();
                emailService.SendMail(senders, ventana);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return true;
        }
        
        // GET: Warehouse/BitacoraVentana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraVentana);
        }

        // POST: Warehouse/BitacoraVentana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BitacoraVentana bitacoraVentana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bitacoraVentana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bitacoraVentana);
        }

        // GET: Warehouse/BitacoraVentana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            if (bitacoraVentana == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraVentana);
        }

        // POST: Warehouse/BitacoraVentana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BitacoraVentana bitacoraVentana = db.BitacoraVentana.Find(id);
            db.BitacoraVentana.Remove(bitacoraVentana);
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
