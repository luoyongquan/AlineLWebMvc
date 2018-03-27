using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AlineLMvcLib.Mvc
{
    public class ContentResult : ActionResult
    {
        private string _content { get; set; }
        private string _contentType { get; set; } = "text/plain";
        public ContentResult(string content)
        {
            this._content = content;
        }
        public ContentResult(string content, string contentType)
        {
            this._content = content;
            this._contentType = contentType;
        }
        public override void Execute(HttpContext context, IDictionary<string, object> routeData)
        {
            context.Response.Write(_content);
            context.Response.ContentType = _contentType;
        }
    }
}
