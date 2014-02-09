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
    public class StaffController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /Staff/

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
            var query = from s in db.tbStaff
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Staff_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderBy(s => s.Staff_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Staff/Details/5

        public ActionResult Details(int id = 0)
        {
            tbStaff tbstaff = db.tbStaff.Find(id);
            if (tbstaff == null)
            {
                return HttpNotFound();
            }
            return View(tbstaff);
        }

        //
        // GET: /Staff/Create

        public ActionResult Create()
        {
            ViewBag.Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name");
            ViewBag.StaffDuty_ID = new SelectList(db.tbStaffDuty, "StaffDuty_ID", "StaffDuty_Name");
            return View();
        }

        //
        // POST: /Staff/Create

        [HttpPost]
        public ActionResult Create(tbStaff tbstaff)
        {
            if (ModelState.IsValid)
            {
                db.tbStaff.Add(tbstaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name", tbstaff.Department_ID);
            ViewBag.StaffDuty_ID = new SelectList(db.tbStaffDuty, "StaffDuty_ID", "StaffDuty_Name", tbstaff.StaffDuty_ID);
            return View(tbstaff);
        }

        //
        // GET: /Staff/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbStaff tbstaff = db.tbStaff.Find(id);
            if (tbstaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name", tbstaff.Department_ID);
            ViewBag.StaffDuty_ID = new SelectList(db.tbStaffDuty, "StaffDuty_ID", "StaffDuty_Name", tbstaff.StaffDuty_ID);
            return View(tbstaff);
        }

        //
        // POST: /Staff/Edit/5

        [HttpPost]
        public ActionResult Edit(tbStaff tbstaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbstaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name", tbstaff.Department_ID);
            ViewBag.StaffDuty_ID = new SelectList(db.tbStaffDuty, "StaffDuty_ID", "StaffDuty_Name", tbstaff.StaffDuty_ID);
            return View(tbstaff);
        }

        //
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbStaff tbstaff = db.tbStaff.Find(id);
            if (tbstaff == null)
            {
                return HttpNotFound();
            }
            return View(tbstaff);
        }

        //
        // POST: /Staff/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbStaff tbstaff = db.tbStaff.Find(id);
            db.tbStaff.Remove(tbstaff);
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