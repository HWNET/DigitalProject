using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Digital.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{PageIndex}",
                defaults: new { controller = "Users", action = "Login", PageIndex = UrlParameter.Optional}
            );
           // routes.MapRoute(
           //    name: "Default1",
           //    url: "{controller}/{action}/{Id}",
           //    defaults: new { controller = "Users", action = "Details", Id = UrlParameter.Optional }
           //);
        }
    }
}
