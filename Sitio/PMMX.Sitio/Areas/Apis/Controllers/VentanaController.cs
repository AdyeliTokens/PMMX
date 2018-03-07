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
using PMMX.Modelo.Entidades.Warehouse;
using PMMX.Modelo.Vistas;

namespace Sitio.Areas.Apis.Controllers
{
    public class VentanaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Ventana
        public IQueryable<Ventana> GetVentana()
        {
            return db.Ventana;
        }

        // GET: api/Ventana/5
        [ResponseType(typeof(Ventana))]
        public IHttpActionResult GetVentana(int id)
        {
            Ventana ventana = db.Ventana.Find(id);
            if (ventana == null)
            {
                return NotFound();
            }

            return Ok(ventana);
        }

        // GET: api/Ventana/5
        [ResponseType(typeof(Ventana))]
        public IHttpActionResult GetVentanabyEvento(int idEvento)
        {
            var ventana = db.Ventana
                          .Where(ve => (ve.IdEvento == idEvento))
                          .Select(v => new VentanaView
                          {
                              Id = v.Id,
                              PO = v.PO,
                              SubCategoriaNombre = db.SubCategoria.Where(s => s.Id == v.IdSubCategoria).Select(s => s.Nombre).FirstOrDefault(),
                              Recurso = v.Recurso,
                              Cantidad = v.Cantidad,
                              CarrierNombre = db.Carrier.Where(c => c.Id == v.IdCarrier).Select(c => c.Nombre).FirstOrDefault(),
                              NombreCarrier = v.NombreCarrier,
                              ProcedenciaNombre = db.Locacion.Where(l => l.Id == v.IdProcedencia).Select(l => l.Nombre).FirstOrDefault(),
                              DestinoNombre = db.Locacion.Where(l => l.Id == v.IdDestino).Select(l => l.Nombre).FirstOrDefault(),
                              ProveedorNombre = db.Proveedores.Where(p => p.Id == v.IdProveedor).Select(l => l.Nombre).FirstOrDefault(),
                              // Vehículo
                              NumeroEconomico = v.NumeroEconomico,
                              NumeroPlaca = v.NumeroPlaca,
                              EconomicoRemolque = v.EconomicoRemolque,
                              PlacaRemolque = v.PlacaRemolque,
                              ModeloContenedor = v.ModeloContenedor,
                              ColorContenedor = v.ColorContenedor,
                              Sellos = v.Sellos,
                              TipoUnidad = v.TipoUnidad,
                              Dimension = v.Dimension,
                              Temperatura = v.Temperatura,
                              // Conductor
                              Conductor = v.Conductor,
                              MovilConductor = v.MovilConductor,
                              Activo = v.Activo,
                              StatusNombre = db.StatusVentana.Where(s => s.IdVentana == v.Id).Select(s => s.Status.Nombre).FirstOrDefault()
                          })
                          .FirstOrDefault();

            if (ventana == null)
            {
                return NotFound();
            }

            return Ok(ventana);
        }

        // PUT: api/Ventana/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVentana(int id, Ventana ventana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ventana.Id)
            {
                return BadRequest();
            }

            db.Entry(ventana).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentanaExists(id))
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

        // POST: api/Ventana
        [ResponseType(typeof(Ventana))]
        public IHttpActionResult PostVentana(Ventana ventana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ventana.Add(ventana);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ventana.Id }, ventana);
        }

        // DELETE: api/Ventana/5
        [ResponseType(typeof(Ventana))]
        public IHttpActionResult DeleteVentana(int id)
        {
            Ventana ventana = db.Ventana.Find(id);
            if (ventana == null)
            {
                return NotFound();
            }

            db.Ventana.Remove(ventana);
            db.SaveChanges();

            return Ok(ventana);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VentanaExists(int id)
        {
            return db.Ventana.Count(e => e.Id == id) > 0;
        }
    }
}