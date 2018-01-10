using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Data.Entity;
using PMMX.Infraestructura.Contexto;
using PMMX.Modelo.Vistas;
using System.Configuration;

namespace Sitio.Helpers
{
    public class EmailService
    {

        private PMMXContext db = new PMMXContext();

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string PostSendGmail()
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("pmm.isoperation@gmail.com", "82000100");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            //can be obtained from your model
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("pmm.isoperation@gmail.com");
            msg.To.Add(new MailAddress("adriana.flores@contracted.pmi.com"));

            msg.Subject = "Message from A.info";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>Message Email</b></body>");
            try
            {
                client.Send(msg);
                return "OK";
            }
            catch (Exception ex)
            {

                return "error:" + ex.ToString();
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string PostSendGmailParameters(int idOrigen, int idHealthCheck, List<PMMX.Modelo.Vistas.RespuestaView> lista)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("pmm.isoperation@gmail.com", "82000100");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("pmm.isoperation@gmail.com", "maya@pmi.com");

            var elements = db.Remitentes
                .Where(q => (q.IdHealthCheck.Equals(idHealthCheck)) && (q.Origen.Id.Equals(idOrigen)))
                .Select(x => new RemitentesView
                {
                    NombreHealthcheck = x.HealthCheck.Nombre,
                    NombreWorkCenter = x.Origen.WorkCenter.Nombre
                }).FirstOrDefault();
            
            var remitentes = db.Remitentes
               .Join(db.Users,
               r => r.IdPuesto,
               p => p.Persona.Puesto.Id,
               (r, p) => new { Remitentes = r, User = p })
               .Where(q => (q.Remitentes.IdHealthCheck.Equals(idHealthCheck)) && (q.Remitentes.Origen.Id.Equals(idOrigen)))
               .Select( x => new UserView {
                   Email = x.User.Email
               })
               .ToList();

            foreach (UserView element in remitentes)
            {
                msg.To.Add(new MailAddress(element.Email));
            }

            msg.To.Add(new MailAddress("adriana.flores@contracted.pmi.com"));

            
            msg.Subject = "[Health Checks] Info: "+elements.NombreWorkCenter+" " +elements.NombreHealthcheck;
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head><meta charset='UTF-8'></head><body>  ");
            msg.Body = msg.Body + string.Format(" <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' bgcolor='#f3f2f2' id='bodyTable'> ");
            msg.Body = msg.Body + string.Format("  <tr><td align='center' valign='top'> ");
            msg.Body = msg.Body + string.Format("  <table border='0' cellpadding='20' cellspacing='0' width='600' id='emailContainer'> ");
            msg.Body = msg.Body + string.Format(" <tr><td align='center' valign='top'> ");
            msg.Body = msg.Body + string.Format("  <table border='0' cellpadding='20' cellspacing='0' width='100%' id='emailHeader'> ");
            msg.Body = msg.Body + string.Format(" <tr><td align='left' valign='top' > ");
            msg.Body = msg.Body + string.Format("  <h3> Se ha realizado el Health Check "+elements.NombreHealthcheck+" en el modulo "+elements.NombreWorkCenter+"</h3>");
            msg.Body = msg.Body + string.Format(" </td></tr></table> ");
            msg.Body = msg.Body + string.Format("  </td></tr> ");
            msg.Body = msg.Body + string.Format(" <tr><td align='center' valign='top'> ");
            msg.Body = msg.Body + string.Format(" <tr><td align='center' valign='top'> ");

            if (lista.Count > 0)
            {
                msg.Body = msg.Body + string.Format(" <table border='0' cellpadding='20' cellspacing='0' width='100%' id='emailBody' bgcolor='#ffffff' > ");
                msg.Body = msg.Body + string.Format(" <tr bgcolor='#0067ac' color='#ffffff'><th>Total</th><th>TotalNo</th><th>TotalSi</th><th>%No</th><th>%Si</th></tr> ");
                msg.Body = msg.Body + string.Format(" <tr> ");
                msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + lista[0].TotalSolucion.ToString() + " </td>");
                msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + lista[0].TotalNo.ToString() + " </td>");
                msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + lista[0].TotalSi.ToString() + " </td>");
                msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + lista[0].PorcentajeNo.ToString() + " </td>");
                msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + lista[0].PorcentajeSi.ToString() + " </td>");
                msg.Body = msg.Body + string.Format(" </tr>");
                msg.Body = msg.Body + string.Format(" </table> ");

                if (lista[0].TotalNo > 0)
                {
                    msg.Body = msg.Body + string.Format(" <table border='0' cellpadding='20' cellspacing='0' width='100%' id='emailBody' bgcolor='#ffffff'> ");
                    msg.Body = msg.Body + string.Format(" <tr bgcolor='#0067ac' color='#ffffff'><th>Pregunta</th><th>Comentario</th><th>Por</th></tr> ");
                    foreach (PMMX.Modelo.Vistas.RespuestaView element in lista)
                    {
                        msg.Body = msg.Body + string.Format(" <tr> ");
                        msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + element.DescripcionPregunta.ToString() + " </td>");
                        msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + element.Comentario.ToString() + " </td>");
                        msg.Body = msg.Body + string.Format(" <td align='center' valign='top'> " + element.RespuestaBy.ToString() + " </td>");
                        msg.Body = msg.Body + string.Format(" </tr>");
                    }
                    msg.Body = msg.Body + string.Format(" </table> ");
                }
            }
            msg.Body = msg.Body + string.Format(" </td></tr> ");
            msg.Body = msg.Body + string.Format(" </td></tr></table> ");
            msg.Body = msg.Body + string.Format(" </td></tr></table> ");
            msg.Body = msg.Body + string.Format(" </body> ");

            try
            {
                client.Send(msg);
               return "OK";
            }
            catch (Exception ex)
            {

               return "error:" + ex.ToString();
            }
        }

        public bool SendMail(string To_Mail)
        {
            try
            {
                MailMessage smail = new MailMessage();
                smail.IsBodyHtml = true;
                smail.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                smail.From = new MailAddress("pmm.isoperation@gmail.com", "Philip Morris");
                smail.To.Add(new MailAddress(To_Mail));
                smail.Subject = "Test PMMX";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("pmm.isoperation@gmail.com", "82000100");
                smtp.Send(smail);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        
    }
}
