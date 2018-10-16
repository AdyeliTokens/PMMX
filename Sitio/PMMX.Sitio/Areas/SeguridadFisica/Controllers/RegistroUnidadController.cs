using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
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
        public ActionResult Index(string Codigo)
        {
            var registroUnidad = db.RegistroUnidad
                .Where(r=> r.Formato.Codigo == Codigo)
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
        public ActionResult Create(string Codigo)
        {
            if(Codigo != null)
            {
                ViewBag.IdFormato = new SelectList(db.Formato
                    .Where(x=> x.Codigo == Codigo)
                    .Select(x => new { Id = x.Id, NombreCorto = x.Descripcion })
                    .OrderBy(x => x.NombreCorto), "Id", "NombreCorto");
            }
            else
            {
                ViewBag.IdFormato = new SelectList(db.Formato
                    .Select(x => new { Id = x.Id, NombreCorto = x.Descripcion })
                    .OrderBy(x => x.NombreCorto), "Id", "NombreCorto");
            }           
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

                var _datos = db.DatosUnidad.Where(d => d.IdRegistroUnidad == registroUnidad.Id).FirstOrDefault();
                _datos.NombreConductor = datos.NombreConductor;
                _datos.Placas = datos.Placas;
                _datos.NoEco = datos.NoEco;
                _datos.NoCaja = datos.NoCaja;
                _datos.TipoRemolque = datos.TipoRemolque;
                db.Entry(_datos).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.IdFormato = new SelectList(db.Formato, "Id", "Codigo", registroUnidad.IdFormato);
            return View(registroUnidad);
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

        public ActionResult downloadReport(DateTime Inicio, DateTime Fin, string Codigo)
        {
            List<RegistroUnidad> registros = db.RegistroUnidad
                            .Include(v => v.Formato)
                            .Include(v => v.Bitacora)
                            .Include(v => v.Bitacora.Select(s => s.Guardia))
                            .Include(v => v.Datos)
                            .Where(v => v.Formato.Codigo.Equals(Codigo))
                            .ToList();

            var fileName = "Bitacora_" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "");
                worksheet = package.Workbook.Worksheets.Add(Codigo);
                worksheet.Row(1).Height = 20;

                worksheet.TabColor = Color.Gold;
                worksheet.DefaultRowHeight = 12;
                worksheet.Row(1).Height = 20;

                //Header
                worksheet.Cells[1, 1].Value = "Nombre";
                worksheet.Cells[1, 2].Value = "Empresa";
                worksheet.Cells[1, 3].Value = "Placas";
                worksheet.Cells[1, 4].Value = "#Economico";
                worksheet.Cells[1, 5].Value = "Asunto";
                worksheet.Cells[1, 6].Value = "Nombre Autoriza";
                worksheet.Cells[1, 7].Value = "#Gafette";
                worksheet.Cells[1, 8].Value = "Hora Entrada";
                worksheet.Cells[1, 9].Value = "Hora Salida";

                var fila = 2;

                foreach (var registro in registros)
                {
                    //Content
                    worksheet.Cells[fila, 1].Value = registro.Datos.Select(d => d.NombreConductor).FirstOrDefault().ToUpper();
                    worksheet.Cells[fila, 2].Value = registro.Empresa.ToUpper();
                    worksheet.Cells[fila, 3].Value = registro.Datos.Select(d => d.Placas).FirstOrDefault().ToUpper();
                    worksheet.Cells[fila, 4].Value = registro.Datos.Select(d => d.NoEco).FirstOrDefault().ToUpper();
                    worksheet.Cells[fila, 5].Value = registro.Asunto.ToUpper();
                    worksheet.Cells[fila, 6].Value = registro.NombreAutoriza.ToUpper();
                    worksheet.Cells[fila, 7].Value = registro.NoGafette;
                    worksheet.Cells[fila, 8].Value = registro.Bitacora.OrderByDescending(b=> b.Fecha).Select(d => d.Fecha).LastOrDefault();
                    worksheet.Cells[fila, 9].Value = registro.Bitacora.OrderByDescending(b => b.Fecha).Select(d => d.Fecha).FirstOrDefault();
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

                package.Workbook.Properties.Title = "Bitacora";
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
