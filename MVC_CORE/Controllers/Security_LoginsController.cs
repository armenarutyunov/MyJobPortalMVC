using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using CareerCloud.EntityFrameworkDataAccess;
using DAL;

namespace MVC_CORE.Controllers
{
    public class Security_LoginsController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Security_Logins> repo = new EFGenericRepository<Security_Logins>(false);
        private Security_Logins_Logic logic;
        private System_Language_Codes_Logic logic_lc;
        public Security_LoginsController()
        {
            var repo_lc = new EFGenericRepository<System_Language_Codes>(false);
            logic = new Security_Logins_Logic(repo);
            logic_lc = new System_Language_Codes_Logic(repo_lc);
        }
        // GET: Security_Logins
        public ActionResult Index()
        {
            Security_Logins applicant =(Security_Logins)TempData["Applicant"];
            TempData["Applicant"] = applicant;
            return View(applicant);
        }
        public ActionResult Error()
        {
            
            return View();
        }
        public ActionResult Message()
        {
            ViewBag.Reason = (string)TempData["Msg"];
            return View();
        } 

        // GET: Security_Logins/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security_Logins security_Logins = logic.Get((Guid)id);// db.Security_Logins.Find(id);
            if (security_Logins == null)
            {
                return HttpNotFound();
            }
            return View(security_Logins);
        }

        // GET: Security_Logins/Create
        public ActionResult Create()
        {
            
            ViewBag.Prefferred_Language = new SelectList(logic_lc.GetAll(), "LanguageID", "LanguageID");
            return View();
        }

        // POST: Security_Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Login,Password, Email_Address, Phone_Number,Full_Name,Prefferred_Language")]
        Security_Logins SL)
        {
            if (ModelState.IsValid)
            {
               
                Security_Logins sl = new Security_Logins(); 
                sl.Id = Guid.NewGuid(); sl.Login = SL.Login; sl.Password = SL.Password; sl.Created_Date = DateTime.Now;
                sl.Is_Inactive = false; sl.Is_Locked = false; sl.Prefferred_Language = SL.Prefferred_Language;
                sl.Email_Address = SL.Email_Address; sl.Phone_Number = SL.Phone_Number; sl.Full_Name = SL.Full_Name;
                TempData["Applicant"] = sl;
                logic.Add(new Security_Logins[] { sl });
                
                return RedirectToAction("Index");
            }
            ViewBag.Reason = "Soory, we have some troubles, Try Later!";
            return View("Error");
        }

        // GET: Security_Logins/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security_Logins security_Logins = logic.Get((Guid)id); //db.Security_Logins.Find(id);
            if (security_Logins == null)
            {
                return HttpNotFound();
            }
            return View(security_Logins);
        }

        // POST: Security_Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,Created_Date,Password_Update_Date,Agreement_Accepted_Date,Is_Locked,Is_Inactive,Email_Address,Phone_Number,Full_Name,Force_Change_Password,Prefferred_Language,Time_Stamp")]
        Security_Logins security_Logins)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(security_Logins).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Security_Logins[] { security_Logins });

                return RedirectToAction("Index");
            }
            return View(security_Logins);
        }

        // GET: Security_Logins/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security_Logins security_Logins = logic.Get((Guid)id); //db.Security_Logins.Find(id);
            if (security_Logins == null)
            {
                return HttpNotFound();
            }
            return View(security_Logins);
        }

        // POST: Security_Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Security_Logins security_Logins = logic.Get((Guid)id); //db.Security_Logins.Find(id);
            logic.Delete(new Security_Logins[] { security_Logins });
            //db.Security_Logins.Remove(security_Logins);
            //db.SaveChanges();
            TempData["Msg"] = "Your Account Have Been Deleted!";
            return RedirectToAction("Message");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo._context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
