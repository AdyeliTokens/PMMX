using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Paros;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PMMX.Operaciones.Servicios
{
    public class ParoServicio
    {
        private PMMXContext _context;

        public ParoServicio(PMMXContext context)
        {
            _context = context;
        }

        public ParoServicio()
        {
            _context = new PMMXContext();
        }

        #region Gets

        public RespuestaServicio<IQueryable<Paro>> GetParos()
        {
            RespuestaServicio<IQueryable<Paro>> respuesta = new RespuestaServicio<IQueryable<Paro>>();
            respuesta.Respuesta = _context.Paros;
            return respuesta;
        }

        public RespuestaServicio<Paro> GetParo(int id)
        {
            RespuestaServicio<Paro> respuesta = new RespuestaServicio<Paro>();
            try
            {
                var paro = _context.Paros
                .Include(p => p.Reportador)
                .Include(p => p.Reportador.Puesto)
                .Include(p => p.TiemposDeParo)
                .Include(p => p.ActividadesEnParo)
                .Include(p => p.Origen)
                .Include(p => p.Origen.Modulo)
                .Include(p => p.Origen.WorkCenter)
                .Include(p => p.Origen.WorkCenter.BussinesUnit)
                .Where(p => p.Id == id)
                .FirstOrDefault();

                respuesta.Respuesta = paro;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }


        public RespuestaServicio<List<Paro>> GetParosByOrigen(int idOrigen)
        {
            RespuestaServicio<List<Paro>> respuesta = new RespuestaServicio<List<Paro>>();
            try
            {
                var paros = _context.Paros
                .Include(p => p.Reportador)
                .Include(p => p.Reportador.Puesto)
                .Include(p => p.TiemposDeParo)
                .Include(p => p.Origen)
                .Include(p => p.Origen.Modulo)
                .Include(p => p.Origen.WorkCenter)
                .Include(p => p.Origen.WorkCenter.BussinesUnit)
                .Where(p => p.IdOrigen == idOrigen)
                .ToList();

                respuesta.Respuesta = paros;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }
        
        public RespuestaServicio<List<Paro>> getParosByWorkCenter(int idWorkCenter)
        {
            RespuestaServicio<List<Paro>> respuesta = new RespuestaServicio<List<Paro>>();
            try
            {
                var paros = _context.Paros
                .Include(p => p.Reportador)
                .Include(p => p.Reportador.Puesto)
                .Include(p => p.TiemposDeParo)
                .Include(p => p.Origen)
                .Include(p => p.Origen.Modulo)
                .Include(p => p.Origen.WorkCenter)
                .Include(p => p.Origen.WorkCenter.BussinesUnit)
                .Where(p => p.Origen.IdWorkCenter == idWorkCenter)
                .ToList();

                respuesta.Respuesta = paros;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }

        public RespuestaServicio<List<Paro>> GetParosByBusinessUnit(int idBusinessUnit)
        {
            RespuestaServicio<List<Paro>> respuesta = new RespuestaServicio<List<Paro>>();
            try
            {
                var paros = _context.Paros
                .Include(p => p.Reportador)
                .Include(p => p.Reportador.Puesto)
                .Include(p => p.TiemposDeParo)
                .Include(p => p.Origen)
                .Include(p => p.Origen.Modulo)
                .Include(p => p.Origen.WorkCenter)
                .Include(p => p.Origen.WorkCenter.BussinesUnit)
                .Where(p => p.Origen.WorkCenter.IdBussinesUnit == idBusinessUnit)
                .ToList();

                respuesta.Respuesta = paros;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }
        
        #endregion

        #region Puts

        public RespuestaServicio<Paro> PutParo(int id, Paro paro)
        {
            RespuestaServicio<Paro> respuesta = new RespuestaServicio<Paro>();
            _context.Entry(paro).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                return GetParo(id);
            }
            catch (DbUpdateConcurrencyException e)
            {
                respuesta.Mensaje = e.Message;
                return respuesta;
            }

        }

        #endregion

        #region Posts

        public RespuestaServicio<Paro> PostParo(Paro paro)
        {
            RespuestaServicio<Paro> respuesta = new RespuestaServicio<Paro>();
            if (paro == null)
            {
                respuesta.Mensaje = "Paro no puede ser nulo";
                return respuesta;
            }


            try
            {
                _context.Paros.Add(paro);
                _context.SaveChanges();

                return GetParo(paro.Id);
            }
            catch (Exception e)
            {

                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }

        #endregion

        #region Deletes

        public RespuestaServicio<String> DeleteParo(int id)
        {
            RespuestaServicio<String> respuesta = new RespuestaServicio<String>();
            var paro = _context.Paros
                .Include(p => p.TiemposDeParo)
                .Include(p => p.ActividadesEnParo)
                .Where(p => p.Id == id)
                .FirstOrDefault();



            if (paro != null)
            {
                try
                {
                    _context.Paros.Remove(paro);
                    _context.SaveChanges();
                    respuesta.Respuesta = "Correcto";

                }
                catch (Exception e)
                {
                    respuesta.Mensaje = e.Message;
                }

            }
            else {
                respuesta.Respuesta = "Nada que eliminar";
            }
            return respuesta;
        }

        #endregion

        #region Helpers

        public void Dispose()
        {
            _context.Dispose();
        }

        private bool WorkCenterExists(int id)
        {
            return _context.WorkCenters.Count(e => e.Id == id) > 0;
        }

        #endregion
    }
}
