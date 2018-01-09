using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.InsiteLAC;
using OfficeOpenXml;
using Sitio.Models;

namespace Sitio.Controllers
{
    public class InSiteLACController : Controller
    {
        private PMMXContext db = new PMMXContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listado()
        {

            if (Request.IsAjaxRequest())
                return PartialView(db.KPIs.ToList());
            else
                return View(db.KPIs.ToList());


        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(FechaModel model ,HttpPostedFileBase file)
        {
            if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                var kpis = new List<KPI>();
                using (var package = new ExcelPackage(file.InputStream))
                {


                    var currentSheet = package.Workbook.Worksheets;
                    foreach (var workSheet in currentSheet)
                    {
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {


                            var kpi = new KPI();

                            kpi.Description = workSheet.Cells[rowIterator, 1].Value.ToString().Trim();
                            kpi.YTD = Convert.ToDouble(workSheet.Cells[rowIterator, 2].Value.ToString().Trim());
                            kpi.Mes_Efectivo = model.Mes;
                            kpi.Anio_Efectivo = model.Anio;

                            kpis.Add(kpi);
                            
                        }
                    }
                    
                    db.KPIs.AddRange(kpis);
                    db.SaveChanges();
                    return RedirectToAction("Listado");
                }
            }

            return View("Listado");
        }



        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return HttpNotFound();
            }
            return View(kPI);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,YTD,Mes_Efectivo,Anio_Efectivo")] KPI kPI)
        {
            if (ModelState.IsValid)
            {
                db.KPIs.Add(kPI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kPI);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return HttpNotFound();
            }
            return View(kPI);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,YTD,Mes_Efectivo,Anio_Efectivo")] KPI kPI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kPI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kPI);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KPI kPI = db.KPIs.Find(id);
            if (kPI == null)
            {
                return HttpNotFound();
            }
            return View(kPI);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KPI kPI = db.KPIs.Find(id);
            db.KPIs.Remove(kPI);
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

        public PartialViewResult SolicitudEnviada()
        {
            return PartialView();
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\PMMX\Aplicaciones\AugmentedReality\AR.apk");
            string fileName = "AR.apk";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }
    }
}
