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
    public class ActividadEnParoServicioTests
    {
        private readonly ActividadEnParoServicio _servicio;
        private readonly PMMXContext _context;


        public ActividadEnParoServicioTests()
        {

            _context = new PMMXContext();
            _servicio = new ActividadEnParoServicio(_context);

        }


        [TestMethod()]
        public void GetActividadesEnParoTest()
        {
            var respuesta = _servicio.GetActividadesEnParo();

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void GetActividadEnParoTest(int id)
        {
            var respuesta = _servicio.GetActividadEnParo(id);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void PutActividadEnParoTest(int id)
        {
            var respuestaActividad = _servicio.GetActividadEnParo(id);
            if (respuestaActividad.EjecucionCorrecta && respuestaActividad.Respuesta != null)
            {
                ActividadEnParo actividad = respuestaActividad.Respuesta;
                actividad.Fecha = DateTime.Now;

                var respuesta = _servicio.PutActividadEnParo(id, actividad);
                Assert.IsTrue(respuesta.EjecucionCorrecta);
            }
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
        [DataRow(10)]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(null)]
        public void PostActividadEnParoTest(int idParo)
        {
            ActividadEnParo actividad = new ActividadEnParo();
            actividad.Fecha = DateTime.Now;
            actividad.IdPersona = 66;
            actividad.Descripcion = "Ejemplo de Actividad por UnitTest";
            actividad.IdParo = idParo;
            var respuesta = _servicio.PostActividadEnParo(actividad);

            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }
    }
}