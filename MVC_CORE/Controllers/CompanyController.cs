using BusinessLogic;
using CareerCloud.EntityFrameworkDataAccess;
using DAL;
using MVC_CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CORE.Controllers
{
    public class CompanyController : Controller
    {
        EFGenericRepository<Company_Profiles> repo_cp = new EFGenericRepository<Company_Profiles>(false);
        EFGenericRepository<Company_Descriptions> repo_cd = new EFGenericRepository<Company_Descriptions>(false);
        EFGenericRepository<System_Language_Codes> repo_lc = new EFGenericRepository<System_Language_Codes>(false);
        EFGenericRepository<Company_Locations> repo_cl = new EFGenericRepository<Company_Locations>(false);
        private Company_Profiles_Logic logic_cp;
        private System_Language_Codes_Logic logic_lc;
        private Company_Descriptions_Logic logic_cd;
        private Company_Locations_Logic logic_cl;
        public int d;
        public int f;
        public bool r = false;
        public List<SelectListItem> countries = new List<SelectListItem>();
        public CompanyController()
        {
           
            logic_cp = new Company_Profiles_Logic(repo_cp);
            logic_cd = new Company_Descriptions_Logic(repo_cd);
            logic_lc = new System_Language_Codes_Logic(repo_lc);
            logic_cl = new Company_Locations_Logic(repo_cl);

           
            //countries.Add(new SelectListItem { Text = "--Please, Select a Country--", Value = "Country" });
            countries.Add(new SelectListItem { Text = "America", Value = "USA" });
            countries.Add(new SelectListItem { Text = "Canada", Value = "CAN" });
            countries.Add(new SelectListItem { Text = "Mexico", Value = "MEX" });

           
            ViewBag.Country = countries;
            //ViewBag.Location.Country_Code = countries;
            ViewData["Location.Country_Code"] = countries;
            ViewData["Location.State_Province_Code"] = new List<SelectListItem>();
            ViewData["Location.City_Town"] = new List<SelectListItem>();
            ViewBag.Province = new List<SelectListItem>();
            ViewBag.City = new List<SelectListItem>();
        }
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateEdit(bool r)
        {
            ViewBag.HasProfile = r;
            Session["IsSignedIn"] = r;
            Company c = new Company();
            if (r)
            {
            Guid _ProfileID = (Guid)Session["ProfileID"];
           
            var cde = logic_cd.GetList(a => a.Company == _ProfileID && a.LanguageID == "EN").FirstOrDefault();
            var cdd = logic_cd.GetList(a => a.Company == _ProfileID && a.LanguageID=="DE").FirstOrDefault();
            var cdf = logic_cd.GetList(a => a.Company == _ProfileID && a.LanguageID=="FR").FirstOrDefault();
            var cl_list = logic_cl.GetList(a => a.Company == _ProfileID);
            c.Id = _ProfileID;
            c.EnName.Company_Name = cde.Company_Name; c.EnName.Company_Description = cde.Company_Description;
            c.Profile.Company_Website = cde.Company_Profiles.Company_Website; c.Profile.Contact_Name = cde.Company_Profiles.Contact_Name;
            c.Profile.Contact_Phone = cde.Company_Profiles.Contact_Phone; c.Profile.Registration_Date = cde.Company_Profiles.Registration_Date;

            if (cdd != null) { c.DeName.Id = cdd.Id; c.DeName.Company_Name = cdd.Company_Name; c.DeName.Company_Description = cdd.Company_Description; }
            if (cdf != null) { c.FrName.Id = cdf.Id; c.FrName.Company_Name = cdf.Company_Name; c.FrName.Company_Description = cdf.Company_Description; }
            c.Locations = new List<Company_Locations>();
            foreach (var item in cl_list)
                {
                    c.Locations.Add(item);
                }
            }
            Session["Company"] = c;
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit([Bind(Include = "EnName,Profile")]
        Company s)
        {
            Company c = new Company();
            Company_Profiles cp;
            Company_Descriptions cd;
            if ((bool)Session["IsSignedIn"] == false)
            {
                cp = new Company_Profiles(); cp.Id = Guid.NewGuid();
                cd = new Company_Descriptions(); cd.Id = Guid.NewGuid(); cd.LanguageID = "EN"; cd.Company = cp.Id;
                //---------- Company Profile Record -------------------
                cp.Company_Website = s.Profile.Company_Website;
                cp.Contact_Name = s.Profile.Contact_Name; cp.Contact_Phone = s.Profile.Contact_Phone;
                //---------- Company Description Record ---------------
                cd.Company_Name = s.EnName.Company_Name; cd.Company_Description = s.EnName.Company_Description;
                //---------- Company ----------------------------------
                c.Profile = cp; c.EnName = cd; c.Id = cp.Id;
                logic_cp.Add(new Company_Profiles[] { cp });
                logic_cd.Add(new Company_Descriptions[] { cd });
            }
            else
            {
                Guid _ProfileID = (Guid)Session["ProfileID"];
                cp = logic_cp.GetList(a => a.Id == _ProfileID).FirstOrDefault();
                cd = logic_cd.GetList(a => a.Company == _ProfileID && a.LanguageID == "EN").FirstOrDefault();
                //---------- Company Profile Record -------------------
                cp.Company_Website = s.Profile.Company_Website;
                cp.Contact_Name = s.Profile.Contact_Name; cp.Contact_Phone = s.Profile.Contact_Phone;
                //---------- Company Description Record ---------------
                cd.Company_Name = s.EnName.Company_Name; cd.Company_Description = s.EnName.Company_Description;
                //---------- Company ----------------------------------
                c.Profile = cp; c.EnName = cd; c.Id = cp.Id;
                //---------- Session ----------------------------------
                logic_cp.Update(new Company_Profiles[] { cp });
                logic_cd.Update(new Company_Descriptions[] { cd });
            }
            ViewBag.HasProfile = true;
            Session["IsSignedIn"] = true;
            Session["ProfileID"] = cp.Id;
            Session["Company"] = c;
            return RedirectToAction("CreateEdit", new { r = true });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeNameCE([Bind(Include = "DeName")]
        Company s)
        {
            Company c = (Company)Session["Company"];
            Guid _ProfileID = (Guid)Session["ProfileID"]; 
            Company_Descriptions cdd;
            IList<Company_Descriptions> cdd_list = logic_cd.GetList(a => a.Company == _ProfileID && a.LanguageID == "DE");
            if (cdd_list.Count!= 0)
            {
                cdd = cdd_list.First();
                cdd.Company_Name= s.DeName.Company_Name; cdd.Company_Description= s.DeName.Company_Description;
                logic_cd.Update(new Company_Descriptions[] { cdd });
               
            }
            else
            {
                cdd = new Company_Descriptions();
                cdd.Id = Guid.NewGuid(); cdd.Company_Name = s.DeName.Company_Name; cdd.LanguageID = "DE";
                cdd.Company_Description = s.DeName.Company_Description; cdd.Company = _ProfileID;
                logic_cd.Add(new Company_Descriptions[] { cdd });
            }
            c.DeName = cdd;
            Session["Company"] = c;
            return RedirectToAction("CreateEdit", new {r=true });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FrNameCE([Bind(Include = "FrName")]
        Company s)
        {
            Company c = (Company)Session["Company"];
            Guid _ProfileID = (Guid)Session["ProfileID"];
            Company_Descriptions cdf;
            IList<Company_Descriptions> cdf_list = logic_cd.GetList(a => a.Company == _ProfileID && a.LanguageID == "FR");

            if (cdf_list.Count != 0)
            {
                cdf = cdf_list.First();
                cdf.Company_Name = s.FrName.Company_Name; cdf.Company_Description = s.FrName.Company_Description;
                logic_cd.Update(new Company_Descriptions[] { cdf });

            }
            else
            {
                cdf = new Company_Descriptions();
                cdf.Id = Guid.NewGuid(); cdf.Company_Name = s.FrName.Company_Name; cdf.LanguageID = "FR";
                cdf.Company_Description = s.FrName.Company_Description; cdf.Company = _ProfileID;
                logic_cd.Add(new Company_Descriptions[] { cdf });
            }
            c.FrName = cdf;
            Session["Company"] = c;
            return RedirectToAction("CreateEdit", new { r = true });
        }
        public ActionResult NameDelete(Guid id)
        {
            //Company c = (Company)Session["Company"];
            IList<Company_Descriptions> cd_list = logic_cd.GetList(a => a.Id == id);
            if (cd_list.Count != 0)
            { 
            logic_cd.Delete(new Company_Descriptions[] { cd_list.First() });
            }
            return RedirectToAction("CreateEdit", new { r = true });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLocation([Bind(Include = "Location")]
        Company s)
        {
            Company c = (Company)Session["Company"];
            Company_Locations cl = new Company_Locations();
            Guid _ProfileID = (Guid)Session["ProfileID"];
            cl.Id = Guid.NewGuid(); cl.Company = _ProfileID; cl.Country_Code = s.Location.Country_Code;
            cl.State_Province_Code = s.Location.State_Province_Code;
            cl.City_Town = s.Location.City_Town; cl.Street_Address = s.Location.Street_Address;
            cl.Zip_Postal_Code = s.Location.Zip_Postal_Code;
            logic_cl.Add(new Company_Locations[] { cl });
            c.Location = cl;
            c.Locations.Add(cl);
            Session["Company"] = c;
            return RedirectToAction("CreateEdit", new { r = true });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLocation(Guid id)
        {
            Company c = (Company)Session["Company"];
            Company_Locations cl = logic_cl.GetList(a => a.Id == id).First();

            c.Locations.Remove(cl);
            c.Location = c.Locations.Last();
            logic_cl.Delete(new Company_Locations[] { cl });
            Session["Company"] = c;
            return RedirectToAction("CreateEdit", new { r = true });
        }
    }
   
}

