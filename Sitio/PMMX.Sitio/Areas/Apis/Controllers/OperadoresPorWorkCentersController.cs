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
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Vistas;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Operaciones.Servicios;

namespace Sitio.Areas.Apis.Controllers
{
    public class OperadoresPorWorkCentersController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        
        public IQueryable<Operadores> GetOperadores()
        {
            return db.Operadores;
        }

        
        [ResponseType(typeof(Operadores))]
        public IHttpActionResult GetOperadoresPorWorkCenter(int id)
        {
            Operadores operadoresPorWorkCenter = db.Operadores.Find(id);
            if (operadoresPorWorkCenter == null)
            {
                return NotFound();
            }

            return Ok(operadoresPorWorkCenter);
        }

        
        [ResponseType(typeof(Operadores))]
        public IHttpActionResult GetWorkCenterByOperador(int idOperador)
        {
            WorkCenterServicio servicio = new WorkCenterServicio(db);
            var respuesta = servicio.GetWorkCenterByOperador(idOperador);
            return Ok(respuesta);
        }


        // PUT: api/OperadoresPorWorkCenters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOperadoresPorWorkCenter(int id, Operadores operadoresPorWorkCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operadoresPorWorkCenter.Id)
            {
                return BadRequest();
            }

            db.Entry(operadoresPorWorkCenter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperadoresPorWorkCenterExists(id))
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

        // POST: api/OperadoresPorWorkCenters
        [ResponseType(typeof(Operadores))]
        public IHttpActionResult PostOperadoresPorWorkCenter(Operadores operadoresPorWorkCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Operadores.Add(operadoresPorWorkCenter);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = operadoresPorWorkCenter.Id }, operadoresPorWorkCenter);
        }

        // DELETE: api/OperadoresPorWorkCenters/5
        [ResponseType(typeof(Operadores))]
        public IHttpActionResult DeleteOperadoresPorWorkCenter(int id)
        {
            Operadores operadoresPorWorkCenter = db.Operadores.Find(id);
            if (operadoresPorWorkCenter == null)
            {
                return NotFound();
            }

            db.Operadores.Remove(operadoresPorWorkCenter);
            db.SaveChanges();

            return Ok(operadoresPorWorkCenter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OperadoresPorWorkCenterExists(int id)
        {
            return db.Operadores.Count(e => e.Id == id) > 0;
        }
    }
}