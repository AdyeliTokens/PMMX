using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitio.Areas.Apis.Controllers.Multimedia
{
    public class DefaultController : Controller
    {
        // GET: Apis/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}