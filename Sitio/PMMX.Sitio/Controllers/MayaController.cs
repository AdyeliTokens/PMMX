using Newtonsoft.Json;
using Sitio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitio.Controllers
{
    public class MayaController : Controller
    {
        // GET: Maya
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\PMMX\Aplicaciones\Maya\Maya.apk");
            string fileName = "Maya.apk";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        public ActionResult Info()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\PMMX\Aplicaciones\Maya\info.json");

            UpdateMaya update = new UpdateMaya
            {
                NewVersion = 1.02,
                Fecha = DateTime.Now,
                url = "",
                releaseNotes = ""
            };

            
            return Json(update, JsonRequestBehavior.AllowGet);

        }

    }
}