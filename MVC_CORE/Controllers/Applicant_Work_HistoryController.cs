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
    public class Applicant_Work_HistoryController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Applicant_Work_History> repo = new EFGenericRepository<Applicant_Work_History>(false);
        private Applicant_Work_History_Logic logic;
        private Applicant_Profiles_Logic logic_ap;
        private System_Country_Codes_Logic logic_cc;
        public Applicant_Work_HistoryController()
        {
           
            logic = new Applicant_Work_History_Logic(repo);
            var repo_ap = new EFGenericRepository<Applicant_Profiles>(false);
            logic_ap = new Applicant_Profiles_Logic(repo_ap);
            var repo_cc = new EFGenericRepository<System_Country_Codes>(false);
            logic_cc = new System_Country_Codes_Logic(repo_cc);
        }
        // GET: Applicant_Work_History
        public ActionResult Index()
        {
            var applicant_Work_History = logic.GetAll();//db.Applicant_Work_History.Include(a => a.Applicant_Profiles).Include(a => a.System_Country_Codes);
            return View(applicant_Work_History.ToList());
        }
         
        // GET: Applicant_Work_History/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Work_History applicant_Work_History = logic.Get((Guid)id);// db.Applicant_Work_History.Find(id);
            if (applicant_Work_History == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Work_History);
        }

        // GET: Applicant_Work_History/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency");
            ViewBag.Country_Code = new SelectList(logic_cc.GetAll(), "Code", "Name");
            return View();
        }

        // POST: Applicant_Work_History/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Company_Name,Country_Code,Location,Job_Title,Job_Description,Start_Month,Start_Year,End_Month,End_Year,Time_Stamp")]
        Applicant_Work_History applicant_Work_History)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Applicant_Work_History[] { applicant_Work_History });
                //applicant_Work_History.Id = Guid.NewGuid();
                //db.Applicant_Work_History.Add(applicant_Work_History);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Work_History.Applicant);
            ViewBag.Country_Code = new SelectList(logic_cc.GetAll(), "Code", "Name", applicant_Work_History.Country_Code);
            return View(applicant_Work_History);
        }

        // GET: Applicant_Work_History/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Work_History applicant_Work_History = logic.Get((Guid)id);//db.Applicant_Work_History.Find(id);
            if (applicant_Work_History == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Work_History.Applicant);
            ViewBag.Country_Code = new SelectList(logic_cc.GetAll(), "Code", "Name", applicant_Work_History.Country_Code);
            return View(applicant_Work_History);
        }

        // POST: Applicant_Work_History/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Company_Name,Country_Code,Location,Job_Title,Job_Description,Start_Month,Start_Year,End_Month,End_Year,Time_Stamp")]
        Applicant_Work_History applicant_Work_History)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(applicant_Work_History).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Applicant_Work_History[] { applicant_Work_History });
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Work_History.Applicant);
            ViewBag.Country_Code = new SelectList(logic_cc.GetAll(), "Code", "Name", applicant_Work_History.Country_Code);
            return View(applicant_Work_History);
        }

        // GET: Applicant_Work_History/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Work_History applicant_Work_History = logic.Get((Guid)id);//db.Applicant_Work_History.Find(id);
            if (applicant_Work_History == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Work_History);
        }

        // POST: Applicant_Work_History/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Applicant_Work_History applicant_Work_History = logic.Get((Guid)id);//db.Applicant_Work_History.Find(id);
            //db.Applicant_Work_History.Remove(applicant_Work_History);
            //db.SaveChanges();
            logic.Delete(new Applicant_Work_History[] { applicant_Work_History });
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
