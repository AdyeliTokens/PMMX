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
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class VolumenesDeProduccionController : Controller
    {
        private PMMXContext db = new PMMXContext();


        public ActionResult Index()
        {

            DateTime diaSeleccionado = DateTime.Now.Date;
            diaSeleccionado = diaSeleccionado.AddDays(1);
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);

            var workCenters = db.WorkCenters.ToList();
            var volumenes = db.VolumenesDeProduccion.Where(x => (x.Fecha >= monday && x.Fecha <= diaSeleccionado)).ToList();
            var objetivos = db.ObjetivosPlanAttainment.Select(x => x).Where(x => (x.FechaInicial >= primerDiaDelAnio)).ToList();
            IList<VolumenDeProduccionPorWorkCenterView> listadelistas = new List<VolumenDeProduccionPorWorkCenterView>();

            foreach (var item in workCenters)
            {

                IList<PlanAttainmentView> cumplimientiAlPlan = new List<PlanAttainmentView>();
                VolumenDeProduccionPorWorkCenterView planPorWorkCenter = new VolumenDeProduccionPorWorkCenterView();
                planPorWorkCenter.WorkCenter = item;

                for (int i = delta; i <= (6 + delta); i++)
                {
                    Double planTotal = volumenes.Where(x => (x.IdWorkCenter == item.Id) && (x.Fecha.Date == diaSeleccionado.AddDays(i).Date)).Sum(o => o.New_Qty);
                    Double objetivo = objetivos.Where(x => (x.IdWorkCenter == item.Id) && (x.FechaInicial <= diaSeleccionado.AddDays(i).Date)).OrderByDescending(x => x.FechaInicial).Select(x => x.Objetivo).FirstOrDefault();

                    cumplimientiAlPlan.Add(new PlanAttainmentView { Fecha = diaSeleccionado.AddDays(i),  Plan_Attainment_Total = planTotal, Objetivo = objetivo });
                }
                planPorWorkCenter.PlanesDeProduccion = new List<PlanAttainmentView>();
                planPorWorkCenter.PlanesDeProduccion.AddRange(cumplimientiAlPlan);
                listadelistas.Add(planPorWorkCenter);
            }
            
            return View(listadelistas);
        }

        public ActionResult Reporte()
        {
            var volumenesDeProduccion = db.VolumenesDeProduccion
                .Include(v => v.MarcaDelCigarrillo)
                .Include(v => v.Reportante)
                .Include(v => v.WorkCenter);
            return View(volumenesDeProduccion.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return HttpNotFound();
            }
            return View(volumenDeProduccion);
        }


        public ActionResult Create()
        {
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion");
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VolumenDeProduccion volumenDeProduccion)
        {
            if (ModelState.IsValid)
            {
                db.VolumenesDeProduccion.Add(volumenDeProduccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", volumenDeProduccion.Code_FA);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", volumenDeProduccion.IdPersona);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", volumenDeProduccion.IdWorkCenter);
            return View(volumenDeProduccion);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", volumenDeProduccion.Code_FA);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", volumenDeProduccion.IdPersona);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", volumenDeProduccion.IdWorkCenter);
            return View(volumenDeProduccion);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VolumenDeProduccion volumenDeProduccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volumenDeProduccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_FA = new SelectList(db.Marcas, "Code_FA", "Descripcion", volumenDeProduccion.Code_FA);
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", volumenDeProduccion.IdPersona);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", volumenDeProduccion.IdWorkCenter);
            return View(volumenDeProduccion);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            if (volumenDeProduccion == null)
            {
                return HttpNotFound();
            }
            return View(volumenDeProduccion);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                //var aliasWorkCenter = db.Alias.Include(w => w.WorkCenters).ToList();
                var wcs = db.WorkCenters.ToList();

                var volumenesDeProduccion = new List<VolumenDeProduccion>();
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

                            if (workSheet.Cells[rowIterator, 3].Value != null)
                            {

                                string code_FA = workSheet.Cells[rowIterator, 2].Value.ToString().Trim();
                                code_FA = code_FA.Replace(" ", "");
                                var voumen = new VolumenDeProduccion();
                                var nombreCorto = workSheet.Cells[rowIterator, 1].Value.ToString().Trim().Substring(32 , 2);
                                var workCenter = wcs.Where(w => w.NombreCorto == nombreCorto).FirstOrDefault();
                                if (workCenter!= null)
                                {
                                    int id = workCenter.Id;
                                    voumen.IdWorkCenter = id;

                                    voumen.IdPersona = persona.Respuesta.Id;
                                    voumen.Fecha = DateTime.Now;


                                    voumen.Container = workSheet.Cells[rowIterator, 1].Value.ToString().Trim();
                                    voumen.Code_FA = code_FA;

                                    voumen.Source_WH = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                                    voumen.Source_Loc = workSheet.Cells[rowIterator, 4].Value.ToString().Trim();
                                    voumen.Dest_WH = workSheet.Cells[rowIterator, 5].Value.ToString().Trim();
                                    voumen.Dest_Loc = workSheet.Cells[rowIterator, 6].Value.ToString().Trim();
                                    voumen.Old_Qty = Convert.ToDouble(workSheet.Cells[rowIterator, 7].Value.ToString().Trim());
                                    voumen.New_Qty = Convert.ToDouble(workSheet.Cells[rowIterator, 8].Value.ToString().Trim());
                                    voumen.UOM = workSheet.Cells[rowIterator, 9].Value.ToString().Trim();
                                    voumen.SAP_Batch = workSheet.Cells[rowIterator, 10].Value.ToString().Trim();


                                    volumenesDeProduccion.Add(voumen);

                                }

                                

                                

                            }
                        }

                    }



                    db.VolumenesDeProduccion.AddRange(volumenesDeProduccion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View("Index");
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VolumenDeProduccion volumenDeProduccion = db.VolumenesDeProduccion.Find(id);
            db.VolumenesDeProduccion.Remove(volumenDeProduccion);
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
