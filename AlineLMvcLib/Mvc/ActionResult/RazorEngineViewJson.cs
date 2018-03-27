using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RazorEngine;
using RazorEngine.Templating;
using System.IO;

namespace AlineLMvcLib.Mvc
{
    /// <summary>
    /// Razor解析视图文件 cshtml
    /// </summary>
    public class RazorEngineViewJson : ActionResult
    {
        public object _content { get; set; }
        public string _contentType { get; set; } = "text/html";
        public RazorEngineViewJson(object content)
        {
            this._content = content;
        }
        public override void Execute(HttpContext context, IDictionary<string, object> routeData)
        {
            var filepath = AppDomain.CurrentDomain.BaseDirectory + @"Views\" + routeData["controller"] + "\\" + routeData["action"] + ".cshtml";
            //使用razor解析模板文件, 使用filepath作为模板缓存的key
            var returnContent = Engine.Razor.RunCompile(File.ReadAllText(filepath), filepath, null, this._content);
            context.Response.Write(returnContent);
            context.Response.ContentType = this._contentType;
        }
    }
}
