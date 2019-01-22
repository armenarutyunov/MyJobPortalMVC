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
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;

namespace MVC_CORE.Controllers
{
    
    public class Company_LoginController : Controller
    {
        
        public static Guid CompanyID;
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Company_Profiles> repo_cp = new EFGenericRepository<Company_Profiles>(false);
        EFGenericRepository<Company_Descriptions> repo_cd = new EFGenericRepository<Company_Descriptions>(false);
        private Company_Profiles_Logic logic_cp;
        private Company_Locations_Logic logic_cl;
        private System_Language_Codes_Logic logic_lc;
        private Company_Descriptions_Logic logic_cd;
        IDataRepository<Company_Jobs> repo_cj = new EFGenericRepository<Company_Jobs>(false); 
        IDataRepository<Company_Jobs_Descriptions> repo_jd = new EFGenericRepository<Company_Jobs_Descriptions>(false); 
        IDataRepository<Company_Job_Educations> repo_je = new EFGenericRepository<Company_Job_Educations>(false); 
        IDataRepository<Company_Job_Skills> repo_js = new EFGenericRepository<Company_Job_Skills>(false); 
        IDataRepository<Applicant_Job_Applications> repo_ja = new EFGenericRepository<Applicant_Job_Applications>(false); 
        IDataRepository<Company_Locations> repo_cl = new EFGenericRepository<Company_Locations>(false);
        public bool r;

