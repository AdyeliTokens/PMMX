using PMMX.Modelo.Account;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Servicios
{
    public interface IAccountServicio
    {
        IRespuestaServicio<User> Login(LoginModel login);
    }
}
