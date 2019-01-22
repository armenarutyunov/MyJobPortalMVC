using BusinessLogic;
using CareerCloud.EntityFrameworkDataAccess;
using DAL;
using MVC_CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Data;

namespace MVC_CORE.Controllers
{
    public class Applicant_LoginController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        private EFGenericRepository<Security_Logins> repo_sl = new EFGenericRepository<Security_Logins>(false);
        private EFGenericRepository<Applicant_Profiles> repo_ap = new EFGenericRepository<Applicant_Profiles>(false);
        private EFGenericRepository<Applicant_Educations> repo_ae = new EFGenericRepository<Applicant_Educations>(false);
        private EFGenericRepository<Applicant_Work_History> repo_awh = new EFGenericRepository<Applicant_Work_History>(false);
        private EFGenericRepository<Applicant_Job_Applications> repo_aja = new EFGenericRepository<Applicant_Job_Applications>(false);
        private EFGenericRepository<Applicant_Resumes> repo_ar = new EFGenericRepository<Applicant_Resumes>(false);
        private EFGenericRepository<Applicant_Skills> repo_as = new EFGenericRepository<Applicant_Skills>(false);
        private EFGenericRepository<Security_Logins_Log> repo_sll = new EFGenericRepository<Security_Logins_Log>(false);
        private EFGenericRepository<Security_Logins_Roles> repo_slr = new EFGenericRepository<Security_Logins_Roles>(false);

        private Security_Logins_Logic logic_sl;
        private Applicant_Profiles_Logic logic_ap;
        private Applicant_Educations_Logic logic_ae;
        private Applicant_Work_History_Logic logic_awh;
        private Applicant_Job_Applications_Logic logic_aja;
        private Applicant_Resumes_Logic logic_ar;
        private Applicant_Skills_Logic logic_as;
        private Security_Logins_Log_Logic logic_sll;
        private Security_Logins_Roles_Logic logic_slr;

        public Applicant_LoginController()
        {
            logic_sl = new Security_Logins_Logic(repo_sl);
            logic_ap = new Applicant_Profiles_Logic(repo_ap);
            logic_ae = new Applicant_Educations_Logic(repo_ae);
            logic_awh = new Applicant_Work_History_Logic(repo_awh);
            logic_aja = new Applicant_Job_Applications_Logic(repo_aja);
            logic_ar = new Applicant_Resumes_Logic(repo_ar);
            logic_as = new Applicant_Skills_Logic(repo_as);
            logic_sll = new Security_Logins_Log_Logic(repo_sll);
            logic_slr = new Security_Logins_Roles_Logic(repo_slr);

        }
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult Details()
        {
            var id = (Guid)TempData["ApplicantID"];
            TempData["ApplicantID"] = id;
            Security_Logins sl = logic_sl.GetList(a => a.Id == id).First();
            return View(sl);

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
                var loglist = logic_sl.GetAll();
                foreach(var item in loglist)
                {
                    if (admin.Login == item.Login && admin.Password == item.Password)
                    {
                        TempData["ApplicantID"] = item.Id;
                        TempData["Applicant"] = item;
                        return RedirectToAction("Index");
                    }
                }
            }
                
            return RedirectToAction("Error");
        }
        public ActionResult Delete()
        {

            Security_Logins applicant = (Security_Logins)TempData["Applicant"];

            TempData["Applicant"] = applicant;


            return View(applicant);

        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            Guid SLID = (Guid)TempData["ApplicantID"];
            if (logic_ap.GetList(a => a.Login == SLID).Count==0)
            {
                //---------Security_Logins_Log-------------------
                Security_Logins_Log[] _sll = logic_sll.GetList(a => a.Login == SLID).ToArray();
                logic_sll.Delete(_sll);
                //---------Security_Logins_Roles-------------------
                Security_Logins_Roles[] _slr = logic_slr.GetList(a => a.Login == SLID).ToArray();
                logic_slr.Delete(_slr);
                Security_Logins[] _sl = logic_sl.GetList(a => a.Id == SLID).ToArray();
                logic_sl.Delete(_sl);
                TempData["Msg"] = "Your Profile Has Been Deleted! We Hope To See You Again!";
                TempData["FromAction"] = 14;
                return RedirectToAction("Message", "Security_Logins");
            }

            Guid applicant = logic_ap.GetList(a => a.Login == SLID).First().Id;
            //---------Applicant Educations-----------
            Applicant_Educations[] ae = logic_ae.GetList(a => a.Applicant == applicant).ToArray();
            logic_ae.Delete(ae);
            //---------Applicant_Work_History--------------
            Applicant_Work_History[] awh = logic_awh.GetList(a => a.Applicant == applicant).ToArray();
            logic_awh.Delete(awh);
            //---------Applicant_Resumes-------------------
            Applicant_Resumes[] ar = logic_ar.GetList(a => a.Applicant == applicant).ToArray();
            logic_ar.Delete(ar);
            //---------Applicant_Job_Applications--------------
            Applicant_Job_Applications[] aja = logic_aja.GetList(a => a.Applicant == applicant).ToArray();
            logic_aja.Delete(aja);
            //---------Applicant_Skills-------------------
            Applicant_Skills[] _as = logic_as.GetList(a => a.Applicant == applicant).ToArray();
            logic_as.Delete(_as);
            //---------Applicant_Profiles-------------------
            Applicant_Profiles[] ap = logic_ap.GetList(a => a.Id == applicant).ToArray();
            logic_ap.Delete(ap);
            //---------Security_Logins_Log-------------------
            Security_Logins_Log[] sll = logic_sll.GetList(a => a.Login == SLID).ToArray();
            logic_sll.Delete(sll);
            //---------Security_Logins_Roles-------------------
            Security_Logins_Roles[] slr = logic_slr.GetList(a => a.Login == SLID).ToArray();
            logic_slr.Delete(slr);
            //---------Security_Logins-------------------
            Security_Logins[] sl = logic_sl.GetList(a => a.Id == SLID).ToArray();
            logic_sl.Delete(sl);
            //------------------All Deleted-----------------

          
            TempData["Msg"] = "Your Profile Has Been Deleted! We Hope To See You Again!";
            TempData["FromAction"] = 14;
            return RedirectToAction("Message", "Security_Logins");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo_sl._context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
