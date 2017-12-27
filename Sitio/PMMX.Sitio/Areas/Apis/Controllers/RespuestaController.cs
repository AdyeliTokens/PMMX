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
using PMMX.Modelo.Entidades;
using PMMX.Modelo;
using PMMX.Modelo.Vistas;
using System.Globalization;
using Maya.Helpers;
using PMMX.Seguridad.Servicios;

namespace Sitio.Areas.Apis.Controllers
{
    public class RespuestaController : ApiController
    {
        private PMMXContext db = new PMMXContext();

        // GET: api/Respuesta
        public IQueryable<Respuesta> GetRespuestas()
        {
            return db.Respuestas;
        }

        // GET: api/Respuesta/5
        [ResponseType(typeof(Respuesta))]
        public IHttpActionResult GetRespuesta(int id)
        {
            Respuesta Respuesta = db.Respuestas.Find(id);
            if (Respuesta == null)
            {
                return NotFound();
            }

            return Ok(Respuesta);
        }
        
        [ResponseType(typeof(IList<RespuestaView>))]
        public IHttpActionResult getRespuestaByMWCRHC(int idOrigen,int idMWCR, int idDDS, int idHC)
        {
            bool dds = (idDDS == 1) ? true : false;

           var respuestas = db.Respuestas
                .Where(x => (x.IdOrigenRespuesta == idMWCR) )
                .Select(x => new RespuestaView
                {
                    Id = x.Id,
                    IdPregunta = x.IdPregunta,
                    IdOrigenRespuesta = x.IdOrigenRespuesta,
                    Solucion = x.Solucion,
                    TotalSolucion = db.Respuestas.Where(p => (p.IdOrigenRespuesta == idMWCR)).Count(),
                    TotalSi = db.Respuestas.Where(p => (p.Solucion == true) && (p.IdOrigenRespuesta == idMWCR)).Count(),
                    TotalNo = db.Respuestas.Where(p => (p.Solucion == false) && (p.IdOrigenRespuesta == idMWCR)).Count(),
                    PorcentajeSi = db.Respuestas.Where(p => (p.Solucion == true) && (p.IdOrigenRespuesta == idMWCR)).Count() * 100 / db.Respuestas.Where(p => (p.IdOrigenRespuesta == idMWCR)).Count(),
                    PorcentajeNo = db.Respuestas.Where(p => (p.Solucion == false) && (p.IdOrigenRespuesta == idMWCR)).Count() * 100 / db.Respuestas.Where(p => (p.IdOrigenRespuesta == idMWCR)).Count(),
                    Fecha = x.Fecha,
                    Comentario = x.Comentario,
                    DescripcionPregunta = db.Preguntas.Where(p => (p.Id == x.IdPregunta)).Select(p => (p.Interrogante)).FirstOrDefault(),
                    RespuestaBy = db.Personas.Where(p => (p.Id == x.OrigenRespuesta.IdEntrevistado)).Select(p => (p.Nombre + " " + p.Apellido1 + " " + p.Apellido2)).FirstOrDefault()

                }).ToList();
            
            NotificationService notify = new NotificationService();
            UsuarioServicio usuarioServicio = new UsuarioServicio();
            List<DispositivoView> dispositivos = usuarioServicio.GetDispositivosByOrigenAndGrupoPreguntas(idOrigen,idHC);
            List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

            foreach (string notificacion in llaves)
            {
                notify.SendPushNotification(notificacion, "Se ha realizado Health Check", "Health Check realizado ");
            }

            if (respuestas == null)
            {
                return NotFound();
            }

            return Ok(respuestas);
        }

