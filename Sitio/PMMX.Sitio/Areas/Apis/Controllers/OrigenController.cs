using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.Entidades.Defectos;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OrigenController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private PMMXContext db = new PMMXContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(List<OrigenView>))]
        public IHttpActionResult GetOrigens()
        {
            var origens = db.Origens
                .Where(o => o.IdModulo != null)
                .Select(o => new OrigenView
                {
                    Id = o.Id,
                    IdModulo = o.IdModulo,
                    IdWorkCenter = o.IdWorkCenter,
                    NombreOrigen = o.WorkCenter.BussinesUnit.Area.NombreCorto + " " + o.WorkCenter.NombreCorto + " " + o.Modulo.NombreCorto,
                }).ToList();

            if (origens == null)
            {
                return NotFound();
            }

            return Ok(origens);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(OrigenView))]
        public IHttpActionResult GetOrigen(int id)
        {
            OrigenView origen = (from origenes in db.Origens
                                 join modulo in db.Modulos on origenes.IdModulo equals modulo.Id
                                 join workcenter in db.WorkCenters on origenes.IdWorkCenter equals workcenter.Id
                                 where origenes.Id == id
                                 select new OrigenView
                                 {
                                     Id = origenes.Id,
                                     IdModulo = origenes.IdModulo,
                                     IdWorkCenter = origenes.IdWorkCenter,
                                     Modulo = new ModuloView
                                     {
                                         Id = modulo.Id,
                                         Nombre = modulo.Nombre,
                                         NombreCorto = modulo.NombreCorto,
                                         Activo = modulo.Activo
                                     },
                                     WorkCenter = new WorkCenterView
                                     {
                                         Id = workcenter.Id,
                                         Nombre = workcenter.Nombre,
                                         Activo = workcenter.Activo,
                                         NombreCorto = workcenter.NombreCorto
                                     }

                                 }).FirstOrDefault();
            if (origen == null)
            {
                return NotFound();
            }

            return Ok(origen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idWorkCenter"></param>
        /// <returns></returns>
        [ResponseType(typeof(List<OrigenView>))]
        public IHttpActionResult getModulosByWorkCenter(int idWorkCenter)
        {
            var modulos = db.Origens.Where(x => x.IdWorkCenter == idWorkCenter && x.IdModulo != null)
                .Select(x => new OrigenView
                {
                    Id = x.Id,
                    IdModulo = x.IdModulo,
                    IdWorkCenter = x.IdWorkCenter,
                    ParosActivos = x.Paros.Where(p => p.Activo == true).Count(),
                    DefectosActivos = x.Defectos.Where(p => p.Activo == true).Count(),
                    Modulo = new ModuloView
                    {
                        Id = x.Modulo.Id,
                        Nombre = x.Modulo.Nombre,
                        NombreCorto = x.Modulo.NombreCorto,
                        Activo = x.Modulo.Activo
                    }

                }).ToList();






            //var modulos = (from origen in db.Origens
            //               join modulo in db.Modulos on origen.IdModulo equals modulo.Id
            //               where origen.IdWorkCenter == idWorkCenter 
            //               select new OrigenView {
            //                   Id = origen.Id,
            //                   IdModulo = origen.IdModulo,
            //                   Modulo = new ModuloView {
            //                       Id = origen.Modulo.Id,
            //                        Activo = origen.Modulo.Activo,
            //                       Nombre = origen.Modulo.Nombre,
            //                        NombreCorto = origen.Modulo.NombreCorto

            //                   },
            //               }).ToList();


            if (modulos == null)
            {
                return NotFound();
            }

            return Ok(modulos);
            //Origen origen = db.Origens.Find(id);
            //if (origen == null)
            //{
            //    return NotFound();
            //}

            //return Ok(origen);
        }

        [ResponseType(typeof(List<OrigenView>))]
        public IHttpActionResult getModulosByWorkCenterAndModuloNull(int idLinkup)
        {
            var modulo = db.Origens.Where(x => x.IdWorkCenter == idLinkup && x.IdModulo == null)
                .Select(x => new OrigenView
                {
                    Id = x.Id,
                    IdModulo = x.IdModulo,
                    IdWorkCenter = x.IdWorkCenter,
                    ParosActivos = x.Paros.Where(p => p.Activo == true).Count(),
                    DefectosActivos = x.Defectos.Where(p => p.Activo == true).Count(),
                }).FirstOrDefault();
            
            return Ok(modulo);
        }

        [ResponseType(typeof(List<OrigenView>))]
        public IHttpActionResult getIdOrigenbyNameModulo(string nameModulo)
        {
            var modulo = db.Origens.Where(x => (x.Modulo.Nombre.Equals(nameModulo)))
                .Select(x => new OrigenView
                {
                    Id = x.Id,
                    IdModulo = x.IdModulo,
                    IdWorkCenter = x.IdWorkCenter,
                    ParosActivos = x.Paros.Where(p => p.Activo == true).Count(),
                    DefectosActivos = x.Defectos.Where(p => p.Activo == true).Count(),
                }).FirstOrDefault();

            return Ok(modulo);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="origen"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrigen(int id, Origen origen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != origen.Id)
            {
                return BadRequest();
            }

            db.Entry(origen).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrigenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origen"></param>
        /// <returns></returns>
        [ResponseType(typeof(Origen))]
        public IHttpActionResult PostOrigen(Origen origen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Origens.Add(origen);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = origen.Id }, origen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Origen))]
        public IHttpActionResult DeleteOrigen(int id)
        {
            Origen origen = db.Origens.Find(id);
            if (origen == null)
            {
                return NotFound();
            }

            db.Origens.Remove(origen);
            db.SaveChanges();

            return Ok(origen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool OrigenExists(int id)
        {
            return db.Origens.Count(e => e.Id == id) > 0;
        }
    }
}