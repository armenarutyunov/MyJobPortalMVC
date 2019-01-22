using MVC_CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CORE.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {

            return View();

        }
       
        public ActionResult Error()
        {
            

            return View();

        }
        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Login,Password")] Admin admin)
        {
            if (ModelState.IsValid) 
            {

                admin.FullName = "0";
                if (admin.Login == "Admin" && admin.Password == "Admin")
                {
                    admin.FullName = "1"; 
                    return RedirectToAction("Index");
                }
            }
         
            return RedirectToAction("Error");
           
           
        }
    }
    
   
}