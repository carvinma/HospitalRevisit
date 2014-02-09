using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HospitalRevisitSystem.Controllers
{
    public class PersonnalCallerController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /PersonnalCaller/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var query = from s in db.tbPersonnal_Caller
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Remarks.ToLower().Contains(searchString.ToLower()) == true);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PersonnalCaller/Details/5

        public ActionResult Details(int id = 0)
        {
            tbPersonnal_Caller tbpersonnal_caller = db.tbPersonnal_Caller.Find(id);
            if (tbpersonnal_caller == null)
            {
                return HttpNotFound();
            }
            return View(tbpersonnal_caller);
        }

        //
        // GET: /PersonnalCaller/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PersonnalCaller/Create

        [HttpPost]
        public ActionResult Create(tbPersonnal_Caller tbpersonnal_caller)
        {
            if (ModelState.IsValid)
            {
                db.tbPersonnal_Caller.Add(tbpersonnal_caller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbpersonnal_caller);
        }

        //
        // GET: /PersonnalCaller/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbPersonnal_Caller tbpersonnal_caller = db.tbPersonnal_Caller.Find(id);
            if (tbpersonnal_caller == null)
            {
                return HttpNotFound();
            }
            return View(tbpersonnal_caller);
        }

        //
        // POST: /PersonnalCaller/Edit/5

        [HttpPost]
        public ActionResult Edit(tbPersonnal_Caller tbpersonnal_caller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbpersonnal_caller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbpersonnal_caller);
        }

        //
        // GET: /PersonnalCaller/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbPersonnal_Caller tbpersonnal_caller = db.tbPersonnal_Caller.Find(id);
            if (tbpersonnal_caller == null)
            {
                return HttpNotFound();
            }
            return View(tbpersonnal_caller);
        }

        //
        // POST: /PersonnalCaller/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPersonnal_Caller tbpersonnal_caller = db.tbPersonnal_Caller.Find(id);
            db.tbPersonnal_Caller.Remove(tbpersonnal_caller);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}