        public Company_LoginController()
        {
            //TempData["ProfileID"] = Guid.NewGuid();
            var repo_lc = new EFGenericRepository<System_Language_Codes>(false);
            logic_cp = new Company_Profiles_Logic(repo_cp);
            logic_cd = new Company_Descriptions_Logic(repo_cd);
            logic_lc = new System_Language_Codes_Logic(repo_lc);
            logic_cl = new Company_Locations_Logic(repo_cl);
            r = false;
            //Session["IsSignedIn"] = false;
        }
        public ActionResult Index()
        {
            
           
            return View();

        }
        public ActionResult Details()
        {


            return View();

        }
        public ActionResult CvmPlayer()
        {

            Guid _ProfileID = (Guid)TempData["ProfileID"];
            var cd = logic_cd.GetAll().Where(a => a.Company == _ProfileID).First();
            var cl = logic_cl.GetAll().Where(a => a.Company == _ProfileID).First();

            if (cd != null)
            {
                CompanyVM cvm = new CompanyVM();
                cvm.CompanyDescription = cd.Company_Description; cvm.CompanyName = cd.Company_Name;
                cvm.CompanyWebsite = cd.Company_Profiles.Company_Website; cvm.ContactName = cd.Company_Profiles.Contact_Name;
                cvm.ContactPhone = cd.Company_Profiles.Contact_Phone; cvm.LanguageId = cd.System_Language_Codes.LanguageID;
                cvm.RegistrationDate = cd.Company_Profiles.Registration_Date; cvm.Id = Guid.NewGuid();
                cvm.Country = cl.Country_Code; cvm.Province = cl.State_Province_Code; cvm.City = cl.City_Town;
                cvm.Street = cl.Street_Address; cvm.Postal_Code = cl.Zip_Postal_Code;
               
                TempData["Profile"] = cvm;
                TempData["ProfileID"] = _ProfileID;
                return View(cvm);
            }
            ViewBag.Reason = "There Is Nothing To Display! You Should Create Profile!";
            return RedirectToAction("Error");
           
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
                Session["IsSignedIn"] = false;
                ViewBag.HasProfile = false;
                var loglist = logic_cp.GetAll(); //db.Security_Logins.ToList();

                foreach (var item in loglist)
                {
                    if (admin.Login == item.Company_Website && admin.Password == item.Contact_Phone)
                    {
                        //TempData["ProfileID"] = item.Id;
                        //ViewData["ProfileID"] = (Guid)item.Id;
                        Session["ProfileID"] = item.Id;
                        Session["IsSignedIn"] = true;
                        ViewBag.HasProfile = true;
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Error");
            }

            return RedirectToAction("Error");

        }
       
        public ActionResult Edit()
        {
            
            CompanyVM cvm = (CompanyVM)TempData["Profile"];
            cvm.LanguageId = null;
            ViewBag.LanguageID = new SelectList(logic_lc.GetAll(), "LanguageID", "LanguageID");
            return View(cvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyName,CompanyDescription,LanguageId," +
        "CompanyWebsite,ContactPhone,ContactName, Country, Province, City, Street, Postal_Code ")]
        CompanyVM CVM)
        {



            //Company_Descriptions cd = logic_cd.GetAll().Where(a => a.Company == _ProfileID).First() as Company_Descriptions;
            Guid _ProfileID = (Guid)TempData["ProfileID"];
            Company_Descriptions cd = logic_cd.GetList(a => a.Company == _ProfileID).FirstOrDefault();
            Company_Locations cl = logic_cl.GetList(a => a.Company == _ProfileID).FirstOrDefault();
            Company_Profiles cp = logic_cp.Get(_ProfileID);

           
            if (ModelState.IsValid)
            {
           
                //-----Company Profile------------------

                cp.Contact_Phone = CVM.ContactPhone; cp.Contact_Name = CVM.ContactName;
                cp.Company_Website = CVM.CompanyWebsite;
                Company_Profiles[] cplist = new Company_Profiles[] { cp };
                logic_cp.Update(cplist);
               
                //----Company Description--------------
                cd.LanguageID = CVM.LanguageId;
                cd.Company = cp.Id;
                cd.Company_Name = CVM.CompanyName; cd.Company_Description = CVM.CompanyDescription; 
                Company_Descriptions[] cdlist = new Company_Descriptions[] { cd };

                //----Company Locations--------------
                cl.Country_Code = CVM.Country; cl.State_Province_Code = CVM.Province; cl.City_Town = CVM.City;
                cl.Street_Address = CVM.Street; cl.Zip_Postal_Code = CVM.Postal_Code;
                Company_Locations[] cllist = new Company_Locations[] { cl };

                //Company_Descriptions cd = db1.Company_Descriptions.Include(Where(a => a.Company == _ProfileID).First();
                //db1.Entry(cd).State = EntityState.Modified;
                //db1.SaveChanges();

                logic_cd.Update(cdlist);
                logic_cl.Update(cllist);
                TempData["Profile"] = CVM;
                TempData["ProfileID"] = _ProfileID;
                return RedirectToAction("CvmPlayer");
            }
            return RedirectToAction("Error");
        }
        public ActionResult Delete()
        {
           
            CompanyVM profile = (CompanyVM)TempData["Profile"];
           
            TempData["Profile"] = profile;
           
            return View(profile);

        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            Guid _ProfileID = (Guid)TempData["ProfileID"];
            //---------Company Descriptions-----------
            Company_Descriptions[] cd = logic_cd.GetList(a => a.Company == _ProfileID).ToArray();
            logic_cd.Delete(cd);
            //---------Company Locations--------------
            Company_Locations[] cl = repo_cl.GetList(a => a.Company == _ProfileID).ToArray();
            repo_cl.Remove(cl);
            //---------Company Jobs-------------------
            Company_Jobs[] cj = repo_cj.GetList(a => a.Company == _ProfileID).ToArray();
            foreach(var item in cj)
            {
                Company_Jobs_Descriptions[] jd = repo_jd.GetList(a => a.Job == item.Id).ToArray();
                repo_jd.Remove(jd);
                Company_Job_Educations[] je = repo_je.GetList(a => a.Job == item.Id).ToArray();
                repo_je.Remove(je);
                Company_Job_Skills[] js = repo_js.GetList(a => a.Job == item.Id).ToArray();
                repo_js.Remove(js);
                Applicant_Job_Applications[] ja = repo_ja.GetList(a => a.Job == item.Id).ToArray();
                repo_ja.Remove(ja);
            }
            repo_cj.Remove(cj);
            //-----------Company Profile----------
            Company_Profiles[] cp = logic_cp.GetList(a => a.Id == _ProfileID).ToArray();
            logic_cp.Delete(cp);

           

            TempData["Msg"] = "Your Profile Has Been Deleted! We Hope To See You Again!";
            TempData["FromAction"] = 12;
            return RedirectToAction("Message", "Security_Logins");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo_cp._context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}