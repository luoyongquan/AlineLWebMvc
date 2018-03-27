using AlineLMvcLib.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AlineLMvcLib
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 注册路由规则1
            RouteTable.Routes.MapRoute(
                urlTemplate: "{controller}/{action}/{id}",
                routeDefaultData: new { controller = "Home", action = "Index" }
                );
            // 注册路由规则2
            RouteTable.Routes.MapRoute(
                urlTemplate: "{controller}/{action}",
                routeDefaultData: new { controller = "Home", action = "Index" }
                );
            // 注册路由规则3
            RouteTable.Routes.MapRoute(
                urlTemplate: "{controller}",
                routeDefaultData: new { controller = "Home", action = "Index" }
                );
        }

    }
}
