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
    public class Applicant_ResumesController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();
        EFGenericRepository<Applicant_Resumes> repo = new EFGenericRepository<Applicant_Resumes>(false);
        private Applicant_Resumes_Logic logic;
        private Applicant_Profiles_Logic logic_ap;
        public Applicant_ResumesController()
        {
            logic = new Applicant_Resumes_Logic(repo);
            var repo_ap = new EFGenericRepository<Applicant_Profiles>(false);
            logic_ap = new Applicant_Profiles_Logic(repo_ap);
        }

        // GET: Applicant_Educations
        public ActionResult Index()
        {
            List<Applicant_Resumes> applicant_Resumes = logic.GetAll();

            return View(applicant_Resumes);

        }

        // GET: Applicant_Educations/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Resumes applicant_Resumes = logic.Get((Guid)id);
            if (applicant_Resumes == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Resumes);
        }

        // GET: Applicant_Educations/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency");
            return View();
        }

        // POST: Applicant_Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Resume,Last_Updated")]
        Applicant_Resumes applicant_Resumes)

        {
            if (ModelState.IsValid)
            {
                logic.Add(new Applicant_Resumes[] { applicant_Resumes });

                return RedirectToAction("Index");
            }

            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Resumes.Applicant);
            return View(applicant_Resumes);
        }

        // GET: Applicant_Educations/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Resumes applicant_Resumes = logic.Get((Guid)id);
            if (applicant_Resumes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Resumes.Applicant);
            return View(applicant_Resumes);
        }

        // POST: Applicant_Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Resume,Last_Updated")] Applicant_Resumes applicant_Resumes)
        {
            if (ModelState.IsValid)
            {

                logic.Update(new Applicant_Resumes[] { applicant_Resumes });
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(logic_ap.GetAll(), "Id", "Currency", applicant_Resumes.Applicant);
            return View(applicant_Resumes);
        }

        // GET: Applicant_Educations/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant_Resumes applicant_Resumes = logic.Get((Guid)id);
            if (applicant_Resumes == null)
            {
                return HttpNotFound();
            }
            return View(applicant_Resumes);
        }

        // POST: Applicant_Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Applicant_Resumes applicant_Resumes = logic.Get((Guid)id);
            logic.Delete(new Applicant_Resumes[] { applicant_Resumes });
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

        //// GET: Applicant_Resumes
        //public ActionResult Index()
        //{
        //    var applicant_Resumes = db.Applicant_Resumes.Include(a => a.Applicant_Profiles);
        //    return View(applicant_Resumes.ToList());
        //}

        //// GET: Applicant_Resumes/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Applicant_Resumes applicant_Resumes = db.Applicant_Resumes.Find(id);
        //    if (applicant_Resumes == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicant_Resumes);
        //}

        //// GET: Applicant_Resumes/Create
        //public ActionResult Create()
        //{
        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency");
        //    return View();
        //}

        //// POST: Applicant_Resumes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Applicant,Resume,Last_Updated")] Applicant_Resumes applicant_Resumes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        applicant_Resumes.Id = Guid.NewGuid();
        //        db.Applicant_Resumes.Add(applicant_Resumes);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency", applicant_Resumes.Applicant);
        //    return View(applicant_Resumes);
        //}

        //// GET: Applicant_Resumes/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Applicant_Resumes applicant_Resumes = db.Applicant_Resumes.Find(id);
        //    if (applicant_Resumes == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency", applicant_Resumes.Applicant);
        //    return View(applicant_Resumes);
        //}

        //// POST: Applicant_Resumes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Applicant,Resume,Last_Updated")] Applicant_Resumes applicant_Resumes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(applicant_Resumes).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Applicant = new SelectList(db.Applicant_Profiles, "Id", "Currency", applicant_Resumes.Applicant);
        //    return View(applicant_Resumes);
        //}

        //// GET: Applicant_Resumes/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Applicant_Resumes applicant_Resumes = db.Applicant_Resumes.Find(id);
        //    if (applicant_Resumes == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicant_Resumes);
        //}

        //// POST: Applicant_Resumes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Applicant_Resumes applicant_Resumes = db.Applicant_Resumes.Find(id);
        //    db.Applicant_Resumes.Remove(applicant_Resumes);
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
