using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Account;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Interfaces.Seguridad;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Servicios;
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
        private PMMXContext db = new PMMXContext();

        public RespuestaServicio<UserView> Login(LoginModel login)
        {
            RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            respuesta = GetUser(login);
            if (respuesta.EjecucionCorrecta)
            {
                if (login.Llave != null || login.Llave != "")
                {
                    DispositivoServicio dispositivoServicio = new DispositivoServicio();
                    RespuestaServicio<DispositivoView> dispositivoRespuesta = new RespuestaServicio<DispositivoView>();
                    dispositivoRespuesta = dispositivoServicio.DispositivoActualByPersona(respuesta.Respuesta.Persona.Id, login.Llave);
                    if (dispositivoRespuesta.EjecucionCorrecta == false)
                    {
                        //respuesta.Mensaje = dispositivoRespuesta.Mensaje;
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


        public RespuestaServicio<UserView> GetUser(LoginModel login)
        {
            RespuestaServicio<UserView> respuesta = new RespuestaServicio<UserView>();
            var user = db.Users
                .Where(x => x.Email == login.Email && x.Password == login.Password)
                .Select(u => new UserView
                {
                    Id = u.Id,
                    Email = u.Email,
                    IdPersona = u.IdPersona,
                    Password = u.Password,
                    Activo = u.Activo,
                    UserName = u.UserName,
                    Entornos = u.Entornos.Select(e => new EntornoView
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Activo = e.Activo
                    }).Where(o => o.Activo == true).ToList(),
                    Persona = new PersonaView
                    {
                        Id = u.Persona.Id,
                        Nombre = u.Persona.Nombre,
                        Apellido1 = u.Persona.Apellido1,
                        Apellido2 = u.Persona.Apellido2,
                        IdPuesto = u.Persona.IdPuesto,
                        Activo = u.Persona.Activo,
                        Puesto = new PuestoView
                        {
                            Id = u.Persona.Puesto.Id,
                            Nombre = u.Persona.Puesto.Nombre,
                            Activo = u.Persona.Puesto.Activo
                        }
                    }
                })
                .FirstOrDefault();

            



            if (user != null)
            {
                if (user.Entornos.Count() == 1) {
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

