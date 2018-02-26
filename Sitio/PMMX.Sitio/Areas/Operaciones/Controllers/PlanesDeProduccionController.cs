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
using OfficeOpenXml;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using Microsoft.AspNet.Identity;
using Sitio.Models;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class PlanesDeProduccionController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Operaciones/PlanesDeProduccion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reporte()
        {
            var planDeProduccion = db.PlanDeProduccion.Include(p => p.Marca_FA).Include(p => p.Uploader).Include(p => p.WorkCenterEfectivo);
            return View(planDeProduccion.ToList());
        }

        // GET: Operaciones/PlanesDeProduccion/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeProduccion planDeProduccion = db.PlanDeProduccion.Find(id);
            if (planDeProduccion == null)
            {
                return HttpNotFound();
            }
            return View(planDeProduccion);
        }

        // GET: Operaciones/PlanesDeProduccion/Create
        public ActionResult Create()
        {
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion");
            ViewBag.IdUploader = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(FechaInicioFin model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    //var aliasWorkCenter = db.Alias.Include(w => w.WorkCenters).ToList();
                    var wcs = db.WorkCenters.ToList();
                    var codes = db.Marcas.ToList();

                    var planes = new List<PlanDeProduccion>();
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        PersonaServicio personaServicio = new PersonaServicio();
                        IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());


                        var currentSheet = package.Workbook.Worksheets;

                        foreach (var workSheet in currentSheet)
                        {
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {

                                if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 1].Value.ToString() != "")
                                {
                                    var str = workSheet.Cells[rowIterator, 7].Value.ToString().Trim();
                                    var wc = str.Substring(str.Length - 2, 2);
                                    var idWorkCenter = wcs.Where(w => w.NombreCorto == wc).Select(w => w.Id).FirstOrDefault();
                                    if (idWorkCenter != null && idWorkCenter > 0)
                                    {

                                        string code_FA = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                                        code_FA = code_FA.Replace(" ", "");
                                        var codeExist = codes.Where(w => w.Code_FA == code_FA).Select(w => w.Code_FA).Count();
                                        if (codeExist > 0)
                                        {
                                            var planDeProduccion = new PlanDeProduccion();

                                            planDeProduccion.IdUploader = persona.Respuesta.Id;
                                            planDeProduccion.FechaSubida = DateTime.Now;

                                            planDeProduccion.Code_FA = code_FA;
                                            planDeProduccion.Codigo = workSheet.Cells[rowIterator, 1].Value.ToString().Trim();
                                            planDeProduccion.Cantidad = Convert.ToDouble(workSheet.Cells[rowIterator, 8].Value.ToString().Trim());
                                            planDeProduccion.Activo = true;
                                            planDeProduccion.ClaseDeOrden = workSheet.Cells[rowIterator, 16].Value.ToString().Trim();
                                            planDeProduccion.Inicio = model.Inicio;
                                            planDeProduccion.Fin = model.Fin;
                                            planDeProduccion.FechaSubida = DateTime.Now;

                                            planDeProduccion.IdWorkCenter = idWorkCenter;


                                            planes.Add(planDeProduccion);
                                        }



                                    }



                                }
                            }

                        }

                        db.PlanDeProduccion.AddRange(planes);
                        db.SaveChanges();
                        return RedirectToAction("Index");


                    }
                }
                else
                {

                    TempData["CustomError"] = "Sube un archivo por favor!!.";
                    return View(model);
                }


            }

            return View(model);
        }

        // POST: Operaciones/PlanesDeProduccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanDeProduccion planDeProduccion)
        {
            if (ModelState.IsValid)
            {
                db.PlanDeProduccion.Add(planDeProduccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", planDeProduccion.Code_FA);
            ViewBag.IdUploader = new SelectList(db.Personas, "Id", "Nombre", planDeProduccion.IdUploader);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", planDeProduccion.IdWorkCenter);
            return View(planDeProduccion);
        }

        // GET: Operaciones/PlanesDeProduccion/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeProduccion planDeProduccion = db.PlanDeProduccion.Find(id);
            if (planDeProduccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", planDeProduccion.Code_FA);
            ViewBag.IdUploader = new SelectList(db.Personas, "Id", "Nombre", planDeProduccion.IdUploader);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", planDeProduccion.IdWorkCenter);
            return View(planDeProduccion);
        }

        // POST: Operaciones/PlanesDeProduccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,IdWorkCenter,Code_FA,Cantidad,ClaseDeOrden,Inicio,Fin,FechaSubida,IdUploader")] PlanDeProduccion planDeProduccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeProduccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", planDeProduccion.Code_FA);
            ViewBag.IdUploader = new SelectList(db.Personas, "Id", "Nombre", planDeProduccion.IdUploader);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", planDeProduccion.IdWorkCenter);
            return View(planDeProduccion);
        }

        // GET: Operaciones/PlanesDeProduccion/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeProduccion planDeProduccion = db.PlanDeProduccion.Find(id);
            if (planDeProduccion == null)
            {
                return HttpNotFound();
            }
            return View(planDeProduccion);
        }

        // POST: Operaciones/PlanesDeProduccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PlanDeProduccion planDeProduccion = db.PlanDeProduccion.Find(id);
            db.PlanDeProduccion.Remove(planDeProduccion);
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
