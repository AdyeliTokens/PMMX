using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Linq;
using System.Data.Entity;

namespace PMMX.Operaciones.Servicios
{
    public class NoConformidadServicio
    {
        private PMMXContext _context;

        public NoConformidadServicio(PMMXContext context)
        {
            _context = context;
        }

        public NoConformidadServicio()
        {
            _context = new PMMXContext();
        }


        #region Gets

        public RespuestaServicio<IQueryable<NoConformidad>> GetNoConformidades()
        {
            RespuestaServicio<IQueryable<NoConformidad>> respuesta = new RespuestaServicio<IQueryable<NoConformidad>>();
            try
            {
                var noconformidades = _context.NoConformidades.Include(n => n.Persona).Include(n => n.Seccion).Include(n => n.WorkCenter);
                respuesta.Respuesta = noconformidades;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        public RespuestaServicio<NoConformidad> GetNoConformidad(int id)
        {
            RespuestaServicio<NoConformidad> respuesta = new RespuestaServicio<NoConformidad>();
            try
            {
                var noconformidad = _context.NoConformidades.Include(n => n.Persona).Include(n => n.Seccion).Include(n => n.WorkCenter).Where(n => n.Id == id).FirstOrDefault(); ;
                respuesta.Respuesta = noconformidad;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        #endregion
    }
}
