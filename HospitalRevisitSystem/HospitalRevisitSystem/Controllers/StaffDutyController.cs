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
    public class StaffDutyController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /StaffDuty/

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
            var query = from s in db.tbStaffDuty
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.StaffDuty_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.StaffDuty_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /StaffDuty/Details/5

        public ActionResult Details(int id = 0)
        {
            tbStaffDuty tbstaffduty = db.tbStaffDuty.Find(id);
            if (tbstaffduty == null)
            {
                return HttpNotFound();
            }
            return View(tbstaffduty);
        }

        //
        // GET: /StaffDuty/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StaffDuty/Create

        [HttpPost]
        public ActionResult Create(tbStaffDuty tbstaffduty)
        {
            if (ModelState.IsValid)
            {
                db.tbStaffDuty.Add(tbstaffduty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbstaffduty);
        }

        //
        // GET: /StaffDuty/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbStaffDuty tbstaffduty = db.tbStaffDuty.Find(id);
            if (tbstaffduty == null)
            {
                return HttpNotFound();
            }
            return View(tbstaffduty);
        }

        //
        // POST: /StaffDuty/Edit/5

        [HttpPost]
        public ActionResult Edit(tbStaffDuty tbstaffduty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbstaffduty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbstaffduty);
        }

        //
        // GET: /StaffDuty/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbStaffDuty tbstaffduty = db.tbStaffDuty.Find(id);
            if (tbstaffduty == null)
            {
                return HttpNotFound();
            }
            return View(tbstaffduty);
        }

        //
        // POST: /StaffDuty/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbStaffDuty tbstaffduty = db.tbStaffDuty.Find(id);
            db.tbStaffDuty.Remove(tbstaffduty);
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