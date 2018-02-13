using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PMMX.Modelo.Account;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using PMMX.Seguridad.Servicios;
using Sitio.Helpers;
using Sitio.Models;
using Sitio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Sitio.Areas.Apis.Controllers.Seguridad
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _AppUserManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }

        public AccountController()
        {
            _repo = new AuthRepository();
        }





        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/Login
        [AllowAnonymous]
        [Route("ExternalLogin")]
        public async Task<IHttpActionResult> ExternalLogin(LoginModel model)
        {
            RespuestaServicio<UserView> respuestaUser = new RespuestaServicio<UserView>();
            AccountService servicio = new AccountService(AppUserManager, SignInManager);
            var respuesta = await servicio.Login(model);
            switch (respuesta.Respuesta)
            {
                case SignInStatus.Success:
                    AccountServicio accountServicio = new AccountServicio();
                   
                    respuestaUser = accountServicio.ExternalLogin(model);

                    return Ok(respuestaUser);

                   
                case SignInStatus.LockedOut:
                    respuestaUser.Mensaje = "Lockout";
                    return Ok(respuestaUser);
                case SignInStatus.RequiresVerification:
                    respuestaUser.Mensaje = "RequiresVerification";
                    return Ok(respuestaUser);
                case SignInStatus.Failure:
                    respuestaUser.Mensaje = "Failure";
                    return Ok(respuestaUser);
                default:
                    respuestaUser.Mensaje = "Invalid login attempt.";
                    return Ok(respuestaUser);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}