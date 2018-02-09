using System;
using System.Data;
using System.Linq;
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

        public RespuestaServicio<Marca> GetMarca(string code_FA)
        {
            RespuestaServicio<Marca> respuesta = new RespuestaServicio<Marca>();
            try
            {
                var marca = _context.Marcas.Where(m => m.Code_FA == code_FA).FirstOrDefault();
                respuesta.Respuesta = marca;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return respuesta;
        }

        
        

    }
}
