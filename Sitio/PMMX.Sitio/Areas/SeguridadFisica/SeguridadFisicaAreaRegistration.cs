using System.Web.Mvc;

namespace Sitio.Areas.SeguridadFisica
{
    public class SeguridadFisicaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SeguridadFisica";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SeguridadFisica_default",
                "SeguridadFisica/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}