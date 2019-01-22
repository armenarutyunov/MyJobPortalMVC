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
    public class Company_ProfilesController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();

        EFGenericRepository<Company_Profiles> repo = new EFGenericRepository<Company_Profiles>(false);
        private Company_Profiles_Logic logic;
        public Company_ProfilesController()
        {
            logic = new Company_Profiles_Logic(repo);
        }
        // GET: Company_Profiles
        public ActionResult Index()
        {
            //return View(db.Company_Profiles.ToList());
            var list = logic.GetAll();
            return View(list);
        }

        // GET: Company_Profiles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Profiles company_Profiles = logic.Get((Guid)id); //db.Company_Profiles.Find(id);
            if (company_Profiles == null)
            {
                return HttpNotFound();
            }
            return View(company_Profiles);
        }

        // GET: Company_Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company_Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Registration_Date,Company_Website,Contact_Phone,Contact_Name,Company_Logo,Time_Stamp")] Company_Profiles company_Profiles)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Company_Profiles[] { company_Profiles });
                //company_Profiles.Id = Guid.NewGuid();
                //db.Company_Profiles.Add(company_Profiles);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company_Profiles);
        }

        // GET: Company_Profiles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Profiles company_Profiles = logic.Get((Guid)id); //db.Company_Profiles.Find(id);
            if (company_Profiles == null)
            {
                return HttpNotFound();
            }
            return View(company_Profiles);
        }

        // POST: Company_Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Registration_Date,Company_Website,Contact_Phone,Contact_Name,Company_Logo,Time_Stamp")] Company_Profiles company_Profiles)
        {
            if (ModelState.IsValid)
            {
                logic.Update(new Company_Profiles[] { company_Profiles });
                //db.Entry(company_Profiles).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company_Profiles);
        }

        // GET: Company_Profiles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_Profiles company_Profiles = logic.Get((Guid)id);// db.Company_Profiles.Find(id);
            if (company_Profiles == null)
            {
                return HttpNotFound();
            }
            return View(company_Profiles);
        }

        // POST: Company_Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company_Profiles company_Profiles = logic.Get((Guid)id); //db.Company_Profiles.Find(id);
            logic.Delete(new Company_Profiles[] { company_Profiles });
            //db.Company_Profiles.Remove(company_Profiles);
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
