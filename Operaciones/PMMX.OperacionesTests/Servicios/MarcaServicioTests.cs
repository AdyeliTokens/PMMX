using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMMX.Infraestructura.Contexto;
using PMMX.Operaciones.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Operaciones.Servicios.Tests
{
    [TestClass()]
    public class MarcaServicioTests
    {

        private readonly MarcaServicio _servicio;
        private readonly PMMXContext _context;


        public MarcaServicioTests()
        {

            _context = new PMMXContext();
            _servicio = new MarcaServicio(_context);

        }



        [TestMethod()]
        public void GetMarcasTest()
        {
            var respuesta = _servicio.GetMarcas();
            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }

        [TestMethod()]
        [DataRow("")]
        [DataRow("")]
        [DataRow(null)]
        public void GetMarcaTest(string code_FA)
        {
            var respuesta = _servicio.GetMarca(code_FA);
            Assert.IsTrue(respuesta.EjecucionCorrecta);
        }
    }
}