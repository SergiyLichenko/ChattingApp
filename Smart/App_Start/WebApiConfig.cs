namespace Smart
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{uniqueId}",
                defaults: new {id = RouteParameter.Optional, uniqueId = RouteParameter.Optional}
                );

            // Controllers with Actions
            // To handle routes like `/api/VTRouting/route`


            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


        }
    }
}