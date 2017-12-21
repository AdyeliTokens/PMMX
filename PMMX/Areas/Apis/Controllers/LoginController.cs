using PMMX.Modelo.Vistas;
using System.Web.Http;
using System.Web.Http.Description;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.Account;

namespace Sitio.Areas.Apis.Controllers
{
    public class LoginController : ApiController
    {

        
        [HttpGet]
        [ResponseType(typeof(UserView))]
        public IHttpActionResult GetLogin(string Email, string Password, string Llave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AccountServicio servicio = new AccountServicio();
            RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            LoginModel login = new LoginModel();
            login.Email = Email;
            login.Password = Password;
            login.Llave = Llave;

            respuesta = servicio.Login(login);
            
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
