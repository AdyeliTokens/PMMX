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
using System.IO;
using OfficeOpenXml;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace Sitio.Areas.Operaciones.Controllers
{
    public class NoConformidadesController : Controller
    {
        private PMMXContext db = new PMMXContext();

        public ActionResult Index()
        {
            var noConformidades = db.NoConformidades.Include(n => n.Persona).Include(n => n.Seccion).Include(n => n.WorkCenter);
            return View(noConformidades.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoConformidad noConformidad = db.NoConformidades.Find(id);
            if (noConformidad == null)
            {
                return HttpNotFound();
            }
            return View(noConformidad);
        }

        public ActionResult Create()
        {

            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( NoConformidad noConformidad)
        {
            if (ModelState.IsValid)
            {
                PersonaServicio personaServicio = new PersonaServicio();
                IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
                noConformidad.IdPersona = persona.Respuesta.Id;
                db.NoConformidades.Add(noConformidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", noConformidad.IdPersona);
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", noConformidad.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", noConformidad.IdWorkCenter);
            return View(noConformidad);
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

                var noConformidades = new List<NoConformidad>();
                using (var package = new ExcelPackage(file.InputStream))
                {
                    PersonaServicio personaServicio = new PersonaServicio();
                    IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());


                    var currentSheet = package.Workbook.Worksheets;

                    foreach (var workSheet in currentSheet)
                    {
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        int idseccion = 0;

                        for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                        {
                            if (workSheet.Cells[rowIterator, 6].Value != null )
                            {
                                var noconformidad = new NoConformidad();


                                noconformidad.IdPersona = persona.Respuesta.Id;
                                String fechaCadena = workSheet.Cells[rowIterator, 2].Value.ToString().Trim();
                                noconformidad.Fecha = Convert.ToDateTime(fechaCadena);
                                if (workSheet.Cells[rowIterator, 5].Value.ToString().Trim() == "Cigarettes") { idseccion = 2; }
                                else if (workSheet.Cells[rowIterator, 5].Value.ToString().Trim() == "Packs") { idseccion = 1; }
                                noconformidad.IdSeccion = idseccion;


                                noconformidad.Code = workSheet.Cells[rowIterator, 6].Value.ToString().Trim();
                                noconformidad.CodeDescription = workSheet.Cells[rowIterator, 7].Value.ToString().Trim();
                                noconformidad.Calificacion_VQI = Convert.ToInt32(Convert.ToDouble(workSheet.Cells[rowIterator, 8].Value.ToString().Trim()));



                                var str = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                                var palabra = str.Substring(0, str.IndexOf(" "));
                                var wc = palabra.Substring(palabra.Length - 2, 2);

                                //var idWC = aliasWorkCenter.Where(w => w.Nombre == str.Substring(0, str.IndexOf(" "))).Select(a => a.WorkCenters.Select(f=> f.Id).FirstOrDefault()).FirstOrDefault();
                                var idWorkCenter = wcs.Where(w=> w.NombreCorto == wc).Select(w => w.Id).FirstOrDefault(); 
                                noconformidad.IdWorkCenter = idWorkCenter;
            

                                
                                noConformidades.Add(noconformidad);
                            }



                        }

                    }




                    db.NoConformidades.AddRange(noConformidades);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View("Index");
        }




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoConformidad noConformidad = db.NoConformidades.Find(id);
            if (noConformidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", noConformidad.IdPersona);
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", noConformidad.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", noConformidad.IdWorkCenter);
            return View(noConformidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoConformidad noConformidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noConformidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersona = new SelectList(db.Personas, "Id", "Nombre", noConformidad.IdPersona);
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", noConformidad.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", noConformidad.IdWorkCenter);
            return View(noConformidad);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoConformidad noConformidad = db.NoConformidades.Find(id);
            if (noConformidad == null)
            {
                return HttpNotFound();
            }
            return View(noConformidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoConformidad noConformidad = db.NoConformidades.Find(id);
            db.NoConformidades.Remove(noConformidad);
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

        static string TrimSuffix(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }
    }
}
