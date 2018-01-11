using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMMX.Seguridad.Servicios
{
    public class UsuarioServicio
    {
        private PMMXContext db = new PMMXContext();

        public User getUser()
        {

            var usuario = db.Users.Select(x => x).FirstOrDefault();

            return usuario;
        }

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
            /*var dispositivos = db.Remitentes.Where(x => (x.IdHealthCheck == idGrupo))
                .Select(d => d.Puesto.Personas.SelectMany(p => p.Dispositivos.Select(r => new DispositivoView { Llave = r.Llave })).ToList()).FirstOrDefault();
            return dispositivos;*/
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

            //var mecanicos = db.Origens.Where(x => x.Id == idOrigen).Select(x => x.WorkCenter.BussinesUnit.Mecanicos.SelectMany(m =>
            //        m.Mecanico.Dispositivos.Where(d => d.Activo == true).Select(y => new DispositivoView
            //        {
            //            Id = y.Id,
            //            Llave = y.Llave
            //        })).ToList()).FirstOrDefault();

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

        public List<DispositivoView> GetDispositivoByJDI(int idJustDoIt)
        {
            List<DispositivoView> dispositivos = new List<DispositivoView>();

            dispositivos = db.JustDoIt
                .Where(e => e.Id == idJustDoIt)
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
                .Select(e => e.Responsable.Usuarios.Where(d => d.Activo == true).Select(v => new UserView
                {
                    Id = v.Id,
                    Email = v.Email
                }).ToList()
                ).FirstOrDefault();

            List<string> emails = emailList.Select(x => x.Email).ToList();
            string stringEmails = "";
             
            foreach (string email in emails)
            {
                stringEmails += (email + ",");
            }
            
            return stringEmails;
        }
    }
}
