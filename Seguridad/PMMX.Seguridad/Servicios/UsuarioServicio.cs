using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PMMX.Modelo.Entidades.Warehouse;

namespace PMMX.Seguridad.Servicios
{
    public class UsuarioServicio
    {
        private PMMXContext db = new PMMXContext();
        
        public List<DispositivoView> GetDispositivosDeMecanicosByBussinesUnit(int idBussinesUnit)
        {
            List<DispositivoView> dispositivos = new List<DispositivoView>();
            //var dispositivos = db.Mecanico.Where(x => x.IdBusinessUnit == idBussinesUnit).SelectMany(x => new DispositivoView { Id = x.Mecanico.Dispositivos.Select( d=> d.Id) }).ToList();
            //var dispositivos = db.BussinesUnits.Where(x => x.Id == idBussinesUnit)
            //    .Select(d => d.Mecanicos.SelectMany(p => p.Mecanico.Select(r => new DispositivoView { Llave = r.Llave })).ToList()).FirstOrDefault();
            return dispositivos;
        }

        public List<DispositivoView> GetDispositivosByOrigenAndGrupoPreguntas(int idOrigen, int idGrupo)
        {
            var dispositivos = db.Origens.Where(o => (o.Id == idOrigen))
                .Select( o => (o.WorkCenter.Responsable.Dispositivos.Where( d=> (d.Activo == true)).Select( y => new DispositivoView
                {
                    Id = y.Id,
                    Llave = y.Llave
                }).ToList())).FirstOrDefault()
                ;

            return dispositivos;
        }

        public List<DispositivoView> GetMecanicosPorOrigen(int idOrigen)
        {
            List<DispositivoView> dispositivos = new List<DispositivoView>();
            
            dispositivos = db.Origens.Where(x => x.Id == idOrigen).Select(x => x.WorkCenter.BussinesUnit.Mecanicos.SelectMany(m =>
                    m.Mecanico.Dispositivos.Where(d => d.Activo == true).Select(y => new DispositivoView
                    {
                        Id = y.Id,
                        Llave = y.Llave
                    })).ToList()).FirstOrDefault();

            return dispositivos;
        }

        public List<DispositivoView> GetDispositivoByEvento(int idEvento)
        {
            List<DispositivoView> dispositivos = new List<DispositivoView>();

            dispositivos = db.EventoResponsable
                .Where(e => e.IdEvento == idEvento)
                .Select(e => e.Responsable.Dispositivos.Where(d => d.Activo == true).Select(v => new DispositivoView
                {
                    Id = v.Id,
                    Llave = v.Llave
                }).ToList()
                ).FirstOrDefault();
            
            return dispositivos;
        }

        public List<DispositivoView> GetDispositivoByMantenimiento(int idMantenimiento)
        {
            List<DispositivoView> dispositivos = new List<DispositivoView>();

            dispositivos = db.Mantenimiento
                .Where(e => e.Id == idMantenimiento)
                .Select(e => e.Responsable.Dispositivos.Where(d => d.Activo == true).Select(v => new DispositivoView
                {
                    Id = v.Id,
                    Llave = v.Llave
                }).ToList()
                ).FirstOrDefault();

            return dispositivos;
        }

        public string GetEmailByEvento(int idEvento)
        {
            var emailList = db.EventoResponsable
                .Where(e => e.IdEvento == idEvento)
                .Select(e => e.Responsable.Users.Where(v => v.IdPersona == e.IdResponsable).Select(v => new UserView
                {
                    Id = v.Id,
                    Email = v.Usuario.Email
                }).ToList()
                ).ToList();
                        
            List<string> emails = emailList.Select(x => x.Select(y => y.Email).FirstOrDefault()).ToList();
            string stringEmails = "";
             
            foreach (string email in emails)
            {
                stringEmails += (email + ",");
            }
            
            return stringEmails;
        }

        public string GetEmailByStatus ( Ventana ventana )
        {
            string stringEmails = "";
            List<UserView> emailList = new List<UserView>();
            var IdStatus = ventana.StatusVentana.OrderByDescending(s => s.Fecha).Select(s => s.IdStatus).FirstOrDefault();

            var workFlow = db.WorkFlows
                           .Where(w => (w.Inicial == IdStatus ) 
                                       && (w.IdSubCategoria == ventana.IdSubCategoria))
                           .FirstOrDefault(); 

            if (workFlow.AlertaProveedor == true)
            {
                emailList = db.ListaDistribucion
                        .Where(l => (l.IdSubarea == workFlow.IdSubAreaANotificar) || (l.IdProveedor == ventana.IdProveedor) && (l.Activo == true))
                        .Select(l=> l.Remitente.Users.Where( r=> (r.IdPersona == l.Remitente.Id))
                        .Select(r => new UserView {
                            Id = r.Id,
                            Email = r.Usuario.Email
                        }).FirstOrDefault()
                        ).ToList();
            }
            else
            {
                emailList = db.ListaDistribucion
                        .Where(l => (l.IdSubarea == workFlow.IdSubAreaANotificar))
                              .Select(l => l.Remitente.Users.Where(r => (r.IdPersona == l.Remitente.Id) && (l.Activo == true))
                        .Select(r => new UserView
                        {
                            Id = r.Id,
                            Email = r.Usuario.Email
                        }).FirstOrDefault()
                        ).ToList();
            }
            
            List<string> emails = emailList.Where(x=>  x.Email != null).Select(x => x.Email).ToList();
            
            foreach (string email in emails)
            {
                stringEmails += (email + ",");
            }

            return stringEmails;
        }
    }
}
