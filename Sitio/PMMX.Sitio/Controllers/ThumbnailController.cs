using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sitio.Controllers
{
    public class ThumbnailController : Controller
    {
        // GET: Thumbnail
        public ActionResult Defectos(string idDefecto)
        {
            string folderName = @"c:\FotosPersonales\Defectos";

            folderName = System.IO.Path.Combine(folderName, idDefecto);
            folderName = System.IO.Path.Combine(folderName, idDefecto + ".png");

            if (!System.IO.File.Exists(folderName))
            {
                folderName = Server.MapPath("~/img/default.png");
            }


            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);


            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Image image = new Bitmap(@"c:\FakePhoto.jpg");
            Image pThumbnail = image.GetThumbnailImage(100, 100, callback, new IntPtr());

            return base.File(folderName, "image/png");



            
            
            
        }

        public ActionResult Origenes(string idOrigen)
        {
            string folderName = @"c:\PMMX\FotosPersonales\Origenes";

            folderName = System.IO.Path.Combine(folderName, idOrigen);
            folderName = System.IO.Path.Combine(folderName, idOrigen + ".png");

            if (!System.IO.File.Exists(folderName))
            {
                folderName = Server.MapPath("~/img/default.png");
            }


            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

        public bool ThumbnailCallback()
        {
            return true;
        }
    }
}