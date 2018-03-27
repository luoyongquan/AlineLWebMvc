using AlineLMvcLib.HttpHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AlineLMvcLib.Route
{
    public static class RouteExpand
    {
        public static void MapRoute(this IList<Route> source, string urlTemplate,  object routeDefaultData)
        {
            MapRoute(source,urlTemplate, routeDefaultData, routeDate=>new MvcHandle(routeDate));
        }
        public static void MapRoute(this IList<Route> source, string urlTemplate, object routeDefaultData, Func<IDictionary<string, object>, IHttpHandler> handler)
        {
            source.Add(new Route(urlTemplate, routeDefaultData, handler));
        }
    }
}
