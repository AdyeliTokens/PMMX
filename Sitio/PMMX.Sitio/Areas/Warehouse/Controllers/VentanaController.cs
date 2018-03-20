﻿using System;
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
using System.Drawing;
using System.IO;

namespace Sitio.Areas.Warehouse.Controllers
{
    [Authorize]
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

                Ventana ventanaSend = db.Ventana
                            .Include(v => v.TipoOperacion)
                            .Include(v => v.StatusVentana)
                            .Include(v => v.StatusVentana.Select(s => s.Status))
                            .Include(v => v.BitacoraVentana)
                            .Include(v => v.BitacoraVentana.Select(b => b.Estatus))
                            .Include(v => v.BitacoraVentana.Select(b => b.Rechazo))
                            .Include(v => v.Evento)
                            .Include(v => v.Proveedor)
                            .SingleOrDefault(x => x.Id == ventana.Id);

                sendNotifications(ventanaSend);

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
                }

                return true;
            }
            return false;
        }

        public bool sendNotifications(Ventana ventana)
        {
            try
            {
                UsuarioServicio usuarioServicio = new UsuarioServicio();
                NotificationService notify = new NotificationService();

                string senders = usuarioServicio.GetEmailByEvento(ventana.IdEvento);
                if (senders != null)
                {
                    EmailService emailService = new EmailService();
                    emailService.SendMail(senders, ventana);
                }

                List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(ventana.IdEvento);
                List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                var estatus = db.StatusVentana
                         .Where(s => (s.IdVentana == ventana.Id))
                         .OrderByDescending(s => s.Fecha)
                         .Select(s => s.Status)
                         .FirstOrDefault();

                foreach (string notificacion in llaves)
                {
                    notify.SendPushNotification(notificacion, " Cambio de estatus Ventana: " + ventana.Evento.Descripcion + ". ", " Cambio de estatus a " + estatus.Nombre);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
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

                ViewBag.SelectSubCategoria = new SelectList(db.SubCategoria.Where(x => (x.IdCategoria == 10)).Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdSubCategoria);
                ViewBag.SelectOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdOperacion);

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
                            ventana.PO = workSheet.Cells[2, 2].Value == null ? string.Empty : workSheet.Cells[2, 2].Value.ToString().Trim();
                            var numProveedor = Convert.ToInt32(workSheet.Cells[3, 2].Value.ToString().Trim());
                            ventana.IdProveedor = db.Proveedores.Where(p => (p.NumeroProveedor == numProveedor)).Select(p => p.Id).FirstOrDefault();

                            ventana.Recurso = workSheet.Cells[4, 2].Value == null ? string.Empty : workSheet.Cells[4, 2].Value.ToString().Trim();
                            ventana.Cantidad = Convert.ToDouble(workSheet.Cells[5, 2].Value == null ? 0 : workSheet.Cells[5, 2].Value);
                            ventana.IdCarrier = 3;
                            ventana.NombreCarrier = workSheet.Cells[6, 2].Value == null ? string.Empty : workSheet.Cells[6, 2].Value.ToString().Trim();
                            ventana.Conductor = workSheet.Cells[7, 2].Value == null ? string.Empty : workSheet.Cells[7, 2].Value.ToString().Trim();
                            ventana.MovilConductor = workSheet.Cells[8, 2].Value == null ? string.Empty : workSheet.Cells[8, 2].Value.ToString().Trim();

                            var nombreCorto = workSheet.Cells[9, 2].Value == null ? "MX" : workSheet.Cells[9, 2].Value.ToString().Trim();
                            ventana.IdProcedencia = db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault() == 0 ? db.Locacion.Where(l => l.NombreCorto == "MX").Select(l => l.Id).FirstOrDefault() : db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault();
                            nombreCorto = workSheet.Cells[10, 2].Value == null ? "MX" : workSheet.Cells[10, 2].Value.ToString().Trim();
                            ventana.IdDestino = db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault() == 0 ? db.Locacion.Where(l => l.NombreCorto == "MX").Select(l => l.Id).FirstOrDefault() : db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault();

                            ventana.NumeroEconomico = workSheet.Cells[11, 2].Value == null ? string.Empty : workSheet.Cells[11, 2].Value.ToString().Trim();
                            ventana.NumeroPlaca = workSheet.Cells[12, 2].Value == null ? string.Empty : workSheet.Cells[12, 2].Value.ToString().Trim();
                            ventana.EconomicoRemolque = workSheet.Cells[13, 2].Value == null ? string.Empty : workSheet.Cells[13, 2].Value.ToString().Trim();
                            ventana.PlacaRemolque = workSheet.Cells[14, 2].Value == null ? string.Empty : workSheet.Cells[14, 2].Value.ToString().Trim();
                            ventana.ModeloContenedor = workSheet.Cells[15, 2].Value == null ? string.Empty : workSheet.Cells[15, 2].Value.ToString().Trim();
                            ventana.ColorContenedor = workSheet.Cells[16, 2].Value == null ? string.Empty : workSheet.Cells[16, 2].Value.ToString().Trim();
                            ventana.Sellos = workSheet.Cells[17, 2].Value == null ? string.Empty : workSheet.Cells[17, 2].Value.ToString().Trim();
                            ventana.TipoUnidad = workSheet.Cells[18, 2].Value == null ? string.Empty : workSheet.Cells[18, 2].Value.ToString().Trim();
                            ventana.Dimension = workSheet.Cells[19, 2].Value == null ? string.Empty : workSheet.Cells[19, 2].Value.ToString().Trim();
                            ventana.Temperatura = workSheet.Cells[20, 2].Value == null ? string.Empty : workSheet.Cells[20, 2].Value.ToString().Trim();

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

        public ActionResult downloadDataVentana(int IdVentana)
        {
            Ventana ventana = db.Ventana
                            .Include(v => v.TipoOperacion)
                            .Include(v => v.StatusVentana)
                            .Include(v => v.StatusVentana.Select(s => s.Status))
                            .Include(v => v.BitacoraVentana)
                            .Include(v => v.BitacoraVentana.Select(b => b.Estatus))
                            .Include(v => v.BitacoraVentana.Select(b => b.Rechazo))
                            .Include(v => v.Evento)
                            .Include(v => v.Proveedor)
                            .Include(v => v.Procedencia)
                            .Include(v => v.Destino)
                            .Include(v => v.Carrier)
                            .SingleOrDefault(x => x.Id == IdVentana);

            var fileName = ventana.PO+"_"+ventana.Proveedor.NombreCorto+"_"+ventana.NombreCarrier+ DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";


            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "");
                worksheet = package.Workbook.Worksheets.Add(ventana.Evento.Descripcion);
                worksheet.Row(1).Height = 20;

                worksheet.TabColor = Color.Gold;
                worksheet.DefaultRowHeight = 12;
                worksheet.Row(1).Height = 20;

                worksheet.Cells[1, 1].Value = "Evento";
                worksheet.Cells[1, 2].Value = ventana.Evento.Descripcion;

                worksheet.Cells[2, 1].Value = "PO";
                worksheet.Cells[2, 2].Value = ventana.PO;
                worksheet.Cells[2, 3].Value = ventana.TipoOperacion.Nombre;

                worksheet.Cells[3, 1].Value = "Material";
                worksheet.Cells[3, 2].Value = ventana.Cantidad;
                worksheet.Cells[3, 3].Value = ventana.Proveedor.NombreCorto+" : "+ ventana.Proveedor.Nombre;

                worksheet.Cells[4, 1].Value = "Ubicación";
                worksheet.Cells[4, 2].Value = "Origen: "+ventana.Procedencia.NombreCorto+"-"+ventana.Procedencia.Nombre;
                worksheet.Cells[4, 3].Value = "Destino: " + ventana.Destino.NombreCorto + "-" + ventana.Destino.Nombre;

                worksheet.Cells[5, 1].Value = "Transporte";
                worksheet.Cells[5, 2].Value = "Linea: " + ventana.Carrier.NombreCorto + " " + ventana.NombreCarrier;
                worksheet.Cells[5, 3].Value = "Color: " + ventana.ColorContenedor;
                worksheet.Cells[6, 2].Value = "Sello" + ventana.Sellos;

                worksheet.Cells[7, 1].Value = "";
                worksheet.Cells[7, 2].Value = "Tipo Unidad: " + ventana.TipoUnidad;
                worksheet.Cells[7, 3].Value = "#Economico Tractor: " + ventana.NumeroEconomico;
                worksheet.Cells[8, 2].Value = "#Placa Tractor" + ventana.NumeroPlaca;

                worksheet.Cells[9, 1].Value = "";
                worksheet.Cells[9, 2].Value = "Modelo contenedor: " + ventana.ModeloContenedor;
                worksheet.Cells[9, 3].Value = "#Economico remolque: " + ventana.EconomicoRemolque;

                worksheet.Cells[10, 1].Value = "";
                worksheet.Cells[10, 2].Value = "Dimensión: " + ventana.Dimension;
                worksheet.Cells[10, 3].Value = "Temperatura: " + ventana.Temperatura;

                worksheet.Cells[11, 1].Value = "Conductor";
                worksheet.Cells[11, 2].Value = ventana.Conductor;
                worksheet.Cells[11, 3].Value = ventana.MovilConductor;
                
                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();


                for (var i = 0; i < 11; i++)
                {
                    worksheet.Cells[i + 1, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[i + 1, 1].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[i + 1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MidnightBlue);                    
                }

                package.Workbook.Properties.Title = ventana.Evento.Descripcion;
                this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                this.Response.AddHeader(
                          "content-disposition",
                          string.Format("attachment;  filename={0}", fileName));
                this.Response.BinaryWrite(package.GetAsByteArray());
            }
            return View();
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
