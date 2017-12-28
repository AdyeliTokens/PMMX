using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PMMX.Modelo.Account;
using Sitio.Models;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System;

namespace Sitio.Helpers
{
    public class AccountService : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountService()
        {
            
        }

        public AccountService(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public async Task<RespuestaServicio<SignInStatus>> Login(LoginModel model)
        {
            

            RespuestaServicio<SignInStatus> respuesta = new RespuestaServicio<SignInStatus>();

            respuesta.Respuesta = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            return respuesta;
        }


        
        
        public async Task<RespuestaServicio<IdentityResult>> Register(RegisterViewModel model)
        {
            RespuestaServicio<IdentityResult> respuesta = new RespuestaServicio<IdentityResult>();
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                respuesta.Respuesta = await UserManager.CreateAsync(user, model.Password);
                
            }

            // If we got this far, something failed, redisplay form
            return respuesta;
        }

    }


}