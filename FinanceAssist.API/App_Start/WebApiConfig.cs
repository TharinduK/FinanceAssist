using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace FinanceAssist.API
{
    internal class WebApiConfig
    {
        internal static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();

            //DI Container setup
            var container = new UnityContainer();
            container.LoadConfiguration();
            config.DependencyResolver = new UnityResolver(container);

            //configure automapper 
            AutomapperConfig.RegisterMappings();

            //web API route
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "default",
                routeTemplate: "{controller}/{id}",
                defaults: new {id= RouteParameter.Optional}
                );

            //do not support XML
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //json configuration 
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}