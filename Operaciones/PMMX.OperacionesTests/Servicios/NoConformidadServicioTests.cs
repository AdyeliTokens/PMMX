using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Operaciones.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios.Tests
{
    [TestClass()]
    public class NoConformidadServicioTests
    {

        private readonly NoConformidadServicio _servicio;
        private readonly PMMXContext _context;


        public NoConformidadServicioTests()
        {

            _context = new PMMXContext();
            _servicio = new NoConformidadServicio(_context);

        }



        [TestMethod()]
        public void GetNoConformidadesTest()
        {
            var respuesta = _servicio.GetNoConformidades();
            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        public void GetNoConformidTest(int id)
        {
            var respuesta = _servicio.GetNoConformidad(id);
            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }

        [TestMethod()]
        public void PutNoConformidadTest()
        {
            NoConformidad noConformidad = new NoConformidad();
            noConformidad.IdPersona = 66;
            noConformidad.IdSeccion = 1;
            noConformidad.IdWorkCenter = 4;
            noConformidad.Fecha = DateTime.Now;
            noConformidad.Code = "PRUEBA TEST";
            noConformidad.CodeDescription = "PRUEBA TEST";
            noConformidad.Calificacion_VQI = 0;

            var respuesta = _servicio.PutNoConformidad(noConformidad);
            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }
    }
}