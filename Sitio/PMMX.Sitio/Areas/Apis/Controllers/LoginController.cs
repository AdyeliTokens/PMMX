using PMMX.Modelo.Vistas;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.Account;
using Sitio.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using PMMX.Infraestructura.Contexto;

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

            AccountService servicio = new AccountService();
            LoginModel model = new LoginModel() { Email = Email, Password = Password, Llave = Llave };
            var respuesta = await servicio.Login(model);
            return Ok(respuesta.Respuesta);
        }

        [HttpPost]
        [ResponseType(typeof(RespuestaServicio<UserView>))]
        public IHttpActionResult PostLogin(LoginModel login)
        {
            AccountServicio servicio = new AccountServicio();
            RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            respuesta = servicio.Login(login);

            return Ok(respuesta);

        }



    }
}
