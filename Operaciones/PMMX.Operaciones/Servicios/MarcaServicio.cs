using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Entidades;

namespace PMMX.Operaciones.Servicios
{
    public class MarcaServicio
    {
        private PMMXContext _context;

        public MarcaServicio()
        {
            _context = new PMMXContext();
        }

        public MarcaServicio(PMMXContext db)
        {
            _context = db;
        }

        public RespuestaServicio<IQueryable<Marca>> GetMarcas()
        {
            RespuestaServicio<IQueryable<Marca>> respuesta = new RespuestaServicio<IQueryable<Marca>>();
            try
            {
                var marcas = _context.Marcas;
                respuesta.Respuesta = marcas;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }
    }
}
