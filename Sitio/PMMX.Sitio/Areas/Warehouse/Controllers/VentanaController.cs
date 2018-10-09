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
using System.Drawing;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.Globalization;
using Sitio.Models;

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

            var ventana = db.Ventana
                 .Where(e => e.Id == id)
                 .Include(e => e.Evento)
                 .Include(e => e.Proveedor)
                 .Include(e => e.Carrier)
                 .Include(e => e.Destino)
                 .Include(e => e.Procedencia)
                 .Include(e => e.SubCategoria)
                 .Include(e => e.TipoOperacion)
                 .Include(e => e.StatusVentana)
                 .Include(e => e.StatusVentana.Select(s => s.Status))
                 .Include(e => e.BitacoraVentana)
                 .Include(e => e.BitacoraVentana.Select(b => b.Estatus))
                 .Include(e => e.BitacoraVentana.Select(b => b.Rechazo))
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

                return RedirectToAction("Index", "Evento", new { Area = "Operaciones" });
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
            ViewBag.IdOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdOperacion);

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
                return RedirectToAction("Index", "Evento", new { Area = "Operaciones" });
            }

            ViewBag.IdProveedor = new SelectList(db.Proveedores.Select(x => new { Id = x.Id, NombreCorto = x.NombreCorto }).OrderBy(x => x.NombreCorto), "Id", "NombreCorto", ventana.IdProveedor);
            ViewBag.IdProcedencia = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdProcedencia);
            ViewBag.IdDestino = new SelectList(db.Locacion.Select(x => new { Id = x.Id, Nombre = (x.NombreCorto + " " + x.Nombre) }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdDestino);
            ViewBag.IdCarrier = new SelectList(db.Carrier.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdCarrier);
            ViewBag.IdSubCategoria = new SelectList(db.SubCategoria.Where(x => (x.IdCategoria == 10)).Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdSubCategoria);
            ViewBag.IdOperacion = new SelectList(db.TipoOperacion.Select(x => new { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre), "Id", "Nombre", ventana.IdOperacion);

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

        public ActionResult Reporte()
        {
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

                        if (workSheet.Cells[3, 2].Value != null)
                        {
                            ventana.PO = workSheet.Cells[2, 2].Value == null ? string.Empty : workSheet.Cells[2, 2].Value.ToString().Trim();
                            var numProveedor = Convert.ToInt32(workSheet.Cells[3, 2].Value.ToString().Trim());
                            ventana.IdProveedor = db.Proveedores.Where(p => (p.NumeroProveedor == numProveedor)).Select(p => p.Id).FirstOrDefault();
                            ventana.Recurso = workSheet.Cells[4, 2].Value == null ? string.Empty : workSheet.Cells[4, 2].Value.ToString().Trim();
                            ventana.Cantidad = workSheet.Cells[5, 2].Value == null ? string.Empty : workSheet.Cells[5, 2].Value.ToString().Trim();
                            ventana.NombreCarrier = workSheet.Cells[6, 2].Value == null ? string.Empty : workSheet.Cells[6, 2].Value.ToString().Trim();
                            ventana.IdCarrier = db.Carrier.Where(c => c.NombreCorto == ventana.NombreCarrier).Select(c => c.Id).FirstOrDefault() == 0 
                                ? db.Carrier.Where(c => c.NombreCorto == "NO CONFIRMADO").Select(c => c.Id).FirstOrDefault() 
                                : db.Carrier.Where(c => c.NombreCorto == ventana.NombreCarrier).Select(c => c.Id).FirstOrDefault();
                            ventana.Conductor = workSheet.Cells[7, 2].Value == null ? string.Empty : workSheet.Cells[7, 2].Value.ToString().Trim();
                            ventana.MovilConductor = workSheet.Cells[8, 2].Value == null ? string.Empty : workSheet.Cells[8, 2].Value.ToString().Trim();
                            var nombreCorto = workSheet.Cells[9, 2].Value == null ? "MX" : workSheet.Cells[9, 2].Value.ToString().Trim();
                            ventana.IdProcedencia = db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault() == 0 
                                ? db.Locacion.Where(l => l.NombreCorto == "MX").Select(l => l.Id).FirstOrDefault() 
                                : db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault();
                            nombreCorto = workSheet.Cells[10, 2].Value == null ? "MX" : workSheet.Cells[10, 2].Value.ToString().Trim();
                            ventana.IdDestino = db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault() == 0 
                                ? db.Locacion.Where(l => l.NombreCorto == "MX").Select(l => l.Id).FirstOrDefault() 
                                : db.Locacion.Where(l => l.NombreCorto == nombreCorto).Select(l => l.Id).FirstOrDefault();
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

        public void downloadDataVentana(int IdVentana)
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

            PdfDocument document = new PdfDocument();
            document = QM0474.CreateDocument(ventana);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            Response.End();
        }

        public ActionResult Report()
        {
            FechaInicioFin model = new FechaInicioFin();
            model.Inicio = DateTime.Now.Date;
            model.Fin = DateTime.Now.Date.AddDays(1);
            return View(model);
        }

        public ActionResult downloadReport(DateTime Inicio, DateTime Fin)
        {
            List<Ventana> ventanas = db.Ventana
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
                            .Include(v => v.SubCategoria)
                            .Where(v => v.Evento.FechaInicio >= Inicio && v.Evento.FechaFin <= Fin)
                            .ToList();

            var fileName = "ReportedeAccesos_" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "");
                worksheet = package.Workbook.Worksheets.Add("Reporte");
                worksheet.Row(1).Height = 20;

                worksheet.TabColor = Color.Gold;
                worksheet.DefaultRowHeight = 12;
                worksheet.Row(1).Height = 20;

                //Header
                worksheet.Cells[1, 1].Value = "Nombre del Conductor";
                worksheet.Cells[1, 2].Value = "Fecha";
                worksheet.Cells[1, 3].Value = "Linea";
                worksheet.Cells[1, 4].Value = "Almacen";
                worksheet.Cells[1, 5].Value = "Proveedor";
                worksheet.Cells[1, 6].Value = "Ventana";
                worksheet.Cells[1, 7].Value = "Tipo";
                worksheet.Cells[1, 8].Value = "Horario Datos en Ventana";
                worksheet.Cells[1, 9].Value = "Producto";
                worksheet.Cells[1, 10].Value = "Horario de llamada 1";
                worksheet.Cells[1, 11].Value = "Horario de llamada 2";
                worksheet.Cells[1, 12].Value = "Hora de Reporte en C3";
                worksheet.Cells[1, 13].Value = "Hora de Ingreso";
                worksheet.Cells[1, 14].Value = "Motivo de No ingreso";

                var fila = 2;

                foreach (var ventana in ventanas)
                {
                    //Content
                    worksheet.Cells[fila, 1].Value = ventana.Conductor.ToUpper();
                    worksheet.Cells[fila, 2].Value = ventana.Evento.FechaInicio.ToString();
                    worksheet.Cells[fila, 3].Value = ventana.NombreCarrier.ToUpper();
                    worksheet.Cells[fila, 4].Value = "ALMACÉN";
                    worksheet.Cells[fila, 5].Value = ventana.Proveedor.NombreCorto.ToUpper();
                    var date = new System.DateTime(ventana.Evento.FechaInicio.Year, ventana.Evento.FechaInicio.Month, ventana.Evento.FechaInicio.Day);
                    double result = ventana.Evento.FechaInicio.Subtract(date).TotalSeconds;
                    worksheet.Cells[fila, 6].Value = result >= 32400 ? "9 a 15" : result >= 54000 ? "17 a 22" : "22 a 9";
                    worksheet.Cells[fila, 7].Value = ventana.SubCategoria.Nombre.ToUpper();
                    worksheet.Cells[fila, 8].Value = ventana.StatusVentana.OrderByDescending(s=> s.Fecha).Where(s=> s.IdStatus == 3).Select(s=> s.Fecha).FirstOrDefault().ToString();
                    worksheet.Cells[fila, 9].Value = ventana.Recurso.ToUpper();
                    worksheet.Cells[fila, 10].Value = ventana.StatusVentana.OrderByDescending(s => s.Fecha).Where(s => s.IdStatus == 4).Select(s => s.Fecha).FirstOrDefault().ToString();
                    worksheet.Cells[fila, 11].Value = "";
                    worksheet.Cells[fila, 12].Value = ventana.StatusVentana.OrderByDescending(s => s.Fecha).Where(s => s.IdStatus == 4).Select(s => s.Fecha).FirstOrDefault().ToString(); 
                    worksheet.Cells[fila, 13].Value = ventana.StatusVentana.OrderByDescending(s => s.Fecha).Where(s => s.IdStatus == 11).Select(s => s.Fecha).FirstOrDefault().ToString();
                    worksheet.Cells[fila, 14].Value = ventana.BitacoraVentana.OrderBy(b => b.Fecha).Select(b => b.Rechazo.Nombre).FirstOrDefault();
                    fila++;
                }
                
                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();


                for (var i = 0; i < 14; i++)
                {
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MidnightBlue);
                }

                package.Workbook.Properties.Title = "Reporte";
                this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                this.Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", fileName));
                this.Response.BinaryWrite(package.GetAsByteArray());
                this.Response.Flush();
                this.Response.Close();
                this.Response.End();
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
