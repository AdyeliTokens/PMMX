using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Sitio.Helpers
{
    public static class MenuLinkHelper
    {
        public static MvcHtmlString MenuLink(this AjaxHelper ajaxHelper, string menuName, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            linkText = "<i class='"+linkText+"'></i><span>"+menuName+"</span>";

            if (ajaxOptions == null)
            {
                ajaxOptions = new AjaxOptions
                {
                    HttpMethod = "POST",
                    InsertionMode = InsertionMode.Replace,
                    OnSuccess = "",
                    OnBegin = "divLoading",
                    UpdateTargetId = "row-content"
                };
            }

            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }
    }
}