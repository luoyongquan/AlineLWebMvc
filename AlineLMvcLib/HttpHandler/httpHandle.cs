using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlineLMvcLib.Mvc;

namespace AlineLMvcLib.HttpHandler
{
    public class MvcHandle : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private IDictionary<string, object> _routeData;
        public MvcHandle(IDictionary<string, object> routeData)
        {
            _routeData = routeData;
        }
        public bool IsReusable { get; }
        public void ProcessRequest(HttpContext context)
        {

            string controllerName = _routeData["controller"].ToString();
            //生成控制器实例
            IController controller = ControllerFactory.CreateController(controllerName);
            if (controller == null)
            {//控制器不存在
                throw new HttpException(404, "Not Found");
            }
            //执行具体的方法
            ActionResult result = controller.Execute(context, this._routeData);
            result.Execute(context, this._routeData);
        }
    }

}