using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace AlineLMvcLib.Mvc
{
    public class JsonResult : ActionResult
    {
        public object _content { get; set; }
        public string _contentType { get; set; } = "application/json";
        public JsonResult(object content)
        {
            this._content = content;
        }
        public override void Execute(HttpContext context, IDictionary<string, object> routeData)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jssStr = jss.Serialize(_content);
            context.Response.Write(jssStr);
            context.Response.ContentType = _contentType;
        }
    }
}
