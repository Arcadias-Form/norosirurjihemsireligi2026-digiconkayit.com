using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ArcForm_Web_v2
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
