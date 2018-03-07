using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.RespuestaGenerica;
using Microsoft.AspNet.Identity;
using PMMX.Modelo.Vistas;
using PMMX.Operaciones.Servicios;
using Sitio.Models;

namespace Sitio.Areas.Operaciones.Controllers
{
    [Authorize]
    public class DesperdiciosController : Controller
    {
        private PMMXContext db = new PMMXContext();

        
        public ActionResult Index()
        {
            FechaInicioFin model = new FechaInicioFin();
            model.Inicio = DateTime.Now.Date;
            model.Fin = DateTime.Now.Date.AddDays(1);
            return View(model);
            
        }

        [HttpPost]
        public ActionResult Reporte(FechaInicioFin model)
        {
            DateTime fechaFin = model.Fin;
            fechaFin = fechaFin.AddDays(1);

           
            DateTime fechaInicio = model.Inicio;
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);


            var workCenters = db.WorkCenters.ToList();
            var desperdicios = db.Desperdicios.Where(x => (x.Fecha >= fechaInicio && x.Fecha <= fechaFin)).ToList();
            var objetivos = db.ObjetivosCRR.Select(x => x).Where(x => (x.FechaInicial >= primerDiaDelAnio)).ToList();
            var volumenes = db.VolumenesDeProduccion.Where(x => (x.Fecha <= fechaFin && x.Fecha >= fechaInicio)).ToList();

            IList<CRRPorWorkCenter> listadelistas = new List<CRRPorWorkCenter>();
            foreach (var item in workCenters)
            {
                CRRPorWorkCenter crrPorWorkCenter = new CRRPorWorkCenter();
                crrPorWorkCenter.WorkCenter = item;
                var desperdicioTotal = desperdicios
                    .Where(x => (x.IdWorkCenter == item.Id))
                    .GroupBy(rd => rd.Code_FA, rd => rd.Cantidad, (code, cant) => new DesperdicioView
                    {
                        Code_FA = code,
                        Cantidad = cant.Sum()
                    })
                    .ToList();

                var VolumenesTotal = volumenes
                    .Where(x => (x.IdWorkCenter == item.Id))
                    .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new DesperdicioView
                    {
                        Code_FA = code,
                        Cantidad = Math.Round((desperdicios.Where(d => d.Code_FA == code && d.IdWorkCenter == item.Id).Select(d => d.Cantidad).Sum() / (cant.Sum() * 1000)), 2)

                


                    })
                    .ToList();




                crrPorWorkCenter.Desperdicio = new List<DesperdicioView>();
                crrPorWorkCenter.Desperdicio = VolumenesTotal;

                listadelistas.Add(crrPorWorkCenter);
            }

            return PartialView(listadelistas);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            return View(desperdicio);
        }

        public ActionResult GetCode_FA(int idWorkCenter)
        {
            if (ModelState.IsValid)
            {
                DateTime hoy = DateTime.Now.Date;
                var lista = db.PlanDeProduccion.Where(x =>(x.Inicio <= hoy && x.Fin >= hoy) && (x.IdWorkCenter == idWorkCenter) )
                            .Select(x => new { Code_FA = x.Code_FA, Descripcion = x.Code_FA + " - " + x.Marca_FA.Descripcion })
                            .Distinct()
                            .ToList();

                return Json(new { lista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            DateTime hoy = DateTime.Now.Date;
            
            
            ViewBag.Code_FA = new SelectList(db.PlanDeProduccion.Where(x=> x.Inicio < hoy && x.Fin > hoy).Select(x => new { Code_FA = x.Code_FA, Descripcion = x.Code_FA + " - " + x.Marca_FA.Descripcion }), "Code_FA", "Descripcion");
            
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Desperdicio desperdicio)
        {
            PersonaServicio personaServicio = new PersonaServicio();
            IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
            desperdicio.IdPersona = persona.Respuesta.Id;
            desperdicio.Fecha = DateTime.Now;
            DateTime hoy = DateTime.Now.Date;
            if (ModelState.IsValid)
            {

                db.Desperdicios.Add(desperdicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_FA = new SelectList(db.PlanDeProduccion.Where(x => x.Inicio < hoy && x.Fin > hoy).Select(x => new { Code_FA = x.Code_FA, Descripcion = x.Code_FA + " - " + x.Marca_FA.Descripcion }), "Code_FA", "Descripcion");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", desperdicio.IdWorkCenter);
            return View(desperdicio);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_FA = new SelectList(db.Marcas.Select(x => new { Code_FA = x.Code_FA, Descripcion = x.Code_FA + " - " + x.Descripcion }), "Code_FA", "Descripcion");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "NombreCorto", desperdicio.IdWorkCenter);
            return View(desperdicio);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Desperdicio desperdicio)
        {
            PersonaServicio personaServicio = new PersonaServicio();
            IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());
            desperdicio.IdPersona = persona.Respuesta.Id;
            desperdicio.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(desperdicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_FA = new SelectList(db.Marcas.Select(x => new { Code_FA = x.Code_FA, Descripcion = x.Code_FA + " - " + x.Descripcion }), "Code_FA", "Descripcion");
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "NombreCorto", desperdicio.IdWorkCenter);
            return View(desperdicio);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            if (desperdicio == null)
            {
                return HttpNotFound();
            }
            return View(desperdicio);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Desperdicio desperdicio = db.Desperdicios.Find(id);
            db.Desperdicios.Remove(desperdicio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
