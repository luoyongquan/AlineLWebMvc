using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlineLMvcLib.Mvc;

namespace WebTest.Controller
{
    public class HomeController:ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}