using PMMX.Infraestructura.Contexto;
using PMMX.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitio.Controllers
{
    public class AugmentedRealityController : Controller
    {
        // GET: ArgumentReality
        public ActionResult Index()
        {
            return View();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Create([Bind(Include = "Id,Nombre,Comentario,Email")] RequisicionDeDescarga requisicionDeDescarga)
        {
            
            if (ModelState.IsValid)
            {
                PMMXContext db = new PMMXContext();
                requisicionDeDescarga.Solicitud = DateTime.Now;
                db.RequisicionDeDescargas.Add(requisicionDeDescarga);
                db.SaveChanges();
                return PartialView("SolicitudEnviada");
            }

            return PartialView(requisicionDeDescarga);
        }
    }
}