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
    public class JobController : Controller
    {

        public Job CompanyJob;
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Company_Jobs> repo_cj = new EFGenericRepository<Company_Jobs>(false);
        private Company_Jobs_Logic logic_cj;
        private Company_Job_Educations_Logic logic_je;
        private Company_Jobs_Descriptions_Logic logic_jd;
        private Company_Job_Skills_Logic logic_js;
        public JobController()
        {
            TempData["ProfileID"] = Guid.NewGuid();
            var repo_je = new EFGenericRepository<Company_Job_Educations>(false);
            var repo_jd = new EFGenericRepository<Company_Jobs_Descriptions>(false);
            var repo_js = new EFGenericRepository<Company_Job_Skills>(false);
            logic_cj = new Company_Jobs_Logic(repo_cj);
            logic_je = new Company_Job_Educations_Logic(repo_je);
            logic_jd = new Company_Jobs_Descriptions_Logic(repo_jd);
            logic_js = new Company_Job_Skills_Logic(repo_js);
        }
            public ActionResult Index()
            {
            Job jobprofile = (Job)TempData["JobProfile"];
            List<Job> list = new List<Job>() { jobprofile };
            TempData["JobProfile"] = jobprofile;
            return View(list);
            }

        
        // GET: Company_Descriptions/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Company_Descriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobName,JobDescription,Major,MajorImp,Skill,SkillLevel,SkillImp")]
        Job CJ)
        {
            if (ModelState.IsValid)
            {
                //logic.Add(new Company_Descriptions[] { company_Descriptions });
                Guid _cpID =(Guid) TempData["ProfileID"];
                //----- Company_Jobs------------
                Company_Jobs j = new Company_Jobs();
                j.Id = Guid.NewGuid(); j.Company = _cpID; j.Profile_Created = DateTime.Now;
                j.Is_Inactive = false; j.Is_Company_Hidden = false;
                logic_cj.Add(new Company_Jobs[] { j });
                
                //----- Company_Description------------
                Company_Jobs_Descriptions jd = new Company_Jobs_Descriptions();
                jd.Id = Guid.NewGuid(); jd.Job = j.Id; jd.Job_Name = CJ.JobName; jd.Job_Descriptions = CJ.JobDescription;
                logic_jd.Add(new Company_Jobs_Descriptions[] { jd });
                //----- Company_Education------------
                Company_Job_Educations je = new Company_Job_Educations();
                je.Id = Guid.NewGuid(); je.Job = j.Id; je.Major = CJ.Major; je.Importance =Convert.ToInt16(CJ.MajorImp);
                logic_je.Add(new Company_Job_Educations[] { je });
                //----- Company_Skill------------
                Company_Job_Skills js = new Company_Job_Skills();
                js.Id = Guid.NewGuid(); js.Job = j.Id; js.Skill = CJ.Skill; js.Skill_Level = CJ.SkillLevel; js.Importance = Convert.ToInt16(CJ.SkillImp);
                logic_js.Add(new Company_Job_Skills[] { js });
                //-------------------------------
                CJ.ProfileCreated = DateTime.Now;
                TempData["JobProfile"] = CJ;
                TempData["Job"] = j.Id;
                TempData["ProfileID"] = _cpID;
                return RedirectToAction("Index");
            }

         
            return View();
        }
        //---------------------------------------------------------------------------------

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Job> listjob = (List<Job>)TempData["JobList"];
            Job job = null;
            foreach (Job item in listjob)
            {
                if (item.Id == id) { job = item; }
            }
            TempData["JobList"] = listjob;
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobName,JobDescription,Major,MajorImp,Skill,SkillLevel,SkillImp")]
        Job CJ)
        {

            //Guid _ProfileID = (Guid)TempData["ProfileID"];

            var jd = logic_jd.GetList(a => a.Job == CJ.Id).FirstOrDefault();
            var je = logic_je.GetList(a => a.Job == CJ.Id).FirstOrDefault();
            var js = logic_js.GetList(a => a.Job == CJ.Id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //-----Job Description------------------
                jd.Job_Name = CJ.JobName; jd.Job_Descriptions=CJ.JobDescription;
                Company_Jobs_Descriptions[] jdlist = new Company_Jobs_Descriptions[] { jd };
                logic_jd.Update(jdlist);

                //-----Job Education--------------------
                je.Major = CJ.Major; je.Importance = Convert.ToInt16(CJ.MajorImp);
                Company_Job_Educations[] jelist = new Company_Job_Educations[] { je };
                logic_je.Update(jelist);

                //-----Job Skills-----------------------
                js.Skill = CJ.Skill; js.Importance = Convert.ToInt16(CJ.SkillImp);js.Skill_Level = CJ.SkillLevel;
                Company_Job_Skills[] jslist = new Company_Job_Skills[] { js };
                logic_js.Update(jslist);

                TempData["JobProfile"] = CJ;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        //---------------------------------------------------------------------------------

        public ActionResult ShowAll()
        {
            Guid _ProfileID = (Guid)TempData["ProfileID"];
            IList<Company_Jobs> list_cj = logic_cj.GetList(a => a.Company == _ProfileID); //logic_cj.GetAll().Where(a => a.Company == _ProfileID).ToList();
            if (list_cj != null)
            {
                List<Job> JobList = new List<Job>();
                Job t = new Job();
                Guid j; 
                foreach (Company_Jobs item in list_cj)
                {
                   
                    j = item.Id;
                    JobList.Add(new Job { 
                    ProfileCreated = logic_cj.Get(j).Profile_Created,
                    JobName = logic_jd.GetList(a => a.Job ==j).First().Job_Name,
                    JobDescription = logic_jd.GetList(a => a.Job == j).First().Job_Descriptions,
                    Major = logic_je.GetList(a => a.Job == j).First().Major,
                    MajorImp = logic_je.GetList(a => a.Job == j).First().Importance.ToString(),
                    Skill = logic_js.GetList(a => a.Job == j).First().Skill,
                    SkillImp = logic_js.GetList(a => a.Job == j).First().Importance.ToString(),
                    SkillLevel = logic_js.GetList(a => a.Job == j).First().Skill_Level,
                    Id = j
                });
                
                    
                    

                }
                TempData["JobList"] = JobList;
                TempData["ProfileID"] = _ProfileID;
                return View(JobList);
            }
            ViewBag.Reason = "There Is Nothig To Show! Post You First Job";
            TempData["ProfileID"] = _ProfileID;
            return RedirectToAction("Error", "Company_Login");

        }
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Job> listjob = (List<Job>)TempData["JobList"];
            Job job=null;
            foreach (Job item in listjob)
            {
                if (item.Id == id) { job = item; }
            }
            TempData["JobList"] = listjob;
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);

        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {

           
            Company_Jobs_Descriptions[] cjd = logic_jd.GetList(a => a.Job == id).ToArray(); logic_jd.Delete(cjd);
            Company_Job_Educations[] cje = logic_je.GetList(a => a.Job == id).ToArray(); logic_je.Delete(cje);
            Company_Job_Skills[] cjs = logic_js.GetList(a => a.Job == id).ToArray(); logic_js.Delete(cjs);

            logic_cj.Delete(new Company_Jobs[] { logic_cj.Get(id) });

            
            TempData["Msg"] = "Your Position Has Been Deleted!";
            TempData["FromAction"] = 11;
            return RedirectToAction("Message","Security_Logins");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo_cj._context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}