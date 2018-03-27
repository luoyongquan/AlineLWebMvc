using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlineLMvcLib.Route
{
    /// <summary>
    /// 全局路由表
    /// </summary>
    public static class RouteTable
    {
        public static IList<Route> Routes { get; set; }
        static RouteTable()
        {
            Routes = new List<Route>();
        }
        public static Route MatchRoutes(string requestUrl, out IDictionary<string, object> routeDate)
        {
            routeDate = null;
            foreach (Route route in RouteTable.Routes)
            {
                if (route.RouteMatch(requestUrl, out routeDate))
                {
                    return route;
                }
            }
            return null;
        }
    }
}
