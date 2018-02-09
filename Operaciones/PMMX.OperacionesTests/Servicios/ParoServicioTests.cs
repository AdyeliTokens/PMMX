using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades.Paros;
using PMMX.Operaciones.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios.Tests
{
    [TestClass()]
    public class ParoServicioTests
    {
        private readonly ParoServicio _servicio;
        private readonly PMMXContext _context;


        public ParoServicioTests()
        {

            _context = new PMMXContext();
            _servicio = new ParoServicio(_context);

        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void GetParoTest(int id)
        {
            var respuesta = _servicio.GetParo(id);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }


        [TestMethod()]
        public void PostParoTest()
        {
            Paro paro = new Paro();
            paro.FechaReporte = DateTime.Now;
            paro.IdReportador = 66;
            paro.IdOrigen = 1;
            paro.TiemposDeParo = new List<TiempoDeParo> { new TiempoDeParo() { Inicio = DateTime.Now } };
            paro.ActividadesEnParo = new List<ActividadEnParo> { new ActividadEnParo() { Fecha = DateTime.Now, IdPersona = paro.IdReportador, Descripcion = "Ejemplo Cargado Con UnitTest", } };

            var respuesta = _servicio.PostParo(paro);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }



        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void GetParosByOrigenTest(int idOrigen)
        {
            var respuesta = _servicio.GetParosByOrigen(idOrigen);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }


        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void GetParosByBusinessUnitTest(int idBusinessUnit)
        {
            var respuesta = _servicio.GetParosByBusinessUnit(idBusinessUnit);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void getParosByWorkCenterTest(int idWorkCenter)
        {
            var respuesta = _servicio.getParosByWorkCenter(idWorkCenter);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(9)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void PutParoTest(int id)
        {
            var respuestaParo = _servicio.GetParo(id);
            if (respuestaParo.EjecucionCorrecta && respuestaParo.Respuesta != null)
            {
                Paro paro = respuestaParo.Respuesta;
                paro.Activo = true;
                paro.FechaReporte = DateTime.Now;

                var respuesta = _servicio.PutParo(id, paro);
                Assert.IsTrue(respuesta.EjecucionCorrecta);
            }

        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(9)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void DeleteParoTest(int id)
        {
            var respuesta = _servicio.DeleteParo(id);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }
    }
}