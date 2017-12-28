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
using PMMX.Operaciones.Servicios;

namespace Sitio.Areas.Apis.Controllers.Operarios
{
    public class PesadoresController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Pesadores
        public IQueryable<Pesador> GetPesadores()
        {

            return db.Pesadores;
        }

        // GET: api/Pesadores/5
        [ResponseType(typeof(Pesador))]
        public IHttpActionResult GetPesador(int id)
        {
            PesadorServicio servicio = new PesadorServicio();
            var workcenters = servicio.GetWorkCentersByPesador(id);
            return Ok(workcenters);
        }

        // PUT: api/Pesadores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPesador(int id, Pesador pesador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pesador.Id)
            {
                return BadRequest();
            }

            db.Entry(pesador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PesadorExists(id))
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

        // POST: api/Pesadores
        [ResponseType(typeof(Pesador))]
        public IHttpActionResult PostPesador(Pesador pesador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pesadores.Add(pesador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pesador.Id }, pesador);
        }

        // DELETE: api/Pesadores/5
        [ResponseType(typeof(Pesador))]
        public IHttpActionResult DeletePesador(int id)
        {
            Pesador pesador = db.Pesadores.Find(id);
            if (pesador == null)
            {
                return NotFound();
            }

            db.Pesadores.Remove(pesador);
            db.SaveChanges();

            return Ok(pesador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PesadorExists(int id)
        {
            return db.Pesadores.Count(e => e.Id == id) > 0;
        }
    }
}