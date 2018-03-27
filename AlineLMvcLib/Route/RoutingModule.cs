using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace AlineLMvcLib.Route
{
    public class RoutingModule : System.Web.IHttpModule
    {
        public void Dispose()
        {
            this.Dispose();
        }
        public void Init(HttpApplication application)
        {
            application.PostResolveRequestCache += Application_PostResolveRequestCache;
        }

        private void Application_PostResolveRequestCache(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            HttpContext context = application.Context;
            IDictionary<string, object> routeDate = null;
            Route route = RouteTable.MatchRoutes(context.Request.RawUrl, out routeDate);
            if (route == null)
            {//未匹配到路由规则
                throw new HttpException(404, "Not Found");
            }
            if (!routeDate.ContainsKey("controller"))
            {//路由规则不包含控制器
                throw new HttpException(404, "Not Found");
            }
            //获取执行路由的Handler
            IHttpHandler httphandler = route.GetRouteHandler(routeDate);
            //执行HttpHandler
            context.RemapHandler(httphandler);
        }
    }
}
