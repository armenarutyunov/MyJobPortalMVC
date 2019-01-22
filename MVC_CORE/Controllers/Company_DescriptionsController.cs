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
    public class Company_DescriptionsController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Company_Descriptions> repo = new EFGenericRepository<Company_Descriptions>(false);

        private Company_Descriptions_Logic logic;
        private Company_Profiles_Logic logic_cp;
        private System_Language_Codes_Logic logic_lc;
       
        public Company_DescriptionsController()
        {
            //TempData["ProfileID"] = Guid.NewGuid();
            var repo_cp = new EFGenericRepository<Company_Profiles>(false);
            var repo_lc = new EFGenericRepository<System_Language_Codes>(false);
            logic_cp = new Company_Profiles_Logic(repo_cp);
            logic = new Company_Descriptions_Logic(repo);
            logic_lc = new System_Language_Codes_Logic(repo_lc);
        }
       
        // GET: Company_Descriptions
        public ActionResult Index()
        {
            var list = logic.GetAll();
            return View(list);
            //var company_Descriptions = db.Company_Descriptions.Include(c => c.Company_Profiles).Include(c => c.System_Language_Codes);
            //return View(company_Descriptions.ToList());
        }

        // GET: Company_Descriptions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Descriptions company_Descriptions = logic.Get((Guid)id);//db.Company_Descriptions.Find(id);
            if (company_Descriptions == null)
            {
                return HttpNotFound();
            }
            return View(company_Descriptions);
        }

        // GET: Company_Descriptions/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(logic_cp.GetAll().ToList(), "Id", "Company_Website");
            ViewBag.LanguageID = new SelectList(logic_lc.GetAll().ToList(), "LanguageID", "Name");
            return View();
        }

        // POST: Company_Descriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Company,LanguageID,Company_Name,Company_Description,Time_Stamp")]
        Company_Descriptions company_Descriptions)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Company_Descriptions[] { company_Descriptions });
                //company_Descriptions.Id = Guid.NewGuid();
                //db.Company_Descriptions.Add(company_Descriptions);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(logic_cp.GetAll().ToList(), "Id", "Company_Website", company_Descriptions.Company);
            ViewBag.LanguageID = new SelectList(logic_lc.GetAll().ToList(), "LanguageID", "Name", company_Descriptions.LanguageID);
            return View(company_Descriptions);
        }
       
        // GET: Company_Descriptions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Descriptions company_Descriptions = logic.Get((Guid)id);//db.Company_Descriptions.Find(id);
            if (company_Descriptions == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(logic_cp.GetAll().ToList(), "Id", "Company_Website", company_Descriptions.Company);
            ViewBag.LanguageID = new SelectList(logic_lc.GetAll().ToList(), "LanguageID", "Name", company_Descriptions.LanguageID);
            return View(company_Descriptions);
        }

        // POST: Company_Descriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Company,LanguageID,Company_Name,Company_Description,Time_Stamp")]
        Company_Descriptions company_Descriptions)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(company_Descriptions).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Company_Descriptions[] { company_Descriptions });
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(logic_cp.GetAll().ToList(), "Id", "Company_Website", company_Descriptions.Company);
            ViewBag.LanguageID = new SelectList(logic_lc.GetAll().ToList(), "LanguageID", "Name", company_Descriptions.LanguageID);
            return View(company_Descriptions);
        }

        // GET: Company_Descriptions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Descriptions company_Descriptions = logic.Get((Guid)id); //db.Company_Descriptions.Find(id);
            if (company_Descriptions == null)
            {
                return HttpNotFound();
            }
            return View(company_Descriptions);
        }

        // POST: Company_Descriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company_Descriptions company_Descriptions = logic.Get((Guid)id);//db.Company_Descriptions.Find(id);
            logic.Delete(new Company_Descriptions[] { company_Descriptions });
            //db.Company_Descriptions.Remove(company_Descriptions);
            //db.SaveChanges();
            return RedirectToAction("Index");
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
