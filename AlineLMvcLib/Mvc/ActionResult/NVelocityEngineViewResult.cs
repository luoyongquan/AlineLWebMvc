using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System.IO;

namespace AlineLMvcLib.Mvc
{
    class NVelocityViewResult : ActionResult
    {
        public object _model { get; set; }
        public string _contentType { get; set; } = "text/html";
        public NVelocityViewResult(object model)
        {
            this._model = model;
        }

        public override void Execute(HttpContext context, IDictionary<string, object> routeData)
        {   //创建NVelocity模板引擎
            VelocityEngine vltEngine = new VelocityEngine();
            //设置模板为文件类型
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            //设置模板所在路径
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, HttpContext.Current.Server.MapPath(string.Format("~/Views/{0}/", routeData["controller"].ToString())));
            vltEngine.SetProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            vltEngine.SetProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");
            //初始化模板引擎
            vltEngine.Init();
            VelocityContext vltContext = new VelocityContext();
            vltContext.Put("Model", this._model);
            //获得具体执行的模板文件
            Template vltTemplate = vltEngine.GetTemplate(string.Format("{0}.html", routeData["action"].ToString()));
            // 定义一个字符串输出流
            StringWriter vltWriter = new StringWriter();
            // 根据模板的上下文，将模板生成的内容写进刚才定义的字符串输出流中
            vltTemplate.Merge(vltContext, vltWriter);
            // 输出字符串流中的数据
            context.Response.Write(vltWriter.GetStringBuilder().ToString());
            vltWriter.Close();
            vltWriter.Dispose();
            context.Response.ContentType = this._contentType;
        }

    }
}
