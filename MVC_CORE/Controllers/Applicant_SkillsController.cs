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
    public class Applicant_SkillsController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Applicant_Skills> repo = new EFGenericRepository<Applicant_Skills>(false);
        private Applicant_Skills_Logic logic;
        private Applicant_Profiles_Logic logic_ap;
        public Applicant_SkillsController()
        {
            
            logic = new Applicant_Skills_Logic(repo);
            var repo_ap = new EFGenericRepository<Applicant_Profiles>(false);
            logic_ap = new Applicant_Profiles_Logic(repo_ap);
        }
        // GET: Applicant_Skills
        public ActionResult Index()
        {
            List<Applicant_Skills> applicant_Skills = logic.GetAll();
            return View(applicant_Skills);
            //var applicant_Skills = db.Applicant_Skills.Include(a => a.Applicant_Profiles);
            //return View(applicant_Skills.ToList());
        }

        // GET: Applicant_Skills/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Applicant_Skills applicant_Skills = db.Applicant_Skills.Find(id);
            Applicant_Skills applicant_Skills = logic.Get((Guid)id);
            if (applicant_Skills == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Skills);
        }

        // GET: Applicant_Skills/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency");
            return View();
        }

        // POST: Applicant_Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Skill,Skill_Level,Start_Month,Start_Year,End_Month,End_Year,Time_Stamp")]
        Applicant_Skills applicant_Skills)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Applicant_Skills[] { applicant_Skills });
                //applicant_Skills.Id = Guid.NewGuid();
                //db.Applicant_Skills.Add(applicant_Skills);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Skills.Applicant);
            return View(applicant_Skills);
        }

        // GET: Applicant_Skills/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Skills applicant_Skills = logic.Get((Guid)id); /*db.Applicant_Skills.Find(id);*/
            if (applicant_Skills == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Skills.Applicant);
            return View(applicant_Skills);
        }

        // POST: Applicant_Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Skill,Skill_Level,Start_Month,Start_Year,End_Month,End_Year,Time_Stamp")]
        Applicant_Skills applicant_Skills)
        {
            if (ModelState.IsValid)
            {
                logic.Update(new Applicant_Skills[] { applicant_Skills });
                //db.Entry(applicant_Skills).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Skills.Applicant);
            return View(applicant_Skills);
        }

        // GET: Applicant_Skills/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Skills applicant_Skills = logic.Get((Guid)id); /*db.Applicant_Skills.Find(id);*/
            if (applicant_Skills == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Skills);
        }

        // POST: Applicant_Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Applicant_Skills applicant_Skills = logic.Get((Guid)id); /*db.Applicant_Skills.Find(id);*/
            logic.Delete(new Applicant_Skills[] { applicant_Skills });
            //db.Applicant_Skills.Remove(applicant_Skills);
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
