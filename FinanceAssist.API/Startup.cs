using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FinanceAssist.API.Startup))]
namespace FinanceAssist.API
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}