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
    public class WorkCenterServicioTests
    {
        [TestMethod()]
        public void GetWorkCentersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWorkCenterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWorkCentersByPersonaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWorkCentersByBussinesUnitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ActualizarWorkCenterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostWorkCenterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteWorkCenterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetWorkCenterByOperadorTest()
        {

            PMMXContext _context = new PMMXContext();
            var respuesta = new WorkCenterServicio(_context).GetWorkCenterByOperador(66);
            Assert.Fail();
        }
        
    }
}