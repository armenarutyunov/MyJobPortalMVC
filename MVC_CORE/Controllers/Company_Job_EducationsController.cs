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
    public class Company_Job_EducationsController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Company_Job_Educations> repo = new EFGenericRepository<Company_Job_Educations>(false);
        private Company_Job_Educations_Logic logic;
        private Company_Jobs_Logic logic_cj;
        public Company_Job_EducationsController()
        {
           
            logic = new Company_Job_Educations_Logic(repo);
            var repo_cj = new EFGenericRepository<Company_Jobs>(false);
            logic_cj = new Company_Jobs_Logic(repo_cj);
        }
        // GET: Company_Job_Educations
        public ActionResult Index()
        {
            //var company_Job_Educations = db.Company_Job_Educations.Include(c => c.Company_Jobs);
            //return View(company_Job_Educations.ToList());
            var list = logic.GetAll();
            return View(list);
        }

        // GET: Company_Job_Educations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Job_Educations company_Job_Educations = logic.Get((Guid)id);//db.Company_Job_Educations.Find(id);
            if (company_Job_Educations == null)
            {
                return HttpNotFound();
            }
            return View(company_Job_Educations);
        }

        // GET: Company_Job_Educations/Create
        public ActionResult Create()
        {
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Company_Job_Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Job,Major,Importance,Time_Stamp")]
        Company_Job_Educations company_Job_Educations)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Company_Job_Educations[] { company_Job_Educations });
                //company_Job_Educations.Id = Guid.NewGuid();
                //db.Company_Job_Educations.Add(company_Job_Educations);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Job_Educations.Job);
            return View(company_Job_Educations);
        }

        // GET: Company_Job_Educations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Job_Educations company_Job_Educations = logic.Get((Guid)id);//db.Company_Job_Educations.Find(id);
            if (company_Job_Educations == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Job_Educations.Job);
            return View(company_Job_Educations);
        }

        // POST: Company_Job_Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Job,Major,Importance,Time_Stamp")] Company_Job_Educations company_Job_Educations)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(company_Job_Educations).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Company_Job_Educations[] { company_Job_Educations });
                return RedirectToAction("Index");
            }
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Job_Educations.Job);
            return View(company_Job_Educations);
        }

        // GET: Company_Job_Educations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Job_Educations company_Job_Educations = logic.Get((Guid)id);//db.Company_Job_Educations.Find(id);
            if (company_Job_Educations == null)
            {
                return HttpNotFound();
            }
            return View(company_Job_Educations);
        }

        // POST: Company_Job_Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company_Job_Educations company_Job_Educations = logic.Get((Guid)id);//db.Company_Job_Educations.Find(id);
            logic.Delete(new Company_Job_Educations[] { company_Job_Educations });
            //db.Company_Job_Educations.Remove(company_Job_Educations);
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
