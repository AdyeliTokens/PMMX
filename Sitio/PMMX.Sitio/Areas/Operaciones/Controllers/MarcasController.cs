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
using PMMX.Operaciones.Servicios;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class MarcasController : Controller
    {
        private PMMXContext db = new PMMXContext();


        public ActionResult Index()
        {
            MarcaServicio servicio = new MarcaServicio(db);
            var respuesta = servicio.GetMarcas();
            return View(respuesta.Respuesta.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marcas.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }


        public ActionResult Create()
        {
            return View();
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

                var marcas = new List<Marca>();
                using (var package = new ExcelPackage(file.InputStream))
                {
                    PersonaServicio personaServicio = new PersonaServicio();
                    IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());


                    var currentSheet = package.Workbook.Worksheets;

                    foreach (var workSheet in currentSheet)
                    {
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 4; rowIterator <= noOfRow; rowIterator++)
                        {

                            if (workSheet.Cells[rowIterator, 3].Value != null)
                            {

                                string code_FA = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                                code_FA = code_FA.Replace(" ", "");

                                if (marcas.Where(m => m.Code_FA == code_FA).Count() == 0)
                                {
                                    var marca = new Marca();

                                    marca.IdPersonaQueDioDeAlta = persona.Respuesta.Id;
                                    marca.FechaDeAlta = DateTime.Now;

                                    marca.Code_FA = code_FA;
                                    marca.Descripcion = workSheet.Cells[rowIterator, 4].Value.ToString().Trim();
                                    marca.Codigo_Cigarrillo = workSheet.Cells[rowIterator, 6].Value.ToString().Trim();
                                    marca.Activo = false;

                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                    {
                                        marca.PesoPorCigarrillo = Convert.ToDouble(workSheet.Cells[rowIterator, 9].Value.ToString().Trim());
                                        marca.Activo = true;
                                    }
                                    if (workSheet.Cells[rowIterator, 12].Value != null)
                                    {
                                        marca.PesoTabacco = Convert.ToDouble(workSheet.Cells[rowIterator, 12].Value.ToString().Trim());
                                    }
                                    marcas.Add(marca);
                                }
                            }
                        }

                    }



                    db.Marcas.AddRange(marcas);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Marca marca)
        {
            PersonaServicio personaServicio = new PersonaServicio();
            IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
            marca.IdPersonaQueDioDeAlta = persona.Respuesta.Id;
            if (ModelState.IsValid)
            {

                db.Marcas.Add(marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marca);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marcas.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marca);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = db.Marcas.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marca marca = db.Marcas.Find(id);
            db.Marcas.Remove(marca);
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
