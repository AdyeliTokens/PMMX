using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Interfaces.Seguridad;
using PMMX.Modelo.RespuestaGenerica;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Seguridad.Servicios
{

    public class DispositivoServicio
    {
        #region Contexto

        private PMMXContext db = new PMMXContext();

        #endregion

        #region Gets
        public RespuestaServicio<DispositivoView> DispositivoActualByPersona(int idPersona, string llave)
        {
            RespuestaServicio<DispositivoView> respuesta = new RespuestaServicio<DispositivoView>();
            var listaDispositivos = GetDispositivosByPersona(idPersona);
            if (listaDispositivos.EjecucionCorrecta)
            {
                if(listaDispositivos == null || listaDispositivos.Respuesta.Count == 0)
                {
                    Dispositivo dispositivoNuevo = new Dispositivo();
                    dispositivoNuevo.Llave = llave;
                    dispositivoNuevo.IdPersona = idPersona;
                    dispositivoNuevo.Activo = true;
                    respuesta = Agregar(dispositivoNuevo);
                }
                else
                {
                    DispositivoView dispositivo = listaDispositivos.Respuesta.Where(x => x.Llave == llave).FirstOrDefault();
                    if (dispositivo != null)
                    {

                        List<Dispositivo> dispositivosActulizar = listaDispositivos.Respuesta.Select(x => new Dispositivo { Id = x.Id, Activo = false, IdPersona = x.IdPersona, Llave = x.Llave }).Where(x => x.Llave != llave).ToList();
                        Dispositivo disp = listaDispositivos.Respuesta.Select(x => new Dispositivo { Id = x.Id, Activo = false, IdPersona = x.IdPersona }).Where(x => x.Llave == llave).FirstOrDefault();
                        foreach(Dispositivo elem in dispositivosActulizar)
                        {
                            elem.Activo = false;
                        }
                        dispositivosActulizar.Add(disp);
                        var dispositivosUpdate = UpdateDispositivos(dispositivosActulizar);
                    }
                    else
                    {
                        Dispositivo dispositivoNuevo = new Dispositivo();
                        dispositivoNuevo.Llave = llave;
                        dispositivoNuevo.IdPersona = idPersona;
                        dispositivoNuevo.Activo = true;
                        respuesta = Agregar(dispositivoNuevo);
                    }
                }
            }
            
            return respuesta;
        }

        public RespuestaServicio<List<DispositivoView>> GetDispositivosByPersona(int idPersona)
        {
            RespuestaServicio<List<DispositivoView>> respuesta = new RespuestaServicio<List<DispositivoView>>();
            List<DispositivoView> dispositivos = db.Dispositivos.Select(x => new DispositivoView { Id = x.Id, IdPersona = x.IdPersona, Llave = x.Llave, Activo = x.Activo }).Where(x => x.IdPersona == idPersona).ToList();
            
                respuesta.Respuesta = dispositivos;
            

            return respuesta;
        }

        #endregion

        #region Put

        public RespuestaServicio<DispositivoView> Agregar(Dispositivo dispositivo)
        {
            RespuestaServicio<DispositivoView> respuesta = new RespuestaServicio<DispositivoView>();
            if (dispositivo.Llave == null || dispositivo.Llave.Count() == 0) {
                respuesta.Mensaje = "El dispositivo no se pudo guardar correctamente";
            }
            else {
                db.Dispositivos.Add(dispositivo);
                db.SaveChanges();

                if (dispositivo.Id > 0)
                {
                    DispositivoView dispositivoGuardado = new DispositivoView();
                    dispositivoGuardado.Id = dispositivo.Id;
                    dispositivoGuardado.IdPersona = dispositivo.IdPersona;
                    dispositivoGuardado.Llave = dispositivo.Llave;
                    dispositivoGuardado.Activo = dispositivo.Activo;
                    respuesta.Respuesta = dispositivoGuardado;

                }
                else
                {
                    respuesta.Mensaje = "El dispositivo no se pudo guardar correctamente";
                }
            }
            
            return respuesta;
        }

        #endregion

        #region Posts

        public RespuestaServicio<bool> UpdateDispositivos(List<Dispositivo> dispositivos)
        {
            RespuestaServicio<bool> respuesta = new RespuestaServicio<bool>();
            db.SaveChanges();
            return respuesta;
        }

        #endregion




    }
}
