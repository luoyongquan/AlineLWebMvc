using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;

namespace AlineLMvcLib.Mvc
{
    public class ControllerBase : IController
    {
        public virtual ActionResult Execute(HttpContext context, IDictionary<string, object> routeData)
        {
            string actionName = routeData["action"].ToString();
            if (string.IsNullOrEmpty(actionName))
            {//路由中缺失方法
                throw new HttpException(404, "Not Found");
            }
            //获取当前控制器中全部方法, 公开,指定实例,当前控制器中声明的方法
            MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            MethodInfo method = null;
            foreach (MethodInfo item in methods)
            {//忽略大小写比较方法名
                if (item.Name.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))
                {
                    method = item;
                    break;
                }
            }
            if (method == null)
            {//方法不存在
                throw new HttpException(404, "Not Found");
            }
            List<object> values = new List<object>();
            foreach (ParameterInfo item in method.GetParameters())
            {
                string parameterName = item.Name;
                Type parameterType = item.ParameterType;
                //从querstring form表单中取值
                string value = context.Request[parameterName];
                if (string.IsNullOrEmpty(value))
                {//从routedata中取值
                    if (routeData.ContainsKey(parameterName))
                    {
                        value = routeData[parameterName].ToString();
                    }
                }
                if (string.IsNullOrEmpty(value))
                {
                    values.Add(null);
                }
                else
                {//转换值类型
                    values.Add(Convert.ChangeType(value, parameterType));
                }
            }
            return method.Invoke(this, values.ToArray()) as ActionResult;
        }
        protected virtual ActionResult View(object model)
        {
            return new RazorEngineViewJson(model);
        }
        protected virtual ActionResult View()
        {
            return new RazorEngineViewJson(null);
        }
    }
}
