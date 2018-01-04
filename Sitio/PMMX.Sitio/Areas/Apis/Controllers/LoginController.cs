using PMMX.Modelo.Vistas;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.Account;
using Sitio.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;


namespace Sitio.Areas.Apis.Controllers
{
    public class LoginController : ApiController
    {
        
        [HttpGet]
        [ResponseType(typeof(UserView))]
        public async Task<IHttpActionResult> GetLoginAsync(string Email, string Password, string Llave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountService servicio = new AccountService(  );
            LoginModel model = new LoginModel() { Email = Email , Password = Password , Llave = Llave };
            var respuesta = await servicio.Login(model);

            //switch (respuesta.Respuesta)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}


            //AccountServicio servicio = new AccountServicio();
            //RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            //LoginModel login = new LoginModel();
            //login.Email = Email;
            //login.Password = Password;
            //login.Llave = Llave;

            //respuesta = servicio.Login(login);

            return Ok(respuesta.Respuesta);

        }

        [HttpPost]
        [ResponseType(typeof(UserView))]
        public IHttpActionResult PostLogin(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountServicio servicio = new AccountServicio();
            RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            respuesta = servicio.Login(login);

            return Ok(respuesta.Respuesta);

        }



    }
}
