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
    public class Company_Job_SkillsController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Company_Job_Skills> repo = new EFGenericRepository<Company_Job_Skills>(false);
        private Company_Job_Skills_Logic logic;
        private Company_Jobs_Logic logic_cj;
        public Company_Job_SkillsController()
        {
            
            logic = new Company_Job_Skills_Logic(repo);
            var repo_cj = new EFGenericRepository<Company_Jobs>(false);
            logic_cj = new Company_Jobs_Logic(repo_cj);
        }
        // GET: Company_Job_Skills
        public ActionResult Index()
        {
            //var company_Job_Skills = db.Company_Job_Skills.Include(c => c.Company_Jobs);
            //return View(company_Job_Skills.ToList());
            var list = logic.GetAll();
            return View(list);
        }

        // GET: Company_Job_Skills/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Job_Skills company_Job_Skills = logic.Get((Guid)id); //db.Company_Job_Skills.Find(id);
            if (company_Job_Skills == null)
            {
                return HttpNotFound();
            }
            return View(company_Job_Skills);
        }

        // GET: Company_Job_Skills/Create
        public ActionResult Create()
        {
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Company_Job_Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Job,Skill,Skill_Level,Importance,Time_Stamp")]
        Company_Job_Skills company_Job_Skills)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Company_Job_Skills[] { company_Job_Skills });
                //company_Job_Skills.Id = Guid.NewGuid();
                //db.Company_Job_Skills.Add(company_Job_Skills);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Job_Skills.Job);
            return View(company_Job_Skills);
        }

        // GET: Company_Job_Skills/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Job_Skills company_Job_Skills = logic.Get((Guid)id); //db.Company_Job_Skills.Find(id);
            if (company_Job_Skills == null)
            {
                return HttpNotFound();
            }
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Job_Skills.Job);
            return View(company_Job_Skills);
        }

        // POST: Company_Job_Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Job,Skill,Skill_Level,Importance,Time_Stamp")]
        Company_Job_Skills company_Job_Skills)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(company_Job_Skills).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Company_Job_Skills[] { company_Job_Skills });
                return RedirectToAction("Index");
            }
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", company_Job_Skills.Job);
            return View(company_Job_Skills);
        }

        // GET: Company_Job_Skills/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Job_Skills company_Job_Skills = logic.Get((Guid)id);// db.Company_Job_Skills.Find(id);
            if (company_Job_Skills == null)
            {
                return HttpNotFound();
            }
            return View(company_Job_Skills);
        }

        // POST: Company_Job_Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company_Job_Skills company_Job_Skills = logic.Get((Guid)id);// db.Company_Job_Skills.Find(id);
            logic.Delete(new Company_Job_Skills[] { company_Job_Skills });
            //db.Company_Job_Skills.Remove(company_Job_Skills);
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
