using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Maquinaria;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios
{
    public class PesadorServicio
    {
        private PMMXContext _context;

        public PesadorServicio(PMMXContext context)
        {
            _context = context;
        }

        public PesadorServicio()
        {
            _context = new PMMXContext();
        }


        #region Gets
        
        public RespuestaServicio<List<WorkCenter>> GetWorkCentersByPesador(int idPesador)
        {
            RespuestaServicio<List<WorkCenter>> respuesta = new RespuestaServicio<List<WorkCenter>>();

            var workCenter = _context.Pesadores.SelectMany(p => p.Area.BussinessUnits.SelectMany(b => b.WorkCenters)).ToList();
            if (workCenter.Count <= 0)
            {

            }
            else
            {
                respuesta.Respuesta = workCenter;
            }

            return respuesta;
        }

        #endregion

    }
}
