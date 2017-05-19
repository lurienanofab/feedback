using LNF.Impl;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

[assembly: OwinStartup(typeof(Feedback.Startup))]

namespace Feedback
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            app.UseDataAccess();

            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
