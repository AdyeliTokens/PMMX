using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitio.Models
{
    public class UpdateMaya
    {
        public Double NewVersion { get; set; }
        public String url { get; set; }
        public String releaseNotes { get; set; }
        public DateTime Fecha { get; set; }
    }
}