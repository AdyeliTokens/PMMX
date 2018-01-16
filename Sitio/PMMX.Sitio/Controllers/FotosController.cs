using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Seguridad.Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sitio.Controllers
{
    public class FotosController : Controller
    {
        // GET: FotosPersonales
        public ActionResult Defectos(string idDefecto)
        {
            int DefectoId = Convert.ToInt32(idDefecto);
            PMMXContext db = new PMMXContext();

            var fotos = db.Defectos.Where(p => p.Id == DefectoId).Select(p => p.Fotos.Where(f => f.Fecha != null)).FirstOrDefault();

            string folderName;
            if (fotos.Count() > 0)
            {
                folderName = fotos.OrderByDescending(w => w.Fecha).FirstOrDefault().Path;
                if (!System.IO.File.Exists(folderName))
                {
                    folderName = Server.MapPath("~/img/default.png");
                }
            }
            else
            {
                folderName = Server.MapPath("~/img/default.png");
            }


            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

        // GET: FotosJustDoIt
        public ActionResult JustDoIt(string idJustDoIt)
        {
            int Id = Convert.ToInt32(idJustDoIt);
            PMMXContext db = new PMMXContext();

            var fotos = db.JustDoIt.Where(p => p.Id == Id).Select(p => p.Fotos.Where(f => f.Fecha != null)).FirstOrDefault();

            string folderName;
            if (fotos.Count() > 0)
            {
                folderName = fotos.OrderByDescending(w => w.Fecha).FirstOrDefault().Path;

                if (!System.IO.File.Exists(folderName))
                {
                    folderName = Server.MapPath("~/img/default.png");
                }
            }
            else
            {
                folderName = Server.MapPath("~/img/default.png");
            }

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

        // GET: FotosPersonales
        public ActionResult Mantenimiento(string idMantenimiento)
        {
            string folderName = @"c:\Fotos\Mantenimiento";
            folderName = System.IO.Path.Combine(folderName, idMantenimiento);

            string pngName = System.IO.Path.Combine(folderName, idMantenimiento + ".png");
            string jpgName = System.IO.Path.Combine(folderName, idMantenimiento + ".jpg");
            folderName = System.IO.File.Exists(pngName) ? pngName : jpgName;

            if (!System.IO.File.Exists(folderName))
            {
                folderName = Server.MapPath("~/img/default.png");
            }

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

        public ActionResult Origenes(string idOrigen)
        {
            int OrigenId = Convert.ToInt32(idOrigen);
            PMMXContext db = new PMMXContext();
            string folderName;
            var fotos = db.Origens.Where(p => (p.Id == OrigenId)).Select(p => p.Fotos.Where(f => f.Fecha != null)).FirstOrDefault();
            if (fotos.Count() > 0)
            {
                folderName = fotos.OrderByDescending(w => w.Fecha).FirstOrDefault().Path;
                if (!System.IO.File.Exists(folderName))
                {
                    folderName = Server.MapPath("~/img/default.png");
                }
            }
            else
            {
                folderName = Server.MapPath("~/img/default.png");
            }





            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

        public ActionResult Personas(string idPersona)
        {
            int PersonaID = Convert.ToInt32(idPersona);
            PMMXContext db = new PMMXContext();

            var fotos = db.Personas.Where(p => p.Id == PersonaID).Select(p => p.FotosPersonales.Where(f => f.Fecha != null)).FirstOrDefault();

            string folderName;
            if (fotos.Count() > 0)
            {
                folderName = fotos.OrderByDescending(w => w.Fecha).FirstOrDefault().Path;
                if (!System.IO.File.Exists(folderName))
                {
                    folderName = Server.MapPath("~/img/user-default.png");
                }
            }
            else
            {
                folderName = Server.MapPath("~/img/user-default.png");
            }



            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

        public ActionResult Usuario(string idUser)
        {
            string folderName;
            PMMXContext db = new PMMXContext();

            PersonaServicio personaServicio = new PersonaServicio();
            IRespuestaServicio<Persona> persona = personaServicio.GetPersona(idUser);
            if (persona.EjecucionCorrecta)
            {
                var fotos = db.Personas.Where(p => p.Id == persona.Respuesta.Id).Select(p => p.FotosPersonales.Where(f => f.Fecha != null)).FirstOrDefault();

                if (fotos.Count() > 0)
                {
                    folderName = fotos.OrderByDescending(w => w.Fecha).FirstOrDefault().Path;
                    if (!System.IO.File.Exists(folderName))
                    {
                        folderName = Server.MapPath("~/img/user-default.png");
                    }
                }
                else
                {
                    folderName = Server.MapPath("~/img/user-default.png");
                }
            }
            else
            {
                folderName = Server.MapPath("~/img/user-default.png");
            }



            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

        public ActionResult Marcas(string idMarca)
        {

            string folderName = Server.MapPath("~/img/default.png");
            
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(folderName);
            Bitmap bitmap; bitmap = new Bitmap(stream);
            return base.File(folderName, "image/png");
        }

    }
}