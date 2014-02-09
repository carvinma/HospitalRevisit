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
    public class DepartmentController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /Department/

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
            var query = from s in db.tbDepartment
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Department_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderBy(s => s.Department_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Department/Details/5

        public ActionResult Details(int id = 0)
        {
            tbDepartment tbdepartment = db.tbDepartment.Find(id);
            if (tbdepartment == null)
            {
                return HttpNotFound();
            }
            return View(tbdepartment);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(tbDepartment tbdepartment)
        {
            if (ModelState.IsValid)
            {
                db.tbDepartment.Add(tbdepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbdepartment);
        }

        //
        // GET: /Department/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbDepartment tbdepartment = db.tbDepartment.Find(id);
            if (tbdepartment == null)
            {
                return HttpNotFound();
            }
            return View(tbdepartment);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(tbDepartment tbdepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbdepartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbdepartment);
        }

        //
        // GET: /Department/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbDepartment tbdepartment = db.tbDepartment.Find(id);
            if (tbdepartment == null)
            {
                return HttpNotFound();
            }
            return View(tbdepartment);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDepartment tbdepartment = db.tbDepartment.Find(id);
            db.tbDepartment.Remove(tbdepartment);
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