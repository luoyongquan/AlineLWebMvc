using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AlineLMvcLib.Mvc
{
    public interface IController
    {
        ActionResult Execute(HttpContext context, IDictionary<string, object> routeData);
    }
}
