using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Entidades.Maquinaria;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sitio.Areas.Apis.Controllers
{
    public class FotosController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        [ResponseType(typeof(Foto))]
        public async Task<HttpResponseMessage> PostFotoDefecto(int IdDefecto)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string folderName = @"c:\PMMX\Fotos\Defectos";
                            string pathString = Path.Combine(folderName, IdDefecto.ToString());
                            Directory.CreateDirectory(pathString);
                            string fileName = Path.GetRandomFileName();
                            pathString = Path.Combine(pathString, fileName);
                            if (!File.Exists(pathString + extension))
                            {
                                postedFile.SaveAs(pathString + extension);
                            }
                            else
                            {

                                File.Delete(pathString + extension);
                                postedFile.SaveAs(pathString + extension);
                            }

                            Foto foto = new Foto();
                            foto.Fecha = DateTime.Now;
                            foto.Path = pathString + extension;
                            
                            foto.IdMantenimiento = 0;
                            foto.Defectos = new List<Defecto>
                            {
                                db.Defectos.Find(IdDefecto)
                            };
                            foto.Nombre = fileName + extension;



                            db.Fotos.Add(foto);
                            db.SaveChanges();

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }


            //return CreatedAtRoute("DefaultApi", new { id = foto.Id }, foto);
        }

        [ResponseType(typeof(Foto))]
        public async Task<HttpResponseMessage> PostFotoOrigen(int IdOrigen)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string folderName = @"c:\PMMX\Fotos\Origenes";
                            string pathString = Path.Combine(folderName, IdOrigen.ToString());
                            Directory.CreateDirectory(pathString);
                            string fileName = System.IO.Path.GetRandomFileName();
                            pathString = System.IO.Path.Combine(pathString, fileName);
                            if (!File.Exists(pathString + extension))
                            {
                                postedFile.SaveAs(pathString + extension);
                            }
                            else
                            {

                                File.Delete(pathString + extension);
                                postedFile.SaveAs(pathString + extension);
                            }

                            Foto foto = new Foto();
                            foto.Fecha = DateTime.Now;
                            foto.Path = pathString + extension;
                            
                            foto.IdMantenimiento = 0;
                            foto.Origenes = new List<Origen>
                            {
                                db.Origens.Find(IdOrigen)
                            };
                            foto.Nombre = fileName + extension;



                            db.Fotos.Add(foto);
                            db.SaveChanges();

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }


            //return CreatedAtRoute("DefaultApi", new { id = foto.Id }, foto);
        }

        [ResponseType(typeof(Foto))]
        public async Task<HttpResponseMessage> PostFotoPersona(int IdPersona)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string folderName = @"c:\PMMX\Fotos\Personas";
                            string pathString = Path.Combine(folderName, IdPersona.ToString());
                            Directory.CreateDirectory(pathString);
                            string fileName = System.IO.Path.GetRandomFileName();
                            pathString = System.IO.Path.Combine(pathString, fileName);
                            if (!File.Exists(pathString + extension))
                            {
                                postedFile.SaveAs(pathString + extension);
                            }
                            else
                            {

                                File.Delete(pathString + extension);
                                postedFile.SaveAs(pathString + extension);
                            }

                            Foto foto = new Foto();
                            foto.Fecha = DateTime.Now;
                            foto.Path = pathString + extension;
                            
                            foto.IdMantenimiento = 0;
                            foto.Personas = new List<Persona>
                            {
                                db.Personas.Find(IdPersona)
                            };
                            foto.Nombre = fileName + extension;



                            db.Fotos.Add(foto);
                            db.SaveChanges();

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        public async Task<HttpResponseMessage> PostFotoJustDoIt(int IdJustDoIt)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string folderName = @"c:\PMMX\Fotos\JustDoIt";
                            string pathString = Path.Combine(folderName, IdJustDoIt.ToString());
                            Directory.CreateDirectory(pathString);
                            string fileName = Path.GetRandomFileName();
                            pathString = Path.Combine(pathString, fileName);
                            if (!File.Exists(pathString + extension))
                            {
                                postedFile.SaveAs(pathString + extension);
                            }
                            else
                            {

                                File.Delete(pathString + extension);
                                postedFile.SaveAs(pathString + extension);
                            }

                            Foto foto = new Foto();
                            foto.Fecha = DateTime.Now;
                            foto.Path = pathString + extension;
                            
                            foto.IdMantenimiento = 0;
                            foto.Defectos = new List<Defecto>
                            {
                                db.Defectos.Find(IdJustDoIt)
                            };
                            foto.Nombre = fileName + extension;
                            
                            db.Fotos.Add(foto);
                            db.SaveChanges();

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }


            //return CreatedAtRoute("DefaultApi", new { id = foto.Id }, foto);
        }



        [ResponseType(typeof(Foto))]
        public async Task<HttpResponseMessage> PostMantenimiento(int IdMantenimiento)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string folderName = @"c:\Fotos\Mantenimiento";
                            string pathString = System.IO.Path.Combine(folderName, IdMantenimiento.ToString());
                            System.IO.Directory.CreateDirectory(pathString);
                            pathString = System.IO.Path.Combine(pathString, IdMantenimiento.ToString());
                            if (!System.IO.File.Exists(pathString))
                            {
                                System.IO.File.Create(pathString);
                                postedFile.SaveAs(pathString + extension);
                            }
                            else
                            {
                                System.IO.File.Delete(pathString + extension);
                                postedFile.SaveAs(pathString + extension);
                            }

                            Foto foto = new Foto();
                            foto.Path = pathString + extension;
                            foto.IdMantenimiento = IdMantenimiento;
                            
                            foto.Nombre = IdMantenimiento.ToString() + extension;

                            db.Fotos.Add(foto);
                            db.SaveChanges();

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex.ToString());
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}
