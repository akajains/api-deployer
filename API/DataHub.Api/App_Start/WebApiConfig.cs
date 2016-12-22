using System.Web.Http;
//using Saule.Http;

namespace DataHub.Api
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            // Web API configuration and services
            HttpConfiguration config = new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Use Saule as JSON API formatter.
            // Warning: Solution design models are not valid JSON API.
            //config.ConfigureJsonApi();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}
