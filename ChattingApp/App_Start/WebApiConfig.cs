using System.Web.Http;

namespace ChattingApp
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{Id}/{uniqueId}",
                defaults: new {id = RouteParameter.Optional, uniqueId = RouteParameter.Optional}
                );
        }
    }
}