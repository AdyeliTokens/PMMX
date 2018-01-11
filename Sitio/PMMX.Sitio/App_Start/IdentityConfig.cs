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
                    smail.Body = smail.Body + string.Format("  <img src = '~/img/maya/logo.jpg' /><br /><br /> ");
                    smail.Body = smail.Body + string.Format("  <div style = 'border - top:3px solid #22BCE5'>&nbsp;</div> ");
                    smail.Body = smail.Body + string.Format("  <span style = 'font - family:Arial; font - size:10pt'> ");
                    smail.Body = smail.Body + string.Format(" Hello <b></b>,<br /><br /> ");
                    smail.Body = smail.Body + string.Format("  A new event has been asigned to you.<br /><br /> ");
                    smail.Body = smail.Body + string.Format("  <h3> Se le ha asignado un nuevo evento " + evento.Descripcion + " con fecha " + evento.FechaInicio + "</h3>");
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
    }

        public class SmsService : IIdentityMessageService
        {
            public Task SendAsync(IdentityMessage message)
            {
                // Plug in your SMS service here to send a text message.
                return Task.FromResult(0);
            }
        }

        // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
        public class ApplicationUserManager : UserManager<ApplicationUser>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
            {
            }

            public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
                // Configure validation logic for usernames
                manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = true,
                    RequireUppercase = false,
                };

                // Configure user lockout defaults
                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
                // You can write your own provider and plug it in here.
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

        // Configure the application sign-in manager which is used in this application.
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


