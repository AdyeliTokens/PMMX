using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMMX.Infraestructura.Contexto;
using PMMX.Operaciones.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Configuration;

namespace PMMX.Operaciones.Servicios.Tests
{
    [TestClass()]
    public class WorkCenterServicioTests
    {
        private readonly WorkCenterServicio _servicio;
        private readonly PMMXContext _context;
        

        public WorkCenterServicioTests()
        {
            
            _context = new PMMXContext();
            _servicio = new WorkCenterServicio(_context);

        }

        [TestMethod()]

        public void GetWorkCentersTest()
        {
            var respuesta = _servicio.GetWorkCenters();
            Assert.IsNotNull(respuesta);
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        public void GetWorkCenterTest(int idWorkCenter)
        {
            var respuesta = _servicio.GetWorkCenter(idWorkCenter);

            Assert.IsNotNull(respuesta);
        }

        [TestMethod()]
        [DataRow(12)]
        [DataRow(6)]
        [DataRow(3)]
        public void GetWorkCentersByPersonaTest(int idPersona)
        {
            var respuesta = _servicio.GetWorkCentersByPersona(idPersona);

            Assert.IsNotNull(respuesta);
        }
        
    }
}