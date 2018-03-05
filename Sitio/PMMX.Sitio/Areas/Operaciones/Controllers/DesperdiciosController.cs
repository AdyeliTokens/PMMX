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

namespace Sitio.Areas.Operaciones.Controllers
{
    public class DesperdiciosController : Controller
    {
        private PMMXContext db = new PMMXContext();


        public ActionResult Index()
        {
            DateTime diaSeleccionado = DateTime.Now.Date;
            diaSeleccionado = diaSeleccionado.AddDays(1);

            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);
            

            var workCenters = db.WorkCenters.ToList();
            var desperdicios = db.Desperdicios.Where(x => (x.Fecha <= monday && x.Fecha >= diaSeleccionado)).ToList();
            var objetivos = db.ObjetivosCRR.Select(x => x).Where(x => (x.FechaInicial >= primerDiaDelAnio)).ToList();
            var volumenes = db.VolumenesDeProduccion.Where(x => (x.Fecha <= diaSeleccionado  && x.Fecha >= monday)).ToList();

            IList<CRRPorWorkCenter> listadelistas = new List<CRRPorWorkCenter>();
            foreach (var item in workCenters)
            {
                CRRPorWorkCenter crrPorWorkCenter = new CRRPorWorkCenter();
                crrPorWorkCenter.WorkCenter = item;
                var desperdicioTotal = desperdicios
                    .Where(x => (x.IdWorkCenter == item.Id))
                    .GroupBy(rd => rd.Code_FA, rd => rd.Cantidad ,(code, cant ) => new DesperdicioView {
                        Code_FA = code,
                        Cantidad = cant.Sum()
                    })
                    .ToList();

                var VolumenesTotal = volumenes
                    .Where(x => (x.IdWorkCenter == item.Id))
                    .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new DesperdicioView
                    {
                        Code_FA = code,
                        Cantidad = desperdicios.Where(d => d.Code_FA == code).Select(d => d.Cantidad).Sum() / cant.Sum()
                        
                        
                    })
                    .ToList();

                


                crrPorWorkCenter.Desperdicio = new List<DesperdicioView>();
                crrPorWorkCenter.Desperdicio = VolumenesTotal;

                listadelistas.Add(crrPorWorkCenter);
            }

            return View(listadelistas);
            
        }

        public ActionResult Reporte()
        {
            var desperdicios = db.Desperdicios.Include(d => d.MarcaDelCigarrillo).Include(d => d.Reportante).Include(d => d.Seccion).Include(d => d.WorkCenter);
            return View(desperdicios.ToList());
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
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
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
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", desperdicio.IdWorkCenter);
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
            ViewBag.IdSeccion = new SelectList(db.ModuloSeccion, "Id", "Nombre", desperdicio.IdSeccion);
            ViewBag.IdWorkCenter = new SelectList(db.WorkCenters, "Id", "Nombre", desperdicio.IdWorkCenter);
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
