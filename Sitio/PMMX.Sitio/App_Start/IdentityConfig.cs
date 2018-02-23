using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Sitio.Models;
using System.Net.Mail;
using System.Configuration;
using PMMX.Modelo.Entidades.Operaciones;
using PMMX.Modelo.Entidades;
using PMMX.Modelo.Entidades.Warehouse;
using Sitio.Helpers;
using PMMX.Seguridad.Servicios;
using PMMX.Modelo.Vistas;

namespace Sitio
    {
        public class EmailService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                // Plug in your email service here to send an email.
                return Task.FromResult(0);
            }

            internal void PostSendGmail(int v1, object idCW, int v2, int idHC, int v3, int idMWCR)
            {
                throw new NotImplementedException();
            }

            public bool SendMail(string To_Mail,Evento evento)
            {
                try
                {
                    MailMessage smail = new MailMessage();
                    smail.IsBodyHtml = true;
                    smail.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                    smail.From = new MailAddress("pmm.isoperation@gmail.com", "maya@pmi.com");
                
                    string[] emails = To_Mail.Split(',');
                    foreach (string email in emails)
                    {
                        if (email != "") smail.To.Add(email);
                    }
                    
                    smail.Subject = "[PMMX Notification] Info: " + evento.Descripcion;
                    smail.Body = string.Format("<html><head><meta charset='UTF-8'></head><body>  ");
                    //smail.Body = smail.Body + string.Format("  <img src = '~/img/maya/logo.jpg' /><br /><br /> ");
                    smail.Body = smail.Body + string.Format("<div style = 'border - top:3px solid #22BCE5'>&nbsp;</div> ");
                    smail.Body = smail.Body + string.Format("<span style = 'font - family:Arial; font - size:10pt'> ");
                    smail.Body = smail.Body + string.Format(" Hello <b></b>,<br /><br /> ");
                    smail.Body = smail.Body + string.Format("   A new event has been asigned to you <h3>" + evento.Descripcion + "</h3> to " + evento.FechaInicio);
                    smail.Body = smail.Body + string.Format(" <br /><br /> ");
                    smail.Body = smail.Body + string.Format("  Thanks<br /> ");
                    smail.Body = smail.Body + string.Format(" </span></body></html> ");

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

            public bool SendMail(string To_Mail, Ventana ventana)
            {
            try
            {
                MailMessage smail = new MailMessage();
                smail.IsBodyHtml = true;
                smail.BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                smail.From = new MailAddress("pmm.isoperation@gmail.com", "maya@pmi.com");

                string[] emails = To_Mail.Split(',');
                foreach (string email in emails)
                {
                    if (email != "") smail.To.Add(email);
                }

                var estatus = ventana.StatusVentana.OrderByDescending(s => s.Fecha).Select(s => s.Status).FirstOrDefault();

                smail.Subject = "[PMMX Notification] Ventana: " + ventana.PO +" "+ventana.NombreCarrier+" "+ ventana.TipoOperacion.Nombre;
                smail.Body = string.Format("<html><head><meta charset='UTF-8'></head>");
                smail.Body = smail.Body + string.Format("<body> <div style='width:100%'>"
                                                        + "<div align='center' style='font-weight:bold; text-align: center; width:50%; margin: 0 auto; display: table; background: #D6EAF8;' >");
                smail.Body = smail.Body + string.Format(" <h1 style ='text - transform: uppercase; background: #21618C; color: #FFFFFF;'> Ventana " + ventana.PO + " </h1>");
                //smail.Body = smail.Body + string.Format(" <br /><br /><img src='~/img/augmented-reality/screen-1.png' alt=''><br /><br /> ");
                smail.Body = smail.Body + string.Format("Has been change to status to <span style='color: #5caad2;'>" + estatus.Nombre);
                    
                    if(ventana.BitacoraVentana.Where(b => (b.IdVentana == ventana.Id) && (b.Estatus.Id == estatus.Id) ).OrderByDescending(v=> v.Fecha).Select(v => v.IdRechazo).Count() > 0  )
                    {
                        smail.Body = smail.Body + string.Format(" Rejected by" 
                            + "</br><span style='color: #21618C;'>"
                            + ventana.BitacoraVentana.OrderByDescending(s => s.Fecha).Select(s => s.Rechazo.Nombre).FirstOrDefault() 
                            + "</span>");
                    }

                    smail.Body = smail.Body + string.Format(" <br /><br /><br /><br /> ");
                    smail.Body = smail.Body + string.Format("<h3 style ='text - transform: uppercase; background: #21618C; color: #FFFFFF;'><a style='color: #FFFFFF;'' href='https://serverpmi.tr3sco.net/'>For more information click here</a><br /></h3> ");
                    smail.Body = smail.Body + string.Format(" </div>" 
                                            +   "</div>" 
                                            +   "</body>" 
                                            +   "</html> ");

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("pmm.isoperation@gmail.com", "82000100");
                    smtp.Send(smail);

                    NotificationService notify = new NotificationService();
                    UsuarioServicio usuarioServicio = new UsuarioServicio();

                    List<DispositivoView> dispositivos = usuarioServicio.GetDispositivoByEvento(ventana.IdEvento);
                    List<string> llaves = dispositivos.Select(x => x.Llave).ToList();

                    foreach (string notificacion in llaves)
                    {
                        notify.SendPushNotification(notificacion, "Ventana: " + ventana.Evento.Descripcion + ". ", "La ventana "+ ventana.Evento.Descripcion +" ha cambiado de estatus a "+ estatus.Nombre);
                    }

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

        public class SmsService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                // Plug in your SMS service here to send a text message.
                return Task.FromResult(0);
            }
        }

        public class ApplicationUserManager : UserManager<ApplicationUser>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
            {
            }

            public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
                
                manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = true,
                    RequireUppercase = false,
                };

                
                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                
                manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
                {
                    MessageFormat = "Your security code is {0}"
                });
                manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });
                manager.EmailService = new EmailService();
                manager.SmsService = new SmsService();
                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }
        }

        public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
        {
            public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
            {
            }

            public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
            {
                return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
            }

            public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
            {
                return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            }
        }
    }


