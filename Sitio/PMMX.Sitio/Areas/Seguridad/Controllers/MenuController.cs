﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Seguridad;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using PMMX.Seguridad.Servicios;
using Sitio.Helpers;

namespace Sitio.Areas.Seguridad.Controllers
{
    public class MenuController : Controller
    {
        private PMMXContext db = new PMMXContext();

        // GET: Seguridad/Menu
        public ActionResult Index()
        {
            return View(db.Menu.ToList());
        }

        // GET: Seguridad/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Seguridad/Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seguridad/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Seguridad/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Seguridad/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        //[RenderAjaxPartialScripts]
        [HttpPost]
        public ActionResult getMenuByPersona()
        {
            List<MenuView> list = new List<MenuView>();
            PersonaServicio personaServicio = new PersonaServicio();
            IRespuestaServicio<Persona> persona = personaServicio.GetPersona(User.Identity.GetUserId());

            if (persona.EjecucionCorrecta)
            {
                list = db.Personas
                    .Where(p => p.Id == persona.Respuesta.Id)
                    .Select(p => p.Menu
                    .OrderBy(o=> o.Nombre)
                    .ThenBy(o => o.SubMenu)
                    .ThenBy(o => o.Programa)
                    .Where(a => a.Activo == true)
                    .Select(v => new MenuView
                    {
                        Nombre = v.Nombre,
                        SubMenu = v.SubMenu,
                        Programa = v.Programa,
                        Ruta = v.Ruta
                    }).ToList()
                    ).FirstOrDefault();
            }
            //return Json(new { list }, JsonRequestBehavior.AllowGet);
            return Json(new { list });
        }

        // GET: Seguridad/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Seguridad/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            db.Menu.Remove(menu);
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