        [ResponseType(typeof(IList<RespuestaView>))]
        public IHttpActionResult getRespuestaByWorkCenter(int idWorkCenter, int Init, int Finish)
        {
            var today = DateTime.Now.Date;
            DateTime dateInit = today.AddDays(-Init);
            DateTime dateFinish = today.AddDays(Finish);

            List<RespuestaView> lista = new List<RespuestaView>();
            var grupoPreguntas = db.GrupoPreguntas.Where(x => (x.DDS.Equals(false))).Select(s=> s.Id).ToList();

            foreach (var idHC in grupoPreguntas)
            { 
                var respuesta = db.Respuestas
                 .Where(x => (x.OrigenRespuesta.Origen.IdWorkCenter == idWorkCenter) && (x.Pregunta.GrupoPreguntas.Id == idHC))
                 .Select(x => new RespuestaView
                 {
                     TotalSolucion = db.Respuestas.Where(p => (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= dateInit && p.Fecha <= dateFinish)).Count(),
                     TotalSi = db.Respuestas.Where(p => (p.Solucion == true) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= dateInit && p.Fecha <= dateFinish)).Count(),
                     TotalNo = db.Respuestas.Where(p => (p.Solucion == false) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= dateInit && p.Fecha <= dateFinish)).Count(),
                     PorcentajeSi = db.Respuestas.Where(p => (p.Solucion == true) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= dateInit && p.Fecha <= dateFinish)).Count() * 100 
                     / db.Respuestas.Where(p => (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= dateInit && p.Fecha <= dateFinish)).Count(),
                     PorcentajeNo = db.Respuestas.Where(p => (p.Solucion == false) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= dateInit && p.Fecha <= dateFinish)).Count() * 100 
                     / db.Respuestas.Where(p => (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= dateInit && p.Fecha <= dateFinish)).Count(),
                 }).FirstOrDefault();
                
                lista.Add(respuesta);
            }

            if (lista == null)
            {
                return NotFound();
            }

            return Ok(lista);
        }
        
        [ResponseType(typeof(IList<Semana>))]
        public IHttpActionResult getRespuestaByWeek(int idWorkCenter, int idHC, int Init, int Finish)
        {
            var today = DateTime.Now.Date;
            DateTime dateInit = today.AddDays(-Init);
            DateTime dateFinish = today.AddDays(Finish);
            
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            int primerSemana = cal.GetWeekOfYear(dateInit, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int ultimaSemana = cal.GetWeekOfYear(dateFinish, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            int totalSemanas = ultimaSemana - primerSemana;

            List<Semana> semanas = new List<Semana>();
            DateTime sigSemana = dateInit;

            for (int i = 0; i<=totalSemanas; i++)
            {
                int semana = 0;
                
                if (i == 0)
                {
                    semana = cal.GetWeekOfYear(sigSemana, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                }
                else
                {
                    sigSemana = dateInit.AddDays(7);
                    semana = cal.GetWeekOfYear(sigSemana, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                }
                
                DateTime firstDayOfWeek = FirstDateOfWeek(sigSemana.Year, semana, CultureInfo.CurrentCulture);
                DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                var respuestabySemana = db.Respuestas
                .Where(x => (x.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (x.Pregunta.GrupoPreguntas.Id == idHC) && (x.Fecha >= firstDayOfWeek && x.Fecha <= lastDayOfWeek))
                .Select(y => new RespuestaView
                {
                    Fecha = y.Fecha,
                    TotalSolucion = db.Respuestas.Where(p => (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= firstDayOfWeek && p.Fecha <= lastDayOfWeek)).Count(),
                    TotalSi = db.Respuestas.Where(p => (p.Solucion == true) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= firstDayOfWeek && p.Fecha <= lastDayOfWeek)).Count(),
                    TotalNo = db.Respuestas.Where(p => (p.Solucion == false) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= firstDayOfWeek && p.Fecha <= lastDayOfWeek)).Count(),
                    PorcentajeSi = db.Respuestas.Where(p => (p.Solucion == true) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= firstDayOfWeek && p.Fecha <= lastDayOfWeek)).Count() * 100 / db.Respuestas.Where(p => (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= firstDayOfWeek && p.Fecha <= lastDayOfWeek)).Count(),
                    PorcentajeNo = db.Respuestas.Where(p => (p.Solucion == false) && (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= firstDayOfWeek && p.Fecha <= lastDayOfWeek)).Count() * 100 / db.Respuestas.Where(p => (p.OrigenRespuesta.Origen.WorkCenter.Id == idWorkCenter) && (p.Pregunta.GrupoPreguntas.Id == idHC) && (p.Fecha >= firstDayOfWeek && p.Fecha <= lastDayOfWeek)).Count(),
                })
                .ToList();

                foreach (RespuestaView respuesta in respuestabySemana)
                {
                    Semana sem = semanas.Where(s => s.numero == semana).FirstOrDefault();
                    if (sem != null)
                    {
                        sem.Respuestas.Add(respuesta);
                    }
                    else
                    {
                        Semana semananueva = new Semana();
                        semananueva.numero = semana;
                        semananueva.Respuestas = new List<RespuestaView>();
                        semananueva.Respuestas.Add(respuesta);
                        semanas.Add(semananueva);
                    }
                }

            }
            
            if (semanas == null)
            {
                return NotFound();
            }

            return Ok(semanas);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }
        
        // PUT: api/Respuesta/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRespuesta(int id, Respuesta Respuesta)
        {
            Respuesta.Fecha = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Respuesta.Id)
            {
                return BadRequest();
            }

            db.Entry(Respuesta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespuestaExists(id))
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

        // POST: api/Respuesta
        [HttpPost]
        [ResponseType(typeof(Respuesta))]
        public IHttpActionResult PostRespuesta(Respuesta Respuesta)
        {
            Respuesta.Fecha = DateTime.Now;
            Respuesta.Activo = true;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Respuestas.Add(Respuesta);
            db.SaveChanges();
            
            var RespuestaAdded= db.Respuestas.Where(x => x.Id == Respuesta.Id).Select(x => new RespuestaView
            {
                Id = x.Id,
                IdPregunta = x.IdPregunta,
                Fecha = x.Fecha,
                Comentario = x.Comentario                
            }).FirstOrDefault();

            return Ok(RespuestaAdded);
            // return CreatedAtRoute("DefaultApi", new { id = Respuesta.Id }, Respuesta);
        }

    
        // DELETE: api/Respuesta/5
        [ResponseType(typeof(Respuesta))]
        public IHttpActionResult DeleteRespuesta(int id)
        {
            Respuesta Respuesta = db.Respuestas.Find(id);
            if (Respuesta == null)
            {
                return NotFound();
            }

            db.Respuestas.Remove(Respuesta);
            db.SaveChanges();

            return Ok(Respuesta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RespuestaExists(int id)
        {
            return db.Respuestas.Count(e => e.Id == id) > 0;
        }
    }
}