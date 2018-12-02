using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CutURL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Click",
                url: "{segment}",
                defaults: new { controller = "UrlShortner", action = "Click" }
            );
            routes.MapRoute(
                name: "ShowStats",
                url: "{segment}",
                defaults: new { controller = "UrlShortner", action = "ShowStats" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "UrlShortner", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "goback",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "UrlShortner", action = "GoBack",id=UrlParameter.Optional }
            );
        }
    }
}
