using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using System.Web.Http;

[assembly:OwinStartup(typeof(Sitio.Startup))]
namespace Sitio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            
            

            ConfigureAuth(app);
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            app.MapSignalR();
        }
    }
}