using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace Sitio
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;

        }
    }
}
