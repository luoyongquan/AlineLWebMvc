using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web.Compilation;

namespace AlineLMvcLib.Mvc
{
    public static class ControllerFactory
    {
        /// <summary>
        /// 存放系统初始化得到的控制器
        /// </summary>
        private static readonly List<Type> _controllers;
        static ControllerFactory()
        {
            _controllers = new List<Type>();
            //获取项目中所有的引用
            var Assemblies = BuildManager.GetReferencedAssemblies();
            //获取全部的controll
            foreach (Assembly item in Assemblies)
            {//循环项目中的全部引用
                Type[] types = item.GetTypes();
                foreach (Type type in types)
                {
                    if (typeof(IController).IsAssignableFrom(type) && type.IsClass && type.IsPublic && !type.IsAbstract)
                    {//过滤控制器之外的类
                        _controllers.Add(type);
                    }
                }
            }
        }
        /// <summary>
        /// 创建控制器
        /// </summary>
        /// <param name="controllName">控制器名称</param>
        /// <returns></returns>
        public static IController CreateController(string controllName)
        {
            IController controller = null;
            foreach (Type type in _controllers)
            {//循环初始化时加载的控制器
                if (type.Name.Equals(controllName + "Controller", StringComparison.InvariantCultureIgnoreCase))
                {//匹配控制器
                    controller = Activator.CreateInstance(type) as IController;
                    break;
                }
            }
            return controller;
        }
    }
}
