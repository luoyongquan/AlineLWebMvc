using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AlineLMvcLib.Route
{
    /// <summary>
    /// 单条路由
    /// </summary>
    public class Route
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Route(string urlTemplate, object routeDate, Func<IDictionary<string, object>, IHttpHandler> handler)
        {
            this.UrlTemplate = urlTemplate;
            this.GetRouteHandler = handler;
            this.Defaults = new Dictionary<string, object>();
            var defaultProperties = routeDate.GetType().GetProperties();
            foreach (var item in defaultProperties)
            {
                Defaults.Add(item.Name, item.GetValue(routeDate));
            }
        }
        /// <summary>
        /// 路由的Url模板
        /// {Controller}/{Action}/...
        /// </summary>
        public string UrlTemplate
        {

            get;
            set;
        }
        /// <summary>
        /// 默认路由 
        /// new { controller = "Home", action = "Index" }
        /// </summary>
        public IDictionary<string, object> Defaults
        {

            get;
            set;
        }
        /// <summary>
        /// 获取执行方法的handle
        /// </summary>
        public Func<IDictionary<string, object>, IHttpHandler> GetRouteHandler
        {
            get; set;
        }
        public bool RouteMatch(string requestUrl, out IDictionary<string, object> RouteDate)
        {
            RouteDate = new Dictionary<string, object>();
            foreach (var item in this.Defaults)
            {//默认路由规则
                RouteDate.Add(item.Key, item.Value);
            }
            string[] requestUrlItems = requestUrl.Split(new char[] { '/'}, StringSplitOptions.RemoveEmptyEntries);
            string[] UrlTemplateItems = UrlTemplate.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (requestUrlItems.Length != UrlTemplateItems.Length)
            {//路由规则不匹配  长度不符
                RouteDate.Clear();
                return false;
            }
            for (int i = 0; i < requestUrlItems.Length; i++)
            {
                var requestUrlItem = requestUrlItems[i];
                var UrlTemplateItem = UrlTemplateItems[i];
                if (UrlTemplateItem.StartsWith("{") && UrlTemplateItem.EndsWith("}"))
                {//模板的当前模块是占位符
                    var routeDataKey = UrlTemplateItem.Trim("{}".ToArray());
                    if (RouteDate.ContainsKey(routeDataKey))
                    {//路由包含模板占位符, 替换
                        RouteDate[routeDataKey] = requestUrlItem;
                    }
                }
                else
                {//模板的当前模块非占位符 必须完全相等 忽略大小写的区块字符比较
                    if (!UrlTemplate.Equals(requestUrlItem, StringComparison.InvariantCultureIgnoreCase))
                    {
                        RouteDate.Clear();
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
