using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios
{
    public class DesperdicioServicio
    {
        private PMMXContext _context;

        public DesperdicioServicio(PMMXContext context)
        {
            _context = context;
        }

        public DesperdicioServicio()
        {
            _context = new PMMXContext();
        }

        #region Gets

        public RespuestaServicio<List<CRRView>> GetCRRPorWorkCenterPorSemanaActual(int IdWorkCenter )
        {
            RespuestaServicio<List<CRRView>> respuesta = new RespuestaServicio<List<CRRView>>();

            DateTime diaSeleccionado = DateTime.Now.Date;
            diaSeleccionado = diaSeleccionado.AddDays(1);
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);

            var desperdicios = _context.Desperdicios
                .Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado))
                .Select(y => new DesperdicioView
                {
                    Id = y.Id,
                    Cantidad = y.Cantidad,
                    Fecha = y.Fecha,
                    Code_FA = y.Code_FA,
                    IdPersona = y.IdPersona,
                    IdSeccion = y.IdSeccion,
                    IdWorkCenter = y.IdWorkCenter,
                    MarcaDelCigarrillo = y.MarcaDelCigarrillo
                })
                .ToList();

            var volumenes = _context.VolumenesDeProduccion
                .Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado))
                .Select(x => x)
                .ToList();

            var marcas = desperdicios.Select(x => x.MarcaDelCigarrillo).Distinct();

            var objetivos = _context.ObjetivosCRR.Select(x => x).Where(x => (x.IdWorkCenter == IdWorkCenter) && (x.FechaInicial >= primerDiaDelAnio)).ToList();

            List<CRRView> crr = new List<CRRView>();
            for (int i = delta; i <= (6 + delta); i++)
            {
                Double crrTotal = 0;
                foreach (var item in marcas)
                {
                    Double crrTotalPorMarca = desperdicios.Where(x => (x.Fecha.Date == diaSeleccionado.AddDays(i).Date) && (x.Code_FA == item.Code_FA)).Sum(o => o.Cantidad);
                    Double volumen = volumenes.Where(v => (v.Code_FA == item.Code_FA) && (v.Fecha.Date == diaSeleccionado.AddDays(i).Date)).Sum(o => o.New_Qty);
                    if (volumen > 0)
                    {
                        crrTotal = crrTotal + (crrTotalPorMarca / volumen);
                    }
                    else
                    {
                        crrTotal = crrTotal + 0;
                    }

                }
                //Double crrTotal = desperdicios.Where(x => x.Fecha.Date == diaSeleccionado.AddDays(i).Date).Sum(o => o.Cantidad);
                Double objetivo = objetivos.Where(x => x.FechaInicial <= diaSeleccionado.AddDays(i).Date).OrderByDescending(x => x.FechaInicial).Select(x => x.Objetivo).FirstOrDefault();

                crr.Add(new CRRView { Fecha = diaSeleccionado.AddDays(i), CRR_Total = crrTotal, Objetivo = objetivo });
            }

            respuesta.Respuesta = crr;

            return respuesta;
        }

        public RespuestaServicio<List<CRRView>> GetCRRPorWorkCenterPorSemana(DateTime fecha, int idWorkCenter)
        {
            RespuestaServicio<List<CRRView>> respuesta = new RespuestaServicio<List<CRRView>>();

            DateTime diaSeleccionado = fecha.Date;
            int delta = DayOfWeek.Monday - diaSeleccionado.DayOfWeek;
            DateTime monday = diaSeleccionado.AddDays(delta);
            DateTime sunday = monday.AddDays(6);
            var primerDiaDelAnio = new DateTime(DateTime.Now.Year, 1, 1);


            var desperdicios = _context.Desperdicios
                .Where(x => (x.IdWorkCenter == idWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado))
                .Select(y => new DesperdicioView
                {
                    Id = y.Id,
                    Cantidad = y.Cantidad,
                    Fecha = y.Fecha,
                    Code_FA = y.Code_FA,
                    IdPersona = y.IdPersona,
                    IdSeccion = y.IdSeccion,
                    IdWorkCenter = y.IdWorkCenter,
                    MarcaDelCigarrillo = y.MarcaDelCigarrillo
                })
                .ToList();

            var volumenes = _context.VolumenesDeProduccion
                .Where(x => (x.IdWorkCenter == idWorkCenter) && (x.Fecha >= monday && x.Fecha <= diaSeleccionado))
                .Select(x => x)
                .ToList();

            var marcas = desperdicios.Select(x => x.MarcaDelCigarrillo).Distinct();

            var objetivos = _context.ObjetivosCRR.Select(x => x).Where(x => (x.IdWorkCenter == idWorkCenter) && (x.FechaInicial >= primerDiaDelAnio)).ToList();

            List<CRRView> crr = new List<CRRView>();
            for (int i = delta; i <= (6 + delta); i++)
            {
                Double crrTotal = 0;
                foreach (var item in marcas)
                {
                    Double crrTotalPorMarca = desperdicios.Where(x => (x.Fecha.Date == diaSeleccionado.AddDays(i).Date) && (x.Code_FA == item.Code_FA)).Sum(o => o.Cantidad);
                    Double volumen = volumenes.Where(v => (v.Code_FA == item.Code_FA) && (v.Fecha.Date == diaSeleccionado.AddDays(i).Date)).Sum(o => o.New_Qty);
                    if (volumen > 0)
                    {
                        crrTotal = crrTotal + (crrTotalPorMarca / volumen);
                    }
                    else
                    {
                        crrTotal = crrTotal + 0;
                    }

                }
                //Double crrTotal = desperdicios.Where(x => x.Fecha.Date == diaSeleccionado.AddDays(i).Date).Sum(o => o.Cantidad);
                Double objetivo = objetivos.Where(x => x.FechaInicial <= diaSeleccionado.AddDays(i).Date).OrderByDescending(x => x.FechaInicial).Select(x => x.Objetivo).FirstOrDefault();

                crr.Add(new CRRView { Fecha = diaSeleccionado.AddDays(i), CRR_Total = crrTotal, Objetivo = objetivo });
            }

            respuesta.Respuesta = crr;

            return respuesta;
        }

        #endregion
    }
}
