using System.Web.Mvc;
using System.Web.Routing;

namespace ChattingApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{Id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}