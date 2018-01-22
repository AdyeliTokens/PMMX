using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sitio
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "FotosDeDefectos",
            url: "Fotos/Defectos/{idDefecto}",
            defaults: new { controller = "Fotos", action = "Defectos", idDefecto = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FotosDeOrigenes",
            url: "Fotos/Origenes/{idOrigen}",
            defaults: new { controller = "Fotos", action = "Origenes", idOrigen = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FotosDePersonas",
            url: "Fotos/Personas/{idPersona}",
            defaults: new { controller = "Fotos", action = "Personas", idPersona = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FotosDeJustDoIt",
            url: "Fotos/GembaWalk/{IdGembaWalk}",
            defaults: new { controller = "Fotos", action = "GembaWalk", IdGembaWalk = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FotosDeMantenimiento",
            url: "Fotos/Mantenimiento/{idMantenimiento}",
            defaults: new { controller = "Fotos", action = "Mantenimiento", idMantenimiento = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FotosDeEntornos",
            url: "Fotos/Entornos/{idEntorno}",
            defaults: new { controller = "Fotos", action = "Entorno", idEntorno = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "FotosDeMarcas",
            url: "Fotos/Marcas/{idMarca}",
            defaults: new { controller = "Fotos", action = "Marca", idMarca = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
