using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Account;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Seguridad.Servicios
{
    public class AccountServicio
    {
        private PMMXContext _context;

        public AccountServicio(PMMXContext context)
        {
            _context = context;
        }

        public AccountServicio()
        {
            _context = new PMMXContext();
        }

        

        public RespuestaServicio<UserView> ExternalLogin(LoginModel login)
        {
            RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            respuesta = GetUserExternal(login);
            if (respuesta.EjecucionCorrecta)
            {
                if (login.Llave != null || login.Llave != "")
                {
                    DispositivoServicio dispositivoServicio = new DispositivoServicio();
                    RespuestaServicio<DispositivoView> dispositivoRespuesta = new RespuestaServicio<DispositivoView>();
                    dispositivoRespuesta = dispositivoServicio.DispositivoActualByPersona(respuesta.Respuesta.Persona.Id, login.Llave);
                    if (dispositivoRespuesta.EjecucionCorrecta == false)
                    {
                        return respuesta;
                    }
                }

                return respuesta;
            }
            else
            {
                return respuesta;
            }

        }
        
        public RespuestaServicio<UserView> GetUserExternal(LoginModel login)
        {
            RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            var user = _context.AspNetUser
                .Where(x => x.Email == login.Email)
                .Select(u => new UserView
                {
                    Email = u.Email,
                    IdPersona = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Id,
                    Activo = true,
                    UserName = u.UserName,
                    Entornos = u.Roles.Select(e => new EntornoView
                    {
                        Id = e.Id,
                        Nombre = e.Name,
                        Activo = true
                    }).Where(o => o.Activo == true).ToList(),
                    Persona = new PersonaView
                    {
                        Id = u.PersonasConEsteUsuario.FirstOrDefault().Id,
                        Nombre = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Nombre,
                        Apellido1 = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Apellido1,
                        Apellido2 = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Apellido2,
                        IdPuesto = u.PersonasConEsteUsuario.FirstOrDefault().Persona.IdPuesto,
                        Activo = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Activo,
                        Puesto = new PuestoView
                        {
                            Id = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Puesto.Id,
                            Nombre = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Puesto.Nombre,
                            Activo = u.PersonasConEsteUsuario.FirstOrDefault().Persona.Puesto.Activo
                        }
                    }
                })
                .FirstOrDefault();





            if (user != null)
            {
                if (user.Entornos.Count() == 1)
                {
                    user.Entorno = user.Entornos.FirstOrDefault();
                    user.IdEntorno = user.Entornos.FirstOrDefault().Id;
                }

                if (user.Persona == null)
                {
                    respuesta.Mensaje = "Usuario no esta ligado a una persona";
                    return respuesta;
                }
                else
                {
                    respuesta.Respuesta = user;
                }
            }
            else
            {
                respuesta.Mensaje = "Usuario no encontrado";
                return respuesta;
            }

            return respuesta;
        }


    }
}

