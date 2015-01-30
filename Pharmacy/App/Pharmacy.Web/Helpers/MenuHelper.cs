using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Pharmacy.Web.Helpers
{
    public static class MenuHelper
    {
        public static MvcHtmlString MenuLink(this HtmlHelper helper, string text, string action, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"];

            if (String.Equals(controller, currentController as string, StringComparison.OrdinalIgnoreCase))
            {
                return new MvcHtmlString("<li class=\"active\">" + helper.ActionLink(text, action, controller) + "</li>");
            }
            return new MvcHtmlString("<li>" + helper.ActionLink(text, action, controller) + "</li>");
        }
    }
}