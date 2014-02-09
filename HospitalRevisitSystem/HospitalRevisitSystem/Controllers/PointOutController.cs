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
    public class PointOutController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /PointOut/

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
            var query = from s in db.tbPoint_Out
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Department_Response_Content.ToLower().Contains(searchString.ToLower()) == true);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PointOut/Details/5

        public ActionResult Details(int id = 0)
        {
            tbPoint_Out tbpoint_out = db.tbPoint_Out.Find(id);
            if (tbpoint_out == null)
            {
                return HttpNotFound();
            }
            return View(tbpoint_out);
        }

        //
        // GET: /PointOut/Create

        public ActionResult Create()
        {
            ViewBag.Deal_Way_ID = new SelectList(db.tbDeal_Way, "Deal_Way_ID", "Processing_Method");
            ViewBag.Response_Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name");
            ViewBag.Information_Register_ID = new SelectList(db.tbInformation_Register, "Information_Register_ID", "Consulting_Phone_Number");
            ViewBag.Response_Staff_ID = new SelectList(db.tbStaff, "Staff_ID", "Staff_Name");
            return View();
        }

        //
        // POST: /PointOut/Create

        [HttpPost]
        public ActionResult Create(tbPoint_Out tbpoint_out)
        {
            if (ModelState.IsValid)
            {
                db.tbPoint_Out.Add(tbpoint_out);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Deal_Way_ID = new SelectList(db.tbDeal_Way, "Deal_Way_ID", "Processing_Method", tbpoint_out.Deal_Way_ID);
            ViewBag.Response_Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name", tbpoint_out.Response_Department_ID);
            ViewBag.Information_Register_ID = new SelectList(db.tbInformation_Register, "Information_Register_ID", "Consulting_Phone_Number", tbpoint_out.Information_Register_ID);
            ViewBag.Response_Staff_ID = new SelectList(db.tbStaff, "Staff_ID", "Staff_Name", tbpoint_out.Response_Staff_ID);
            return View(tbpoint_out);
        }

        //
        // GET: /PointOut/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbPoint_Out tbpoint_out = db.tbPoint_Out.Find(id);
            if (tbpoint_out == null)
            {
                return HttpNotFound();
            }
            ViewBag.Deal_Way_ID = new SelectList(db.tbDeal_Way, "Deal_Way_ID", "Processing_Method", tbpoint_out.Deal_Way_ID);
            ViewBag.Response_Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name", tbpoint_out.Response_Department_ID);
            ViewBag.Information_Register_ID = new SelectList(db.tbInformation_Register, "Information_Register_ID", "Consulting_Phone_Number", tbpoint_out.Information_Register_ID);
            ViewBag.Response_Staff_ID = new SelectList(db.tbStaff, "Staff_ID", "Staff_Name", tbpoint_out.Response_Staff_ID);
            return View(tbpoint_out);
        }

        //
        // POST: /PointOut/Edit/5

        [HttpPost]
        public ActionResult Edit(tbPoint_Out tbpoint_out)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbpoint_out).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Deal_Way_ID = new SelectList(db.tbDeal_Way, "Deal_Way_ID", "Processing_Method", tbpoint_out.Deal_Way_ID);
            ViewBag.Response_Department_ID = new SelectList(db.tbDepartment, "Department_ID", "Department_Name", tbpoint_out.Response_Department_ID);
            ViewBag.Information_Register_ID = new SelectList(db.tbInformation_Register, "Information_Register_ID", "Consulting_Phone_Number", tbpoint_out.Information_Register_ID);
            ViewBag.Response_Staff_ID = new SelectList(db.tbStaff, "Staff_ID", "Staff_Name", tbpoint_out.Response_Staff_ID);
            return View(tbpoint_out);
        }

        //
        // GET: /PointOut/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbPoint_Out tbpoint_out = db.tbPoint_Out.Find(id);
            if (tbpoint_out == null)
            {
                return HttpNotFound();
            }
            return View(tbpoint_out);
        }

        //
        // POST: /PointOut/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPoint_Out tbpoint_out = db.tbPoint_Out.Find(id);
            db.tbPoint_Out.Remove(tbpoint_out);
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