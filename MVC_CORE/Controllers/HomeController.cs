using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CORE.Controllers
{
    public class HomeController : Controller
    {
        public bool r;
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "MyJobPortal Works From May of 2018 year";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our Main Office Is Located At 265 Humber College Blvd";

            return View();
        }
    }
}