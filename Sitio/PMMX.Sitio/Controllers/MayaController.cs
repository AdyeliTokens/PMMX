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

        public FileResult Upload()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\PMMX\Aplicaciones\Maya\app-release.apk");
            string fileName = "Maya.apk";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

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
                
            }

            return View("Index");
        }


        public ActionResult Info()
        {
            using (StreamReader r = new StreamReader(@"c:\PMMX\Aplicaciones\Maya\output.json"))
            {
                string json = r.ReadToEnd();
                UpdateMaya update = JsonConvert.DeserializeObject<UpdateMaya>(json);

                return Json(update, JsonRequestBehavior.AllowGet);
            }

        }

    }
}