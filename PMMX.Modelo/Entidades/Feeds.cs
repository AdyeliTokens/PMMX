using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PMMX.Modelo.Entidades
{
    public class Feeds
    {
        public string Title { get; set; }
        public string PublishDate { get; set; }
        public string Description { get; set; }
    }
}
