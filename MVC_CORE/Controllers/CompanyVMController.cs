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
    public class CompanyVMController : Controller
    {

        public CompanyVM profile;
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Company_Profiles> repo_cp = new EFGenericRepository<Company_Profiles>(false);
        private Company_Profiles_Logic logic_cp;
        private System_Language_Codes_Logic logic_lc;
        private Company_Descriptions_Logic logic_cd;
        private Company_Locations_Logic logic_cl;
        public CompanyVMController()
        {
            TempData["ProfileID"] = Guid.NewGuid();
            var repo_cd = new EFGenericRepository<Company_Descriptions>(false);
            var repo_lc = new EFGenericRepository<System_Language_Codes>(false);
            var repo_cl = new EFGenericRepository<Company_Locations>(false);
            logic_cp = new Company_Profiles_Logic(repo_cp);
            logic_cd = new Company_Descriptions_Logic(repo_cd);
            logic_lc = new System_Language_Codes_Logic(repo_lc);
            logic_cl = new Company_Locations_Logic(repo_cl);
        }
        public ActionResult Index()
        {
           
            profile = (CompanyVM)TempData["Profile"];
            return View(profile);
           
        }

       

        // GET: Company_Descriptions/Create
        public ActionResult Create()
        {
            //List<string> Countries = new List<string>() {"USA","MEX","CAN","RUS","VEN","BRA", "ARG","CHL" };
            //String[] Provinces = new string[] { "ON", "MO", "MI", "DG", "KH" };

            //Dictionary<string, string[]> DWA = new Dictionary<string, string[]>();
            //DWA["USA"] = new string[] { "US1", "US2", "US3", "US4", "US5" };
            //DWA["MEX"] = new string[] { "ME1", "ME2", "ME3", "ME4", "ME5" };
            //DWA["CAN"] = new string[] { "CA1", "CA2", "CA3", "CA4", "CA5" };
            //string[] Countries = new string[] { "USA", "MEX", "CAN" };
            //Country[] Countries = new Country[] {
            //new Country { id = 1, Country_Name = "USA", Country_Code = "USA" },
            //new Country { id = 2, Country_Name = "Canada", Country_Code = "CAN" },
            //new Country { id = 1, Country_Name = "Mexico", Country_Code = "MEX" }};
            //string sel;
            //ViewBag.Company = new SelectList(db.Company_Profiles, "Id", "Company_Website");
            //ViewBag.LanguageID = new SelectList(db.System_Language_Codes, "LanguageID", "Name");

            //string s;
            ViewBag.LanguageID = new SelectList(logic_lc.GetAll(), "LanguageID", "Native_Name");

            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Add(new SelectListItem { Text = "Please, Select a Country   ", Value = "Country" });
            countries.Add(new SelectListItem { Text = "America", Value = "USA" });
            countries.Add(new SelectListItem { Text = "Canada", Value = "CAN" });
            countries.Add(new SelectListItem { Text = "Mexico", Value = "MEX" });
            //ViewData["Country"] = countries;
            ViewBag.Country = countries;
            ViewBag.Province = new List<SelectListItem>();
            ViewBag.City = new List<SelectListItem>();

            return View();
        }

        // POST: Company_Descriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyName,CompanyDescription,LanguageId," +
        "CompanyWebsite,ContactPhone,ContactName, Country, Province, City, Street, Postal_Code ")]
        CompanyVM CVM)
        {
            if (ModelState.IsValid)
            {
               //----------Company Profeles-------
                Company_Profiles cp = new Company_Profiles();
                cp.Id = Guid.NewGuid(); cp.Registration_Date = DateTime.Now;
                cp.Contact_Phone = CVM.ContactPhone; cp.Contact_Name = CVM.ContactName; cp.Company_Website = CVM.CompanyWebsite;
                logic_cp.Add(new Company_Profiles[] { cp });
                //----------Company Descriptions-------
                Company_Descriptions cd = new Company_Descriptions();
                cd.Id = Guid.NewGuid(); cd.Company = cp.Id; cd.LanguageID = CVM.LanguageId;
                cd.Company_Name = CVM.CompanyName; cd.Company_Description = CVM.CompanyDescription;
                logic_cd.Add(new Company_Descriptions[] { cd });
                //----------Company Locations-------
                Company_Locations cl = new Company_Locations();
                cl.Id= Guid.NewGuid(); cl.Company = cp.Id; cl.Country_Code = CVM.Country;
                cl.State_Province_Code = CVM.Province; cl.Street_Address = CVM.Street;
                cl.City_Town = CVM.City; cl.Zip_Postal_Code = CVM.Postal_Code;
                logic_cl.Add(new Company_Locations[] { cl });
               
                CVM.Id = Guid.NewGuid();
                TempData["Profile"] = CVM;
                TempData["ProfileID"] = cp.Id;

                return RedirectToAction("Index");
            }

            //ViewBag.Company = new SelectList(db.Company_Profiles, "Id", "Company_Website", company_Descriptions.Company);
            //ViewBag.LanguageID = new SelectList(db.System_Language_Codes, "LanguageID", "Name", company_Descriptions.LanguageID);
            return View();
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