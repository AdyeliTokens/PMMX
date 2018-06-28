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

        [HttpPost]
        public ActionResult ReporteAnual(FechaInicioFin model)
        {
            DateTime fechaFin = model.Fin;
            fechaFin = fechaFin.AddDays(1);

            DateTime fechaInicio = model.Inicio;
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);

            var businessUnits = db.BussinesUnits.ToList();
            var desperdicios = db.Desperdicios.Where(x => (x.Fecha >= fechaInicio && x.Fecha <= fechaFin)).ToList();
            var objetivos = db.ObjetivosCRR.Select(x => x).Where(x => (x.FechaInicial >= primerDiaDelAnio)).ToList();
            var volumenes = db.VolumenesDeProduccion.Include(x=> x.WorkCenter).Where(x => (x.Fecha <= fechaFin && x.Fecha >= fechaInicio)).ToList();
            var planes = db.PlanDeProduccion.Where(x => (x.Inicio <= fechaInicio && x.Fin >= fechaFin)).ToList();

            IList<CRRPorBusinessUnit> listaBU = new List<CRRPorBusinessUnit>();
            foreach (var businessUnit in businessUnits)
            {
                CRRPorBusinessUnit crrPorBusinessUnit = new CRRPorBusinessUnit();
                crrPorBusinessUnit.BusinessUnit = businessUnit;

                var workCenters = db.WorkCenters.Where(w => (w.IdBussinesUnit == businessUnit.Id)).ToList();

                var VolumenesBU = volumenes
                        .Where(x => (x.WorkCenter.IdBussinesUnit == businessUnit.Id))
                        .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new ReporteTotalView
                        {
                            Code_FA = code,
                            CRR = Math.Round((desperdicios.Where(d => d.Code_FA == code && d.WorkCenter.IdBussinesUnit == businessUnit.Id).Select(d => d.Cantidad).Sum() / (cant.Sum() * 1000)), 2),
                            PlanProduccion = Math.Round((planes.Where(d => d.Code_FA == code && d.WorkCenterEfectivo.IdBussinesUnit == businessUnit.Id).Select(d => d.Cantidad).Sum()), 2),
                            VolumenProduccion = Math.Round(cant.Sum() * 1000, 2)
                        })
                        .ToList();

                IList<CRRPorWorkCenter> listadelistas = new List<CRRPorWorkCenter>();
                foreach (var item in workCenters)
                {
                    CRRPorWorkCenter crrPorWorkCenter = new CRRPorWorkCenter();
                    crrPorWorkCenter.WorkCenter = item;

                    var VolumenesTotal = volumenes
                        .Where(x => (x.IdWorkCenter == item.Id))
                        .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new ReporteTotalView
                        {
                            Code_FA = code,
                            CRR = Math.Round((desperdicios.Where(d => d.Code_FA == code && d.IdWorkCenter == item.Id).Select(d => d.Cantidad).Sum() / (cant.Sum() * 1000)), 2),
                            PlanProduccion = Math.Round((planes.Where(d => d.Code_FA == code && d.IdWorkCenter == item.Id).Select(d => d.Cantidad).Sum()), 2),
                            VolumenProduccion = Math.Round(cant.Sum() * 1000, 2)
                        })
                        .ToList();

                    crrPorWorkCenter.Valores = new List<ReporteTotalView>();
                    crrPorWorkCenter.Valores = VolumenesTotal;
                    listadelistas.Add(crrPorWorkCenter);
                }

                crrPorBusinessUnit.Valores = new List<ReporteTotalView>();
                crrPorBusinessUnit.Valores = VolumenesBU;
                crrPorBusinessUnit.ValoresWorkCenters = new List<CRRPorWorkCenter>();
                crrPorBusinessUnit.ValoresWorkCenters = listadelistas;
                listaBU.Add(crrPorBusinessUnit);
            }

            return PartialView(listaBU);
        }

        public ActionResult Parametros(DateTime Inicio)
        {
            if (ModelState.IsValid)
            {
                int dia = (Convert.ToInt32(Inicio.DayOfWeek)-1);

                DateTime weekInicio = Inicio.AddDays((dia) * (-2));
                DateTime weekFin = weekInicio.AddDays(6);
                DateTime monthInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime monthFin = monthInicio.AddMonths(1);
                DateTime yearInicio = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime yearFin = new DateTime(DateTime.Now.Year, 12, 31);
                
                var fa = db.VolumenesDeProduccion.Where(x => (x.Fecha >= yearInicio && x.Fecha <= yearFin)).Select(v=> v.Code_FA).ToList();
                var codes = fa.Distinct();
                var desperdicios = db.Desperdicios.Where(x => (x.Fecha >= yearInicio && x.Fecha <= yearFin)).ToList();
                var volumenes = db.VolumenesDeProduccion.Where(x => (x.Fecha >= yearInicio && x.Fecha <= yearFin)).ToList();
                var planes = db.PlanDeProduccion.Where(x => (x.Inicio >= yearInicio && x.Fin <= yearFin)).ToList();

                IList<CRRPorFA> listadelistas = new List<CRRPorFA>();
                foreach (var item in codes)
                {
                    CRRPorFA crrPorFA = new CRRPorFA();
                    crrPorFA.Code_FA = item;

                    var VolumenesDaily = volumenes
                        .Where(x => (x.Code_FA == item) && (x.Fecha >= Inicio))
                        .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new ReporteTotalView
                        {
                            Code_FA = code,
                            CRR = Math.Round((desperdicios.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum() / (cant.Sum() * 1000)), 2),
                            PlanProduccion = Math.Round((planes.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum()), 2),
                            VolumenProduccion = Math.Round(cant.Sum() * 1000, 2)
                        }).ToList();

                    var VolumenesWeekly = volumenes
                        .Where(x => (x.Code_FA == item) && (x.Fecha >= weekInicio && x.Fecha <= weekFin))
                        .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new ReporteTotalView
                        {
                            Code_FA = code,
                            CRR = Math.Round((desperdicios.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum() / (cant.Sum() * 1000)), 2),
                            PlanProduccion = Math.Round((planes.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum()), 2),
                            VolumenProduccion = Math.Round(cant.Sum() * 1000, 2)
                        }).ToList();

                    var VolumenesMonthly = volumenes
                        .Where(x => (x.Code_FA == item) && (x.Fecha >= monthInicio && x.Fecha <= monthFin ))
                        .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new ReporteTotalView
                        {
                            Code_FA = code,
                            CRR = Math.Round((desperdicios.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum() / (cant.Sum() * 1000)), 2),
                            PlanProduccion = Math.Round((planes.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum()), 2),
                            VolumenProduccion = Math.Round(cant.Sum() * 1000, 2)
                        }).ToList();

                    var VolumenesYearly = volumenes
                        .Where(x => (x.Code_FA == item) && (x.Fecha >= yearInicio && x.Fecha <= yearFin))
                        .GroupBy(rd => rd.Code_FA, rd => rd.New_Qty, (code, cant) => new ReporteTotalView
                        {
                            Code_FA = code,
                            CRR = Math.Round((desperdicios.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum() / (cant.Sum() * 1000)), 2),
                            PlanProduccion = Math.Round((planes.Where(d => d.Code_FA == code && d.Code_FA == item).Select(d => d.Cantidad).Sum()), 2),
                            VolumenProduccion = Math.Round(cant.Sum() * 1000, 2)
                        }).ToList();

                    crrPorFA.Daily = new List<ReporteTotalView>();
                    crrPorFA.Daily = VolumenesDaily;

                    crrPorFA.Weekly = new List<ReporteTotalView>();
                    crrPorFA.Weekly = VolumenesWeekly;

                    crrPorFA.Monthly = new List<ReporteTotalView>();
                    crrPorFA.Monthly = VolumenesMonthly;

                    crrPorFA.Yearly = new List<ReporteTotalView>();
                    crrPorFA.Yearly = VolumenesYearly;

                    listadelistas.Add(crrPorFA);
                }

                return Json(new { listadelistas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 400 }, JsonRequestBehavior.AllowGet);
            }
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
