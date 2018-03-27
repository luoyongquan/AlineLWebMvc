using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AlineLMvcLib.Mvc
{
    public class ActionResult : IActionResult
    {
        public virtual void Execute(HttpContext context, IDictionary<string, object> routeData)
        {
            throw new HttpException(500, "The Method is NULL of Execute");
        }
    }
}
