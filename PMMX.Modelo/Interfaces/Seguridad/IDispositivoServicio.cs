using PMMX.Modelo.Entidades;
using PMMX.Modelo.RespuestaGenerica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Modelo.Interfaces.Seguridad
{
    public interface IDispositivoServicio
    {
        IRespuestaServicio<Dispositivo> Agregar(Dispositivo dispositivo);
    }
}
