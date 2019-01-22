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
    public class Applicant_Job_ApplicationsController : Controller
    {
        // private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        private EFGenericRepository<Applicant_Job_Applications> repo = new EFGenericRepository<Applicant_Job_Applications>(true);
        private Applicant_Job_Applications_Logic logic;
        private Applicant_Profiles_Logic logic_ap;
        private Company_Jobs_Logic logic_cj;
        public Applicant_Job_ApplicationsController()
        {
            //var repo = new EFGenericRepository<Applicant_Job_Applications>(false);
            logic = new Applicant_Job_Applications_Logic(repo);
            var repo_ap = new EFGenericRepository<Applicant_Profiles>(false);
            logic_ap = new Applicant_Profiles_Logic(repo_ap);
            var repo_cj = new EFGenericRepository<Company_Jobs>(false);
            logic_cj = new Company_Jobs_Logic(repo_cj);
        }
        // GET: Applicant_Job_Applications
        public ActionResult Index()
        {
            //var applicant_Job_Applications = db.Applicant_Job_Applications.Include(a => a.Applicant_Profiles).Include(a => a.Company_Jobs);
            //return View(applicant_Job_Applications.ToList());
            List<Applicant_Job_Applications> applicant_Job_Applications = logic.GetAll();
            return View(applicant_Job_Applications);
        }

        // GET: Applicant_Job_Applications/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Job_Applications applicant_Job_Applications = logic.Get((Guid)id);
            if (applicant_Job_Applications == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Job_Applications);
        }

        // GET: Applicant_Job_Applications/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency");
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Applicant_Job_Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Job,Application_Date,Time_Stamp")]
        Applicant_Job_Applications applicant_Job_Applications)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Applicant_Job_Applications[] { applicant_Job_Applications });
                //applicant_Job_Applications.Id = Guid.NewGuid();
                //db.Applicant_Job_Applications.Add(applicant_Job_Applications);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Job_Applications.Applicant);
            ViewBag.Job = new SelectList(logic_cj.GetAll(), "Id", "Id", applicant_Job_Applications.Job);
            return View(applicant_Job_Applications);
        }

        // GET: Applicant_Job_Applications/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Job_Applications applicant_Job_Applications = logic.Get((Guid)id);
            if (applicant_Job_Applications == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Job_Applications.Applicant);
            ViewBag.Job = new SelectList(logic_ap.GetAll(), "Id", "Id", applicant_Job_Applications.Job);
            return View(applicant_Job_Applications);
        }

        // POST: Applicant_Job_Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Job,Application_Date,Time_Stamp")] Applicant_Job_Applications applicant_Job_Applications)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(applicant_Job_Applications).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Applicant_Job_Applications[] { applicant_Job_Applications });
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Job_Applications.Applicant);
            ViewBag.Job = new SelectList(logic_ap.GetAll(), "Id", "Id", applicant_Job_Applications.Job);
            return View(applicant_Job_Applications);
        }

        // GET: Applicant_Job_Applications/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Job_Applications applicant_Job_Applications = logic.Get((Guid)id);
            if (applicant_Job_Applications == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Job_Applications);
        }

        // POST: Applicant_Job_Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            //Applicant_Job_Applications applicant_Job_Applications = db.Applicant_Job_Applications.Find(id);
            //db.Applicant_Job_Applications.Remove(applicant_Job_Applications);
            //db.SaveChanges();
            Applicant_Job_Applications applicant_Job_Applications = logic.Get((Guid)id);
            logic.Delete(new Applicant_Job_Applications[] { applicant_Job_Applications });
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
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();

        //// GET: Applicant_Job_Applications
        //public ActionResult Index()
        //{
        //    var applicant_Job_Applications = db.Applicant_Job_Applications.Include(a => a.Applicant_Profiles).Include(a => a.Company_Jobs);
        //    return View(applicant_Job_Applications.ToList());
        //}

        //// GET: Applicant_Job_Applications/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Applicant_Job_Applications applicant_Job_Applications = db.Applicant_Job_Applications.Find(id);
        //    if (applicant_Job_Applications == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicant_Job_Applications);
        //}

        //// GET: Applicant_Job_Applications/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency");
        //    ViewBag.Job = new SelectList(db.Company_Jobs, "Id", "Id");
        //    return View();
        //}

        //// POST: Applicant_Job_Applications/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Applicant,Job,Application_Date,Time_Stamp")] Applicant_Job_Applications applicant_Job_Applications)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        applicant_Job_Applications.Id = Guid.NewGuid();
        //        db.Applicant_Job_Applications.Add(applicant_Job_Applications);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency", applicant_Job_Applications.Applicant);
        //    ViewBag.Job = new SelectList(db.Company_Jobs, "Id", "Id", applicant_Job_Applications.Job);
        //    return View(applicant_Job_Applications);
        //}

        //// GET: Applicant_Job_Applications/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Applicant_Job_Applications applicant_Job_Applications = db.Applicant_Job_Applications.Find(id);
        //    if (applicant_Job_Applications == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency", applicant_Job_Applications.Applicant);
        //    ViewBag.Job = new SelectList(db.Company_Jobs, "Id", "Id", applicant_Job_Applications.Job);
        //    return View(applicant_Job_Applications);
        //}

        //// POST: Applicant_Job_Applications/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Applicant,Job,Application_Date,Time_Stamp")] Applicant_Job_Applications applicant_Job_Applications)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(applicant_Job_Applications).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency", applicant_Job_Applications.Applicant);
        //    ViewBag.Job = new SelectList(db.Company_Jobs, "Id", "Id", applicant_Job_Applications.Job);
        //    return View(applicant_Job_Applications);
        //}

        //// GET: Applicant_Job_Applications/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Applicant_Job_Applications applicant_Job_Applications = db.Applicant_Job_Applications.Find(id);
        //    if (applicant_Job_Applications == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicant_Job_Applications);
        //}

        //// POST: Applicant_Job_Applications/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Applicant_Job_Applications applicant_Job_Applications = db.Applicant_Job_Applications.Find(id);
        //    db.Applicant_Job_Applications.Remove(applicant_Job_Applications);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
