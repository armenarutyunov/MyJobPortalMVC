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
    public class Company_Jobs_DescriptionsController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Company_Jobs_Descriptions> repo = new EFGenericRepository<Company_Jobs_Descriptions>(false);
        private Company_Jobs_Descriptions_Logic logic;
        private Company_Jobs_Logic logic_cj;
        public Company_Jobs_DescriptionsController()
        {
            logic = new Company_Jobs_Descriptions_Logic(repo);
            var repo_cj = new EFGenericRepository<Company_Jobs>(false);
            logic_cj = new Company_Jobs_Logic(repo_cj);
        }
        // GET: Company_Jobs_Descriptions
        public ActionResult Index()
        {
            //var company_Jobs_Descriptions = db.Company_Jobs_Descriptions.Include(c => c.Company_Jobs);
            //return View(company_Jobs_Descriptions.ToList());
            var list = logic.GetAll();
            return View(list);
        }
        public ActionResult FindJobs()
        {
            //Company_Jobs_Descriptions cjd = new Company_Jobs_Descriptions();
            //cjd.Job_Name = ""; var list = new List<Company_Jobs_Descriptions>() { cjd };
            //return View(list);
                List<Company_Jobs_Descriptions> list = new List<Company_Jobs_Descriptions>();


            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult FindJobs(/*[Bind(Include = "CJD.First().Job_Name")]*/ List<Company_Jobs_Descriptions> CJD)
        public ActionResult FindJobs(string parameterName)

        {
            if (ModelState.IsValid)
            {
                string s = parameterName;
                List<Company_Jobs_Descriptions> list = new List<Company_Jobs_Descriptions>();
                list = logic.GetList(a => a.Job_Name.Contains(s)).ToList();
                return View(list);
            }

            //ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Jobs_Descriptions.Job);
            return View(parameterName);
        }
        public ActionResult ShowJobs(string s)
        {

            List<Company_Jobs_Descriptions> list = new List<Company_Jobs_Descriptions>();
            list = logic.GetList(a => a.Job_Name.Contains(s)).ToList();
            if (Request.IsAjaxRequest())
            {
                
                return PartialView(list);
            }

            //list = logic.GetAll().ToList();

            //list = logic.GetList(a=>a.Job_Name.Contains(s)).ToList();
            return View(list);
        }

        // GET: Company_Jobs_Descriptions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Jobs_Descriptions company_Jobs_Descriptions = logic.Get((Guid)id); //db.Company_Jobs_Descriptions.Find(id);
            if (company_Jobs_Descriptions == null)
            {
                return HttpNotFound();
            }
            return View(company_Jobs_Descriptions);
        }

        // GET: Company_Jobs_Descriptions/Create
        public ActionResult Create()
        {
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Company_Jobs_Descriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Job,Job_Name,Job_Descriptions,Time_Stamp")] Company_Jobs_Descriptions company_Jobs_Descriptions)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Company_Jobs_Descriptions[] { company_Jobs_Descriptions });
                //company_Jobs_Descriptions.Id = Guid.NewGuid();
                //db.Company_Jobs_Descriptions.Add(company_Jobs_Descriptions);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Jobs_Descriptions.Job);
            return View(company_Jobs_Descriptions);
        }

        // GET: Company_Jobs_Descriptions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Jobs_Descriptions company_Jobs_Descriptions = logic.Get((Guid)id);//db.Company_Jobs_Descriptions.Find(id);
            if (company_Jobs_Descriptions == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Jobs_Descriptions.Job);
            return View(company_Jobs_Descriptions);
        }

        // POST: Company_Jobs_Descriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Job,Job_Name,Job_Descriptions,Time_Stamp")] Company_Jobs_Descriptions company_Jobs_Descriptions)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(company_Jobs_Descriptions).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Company_Jobs_Descriptions[] { company_Jobs_Descriptions });
                return RedirectToAction("Index");
            }
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Jobs_Descriptions.Job);
            return View(company_Jobs_Descriptions);
        }

        // GET: Company_Jobs_Descriptions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Jobs_Descriptions company_Jobs_Descriptions = logic.Get((Guid)id); //db.Company_Jobs_Descriptions.Find(id);
            if (company_Jobs_Descriptions == null)
            {
                return HttpNotFound();
            }
            return View(company_Jobs_Descriptions);
        }

        // POST: Company_Jobs_Descriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company_Jobs_Descriptions company_Jobs_Descriptions = logic.Get((Guid)id); //db.Company_Jobs_Descriptions.Find(id);
            logic.Delete(new Company_Jobs_Descriptions[] { company_Jobs_Descriptions });
            //db.Company_Jobs_Descriptions.Remove(company_Jobs_Descriptions);
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
