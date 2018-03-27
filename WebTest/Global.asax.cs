using AlineLMvcLib.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebTest
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
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

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}