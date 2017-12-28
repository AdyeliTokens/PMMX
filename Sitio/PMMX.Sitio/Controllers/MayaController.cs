using Newtonsoft.Json;
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

            using (StreamReader sr = new StreamReader(@"c:\PMMX\Aplicaciones\Maya\info.json"))
            {
                
                return Json(sr.ReadToEnd(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}