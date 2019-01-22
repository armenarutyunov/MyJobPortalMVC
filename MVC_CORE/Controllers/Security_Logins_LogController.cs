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
    public class Security_Logins_LogController : Controller
    {
        //private JOB_PORTAL_DBEntities db = new JOB_PORTAL_DBEntities();

        EFGenericRepository<Security_Logins_Log> repo = new EFGenericRepository<Security_Logins_Log>(false);
        private Security_Logins_Log_Logic logic;
        private Security_Logins_Logic logic_sl;
        public Security_Logins_LogController()
        {
            logic = new Security_Logins_Log_Logic(repo);
            var repo_sl = new EFGenericRepository<Security_Logins>(false);
            logic_sl = new Security_Logins_Logic(repo_sl);
        }
        // GET: Security_Logins_Log
        public ActionResult Index()
        {
            //var security_Logins_Log = db.Security_Logins_Log.Include(s => s.Security_Logins);
            //return View(security_Logins_Log.ToList());
            var list = logic.GetAll();
            return View(list);
        }

        // GET: Security_Logins_Log/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security_Logins_Log security_Logins_Log = logic.Get((Guid)id); //db.Security_Logins_Log.Find(id);
            if (security_Logins_Log == null)
            {
                return HttpNotFound();
            }
            return View(security_Logins_Log);
        }

        // GET: Security_Logins_Log/Create
        public ActionResult Create()
        {
            ViewBag.Login = new SelectList(logic_sl.GetAll(), "Id", "Login");
            return View();
        }

        // POST: Security_Logins_Log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Source_IP,Logon_Date,Is_Succesful")]
        Security_Logins_Log security_Logins_Log)
        {
            if (ModelState.IsValid)
            {
                logic.Add(new Security_Logins_Log[] { security_Logins_Log });
                //security_Logins_Log.Id = Guid.NewGuid();
                //db.Security_Logins_Log.Add(security_Logins_Log);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Login = new SelectList(logic_sl.GetAll(), "Id", "Login", security_Logins_Log.Login);
            return View(security_Logins_Log);
        }

        // GET: Security_Logins_Log/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security_Logins_Log security_Logins_Log = logic.Get((Guid)id); //db.Security_Logins_Log.Find(id);
            if (security_Logins_Log == null)
            {
                return HttpNotFound();
            }
            ViewBag.Login = new SelectList(logic_sl.GetAll(), "Id", "Login", security_Logins_Log.Login);
            return View(security_Logins_Log);
        }

        // POST: Security_Logins_Log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Source_IP,Logon_Date,Is_Succesful")]
        Security_Logins_Log security_Logins_Log)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(security_Logins_Log).State = EntityState.Modified;
                //db.SaveChanges();
                logic.Update(new Security_Logins_Log[] { security_Logins_Log });
                return RedirectToAction("Index");
            }
            ViewBag.Login = new SelectList(logic_sl.GetAll(), "Id", "Login", security_Logins_Log.Login);
            return View(security_Logins_Log);
        }

        // GET: Security_Logins_Log/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Security_Logins_Log security_Logins_Log = logic.Get((Guid)id); //db.Security_Logins_Log.Find(id);
            if (security_Logins_Log == null)
            {
                return HttpNotFound();
            }
            return View(security_Logins_Log);
        }

        // POST: Security_Logins_Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Security_Logins_Log security_Logins_Log = logic.Get((Guid)id); //db.Security_Logins_Log.Find(id);
            logic.Delete(new Security_Logins_Log[] { security_Logins_Log });
            //db.Security_Logins_Log.Remove(security_Logins_Log);
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
