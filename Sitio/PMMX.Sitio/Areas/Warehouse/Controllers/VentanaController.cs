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
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Entidades;
using Microsoft.AspNet.Identity;
using PMMX.Operaciones.Servicios;
using PMMX.Modelo.Vistas;
using OfficeOpenXml;
using Sitio.Helpers;

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
                .Include(e => e.TipoOperacion)
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
            
            var ventana = db.Ventana.Where(v=> (v.Id == id))
                 .Include(e => e.Evento)
                 .Include(e => e.Proveedor)
                 .Include(e => e.Carrier)
                 .Include(e => e.Destino)
                 .Include(e => e.Procedencia)
                 .Include(e => e.SubCategoria)
                 .Include(e => e.TipoOperacion)
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
                var ventana = db.Ventana.Where(o => (o.IdEvento == idEvento))
                    .Select(o => new
                    {
                        Id = o.Id,
                        PO = o.PO,
                        Cancelado = o.StatusVentana.OrderByDescending(s => s.Fecha).Select(s => s.Status.WorkFlowInicial.Select( w => w.Inicial)).FirstOrDefault()
                    }).FirstOrDefault();
                
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
            ViewBag.IdOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");

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
            ViewBag.IdOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");

            if (ventana.PO == null) ventana.PO = " ";
            if (ventana.Recurso == null) ventana.Recurso = " ";
            if (ventana.TipoUnidad == null) ventana.TipoUnidad = " ";
            if (ventana.NombreCarrier == null) ventana.NombreCarrier = " ";
            if (ventana.Dimension == null) ventana.Dimension = " ";
            if (ventana.NumeroEconomico == null) ventana.NumeroEconomico = " ";
            if (ventana.NumeroPlaca == null) ventana.NumeroPlaca = " ";
            if (ventana.EconomicoRemolque == null) ventana.EconomicoRemolque = " ";
            if (ventana.ColorContenedor == null) ventana.ColorContenedor = " ";
            if (ventana.PlacaRemolque == null) ventana.PlacaRemolque = " ";
            if (ventana.Sellos == null) ventana.Sellos = " ";
            if (ventana.ModeloContenedor == null) ventana.ModeloContenedor = " ";
            if (ventana.Conductor == null) ventana.Conductor = " ";
            if (ventana.MovilConductor == null) ventana.MovilConductor = " ";
            
            if (ModelState.IsValid)
            {
                db.Ventana.Add(ventana);
                db.SaveChanges();

                for (int i = 0; i < 3; i++)
                {
                    changeEstatus(ventana);
                }

                return RedirectToAction("Index");
            }

            return View(ventana);
        }

        public Boolean changeEstatus(Ventana ventana)
        {
            if (ModelState.IsValid)
            {
                var estatus = new Estatus();

               estatus = db.StatusVentana
                    .Where(s => (s.IdVentana == ventana.Id))
                    .OrderByDescending(s => s.Fecha)
                    .Select(s => s.Status)
                    .FirstOrDefault();

                if (estatus == null)
                {
                    estatus = db.Estatus
                        .Where(e => e.IdCategoria == (db.SubCategoria.Where(s => s.Id == ventana.IdSubCategoria).Select(s => s.IdCategoria).FirstOrDefault()))
                        .FirstOrDefault();
                }
               
                
                WorkFlowServicio workflowServicio = new WorkFlowServicio();
                IRespuestaServicio<WorkFlowView> workFlow = workflowServicio.nextEstatus(ventana.IdSubCategoria, estatus.Id, false);

                if (workFlow.EjecucionCorrecta)
                {
                    PersonaServicio personaServicio = new PersonaServicio();
                    IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());

                    StatusVentana statusVentana = new StatusVentana();
                    statusVentana.IdVentana = ventana.Id;
                    statusVentana.IdResponsable = persona.Respuesta.Id;

                    if(estatus.Id == 0)
                    {
                        statusVentana.IdStatus = workFlow.Respuesta.EstatusInicial.Id;
                    }
                    else
                    {
                        statusVentana.IdStatus = workFlow.Respuesta.EstatusSiguiente.Id;
                    }
                    
                    statusVentana.Fecha = DateTime.Now;
                    statusVentana.Comentarios = " ";
                    db.StatusVentana.Add(statusVentana);
                    db.SaveChanges();

                    try
                    {
                        UsuarioServicio usuarioServicio = new UsuarioServicio();
                        NotificationService notify = new NotificationService();

                        string senders = usuarioServicio.GetEmailByEvento(statusVentana.Ventana.IdEvento);
                        if (senders != null)
                        {
                            EmailService emailService = new EmailService();

                            Ventana ventanaSend = db.Ventana
                                     .Include(v => v.TipoOperacion)
                                     .Include(v => v.StatusVentana)
                                     .Include(v => v.StatusVentana.Select(s => s.Status))
                                     .Include(v => v.BitacoraVentana)
                                     .Include(v => v.BitacoraVentana.Select(b => b.Estatus))
                                     .Include(v => v.BitacoraVentana.Select(b => b.Rechazo))
                                     .Include(v => v.Evento)
                                     .SingleOrDefault(x => x.Id == statusVentana.IdVentana);

                            emailService.SendMail(senders, ventanaSend);
                        }

                        List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(statusVentana.Ventana.IdEvento);
                        List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                        foreach (string notificacion in llaves)
                        {
                            notify.SendPushNotification(notificacion, " Cambio de estatus Ventana: " + ventana.Evento.Descripcion + ". ", " Cambio de estatus a " + estatus.Nombre);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                return true;
            }
            return false;
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
            ViewBag.IdOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");

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
            ViewBag.IdOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");

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
                 .Include(e => e.TipoOperacion)
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

        public ActionResult Upload()
        {
            ViewBag.SelectSubCategoria = new SelectList(db.SubCategoria.Where(x => (x.IdCategoria == 10)).Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            ViewBag.SelectOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, Ventana ventana)
        {
            if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                
                using (var package = new ExcelPackage(file.InputStream))
                {
                    PersonaServicio personaServicio = new PersonaServicio();
                    IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
                    
                    var currentSheet = package.Workbook.Worksheets;

                    foreach (var workSheet in currentSheet)
                    {
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        if (workSheet.Cells[2, 2].Value != null)
                        {
                            ventana.PO = workSheet.Cells[2, 2].Value.ToString().Trim();
                            var numProveedor = Convert.ToInt32(workSheet.Cells[3, 2].Value.ToString().Trim());
                            ventana.IdProveedor = db.Proveedores.Where(p => (p.NumeroProveedor == numProveedor)).Select(p => p.Id).FirstOrDefault();

                            ventana.Recurso = workSheet.Cells[4, 2].Value.ToString().Trim();
                            ventana.Cantidad = Convert.ToDouble(workSheet.Cells[5, 2].Value.ToString().Trim());
                            ventana.IdCarrier = 3;
                            ventana.NombreCarrier = workSheet.Cells[6, 2].Value.ToString().Trim();
                            ventana.Conductor = workSheet.Cells[7, 2].Value.ToString().Trim();
                            ventana.MovilConductor = workSheet.Cells[8, 2].Value.ToString().Trim();

                            var nombreCorto = (workSheet.Cells[9, 2].Value.ToString().Trim());
                            ventana.IdProcedencia = db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault() ;
                            nombreCorto = (workSheet.Cells[10, 2].Value.ToString().Trim());
                            ventana.IdDestino = db.Locacion.Where(l => l.NombreCorto == nombreCorto ).Select(l => l.Id).FirstOrDefault();

                            ventana.NumeroEconomico = workSheet.Cells[11, 2].Value.ToString().Trim();
                            ventana.NumeroPlaca = workSheet.Cells[12, 2].Value.ToString().Trim();
                            ventana.EconomicoRemolque = workSheet.Cells[13, 2].Value.ToString().Trim();
                            ventana.PlacaRemolque = workSheet.Cells[14, 2].Value.ToString().Trim();
                            ventana.ModeloContenedor = workSheet.Cells[15, 2].Value.ToString().Trim();
                            ventana.ColorContenedor = workSheet.Cells[16, 2].Value.ToString().Trim();
                            ventana.Sellos = workSheet.Cells[17, 2].Value.ToString().Trim();
                            ventana.TipoUnidad = workSheet.Cells[18, 2].Value.ToString().Trim();
                            ventana.Dimension = workSheet.Cells[19, 2].Value.ToString().Trim();
                            ventana.Temperatura = workSheet.Cells[20, 2].Value.ToString().Trim();

                            db.Ventana.Add(ventana);
                            db.SaveChanges();

                            for(int i=0; i<3; i++)
                            {
                                changeEstatus(ventana);
                            }
                        }
                    }
                    return RedirectToAction("Index", "Evento", new {  Area = "Operaciones" });
                }
            }
            return View("Index");
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
