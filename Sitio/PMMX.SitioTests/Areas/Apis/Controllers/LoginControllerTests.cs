using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Account;
using Sitio.Areas.Apis.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitio.Areas.Apis.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        private readonly LoginController _controller;
        

        public LoginControllerTests()
        {
            _controller = new LoginController();

        }

        
        [TestMethod()]
        public void PostLoginTest()
        {
            LoginModel model = new LoginModel();
            model.Email = "mario.chan@contracted.pmi.com";
            model.Password = "mario.chan";

            var respuesta = _controller.PostLoginAsync(model);
            Assert.IsNotNull(respuesta);
        }
    }
}