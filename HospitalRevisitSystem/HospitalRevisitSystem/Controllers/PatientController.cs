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
    public class PatientController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /Patient/

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
            var query = from s in db.tbPatient
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Patient_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderBy(s => s.Patient_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Patient/Details/5

        public ActionResult Details(int id = 0)
        {
            tbPatient tbpatient = db.tbPatient.Find(id);
            if (tbpatient == null)
            {
                return HttpNotFound();
            }
            return View(tbpatient);
        }

        //
        // GET: /Patient/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Patient/Create

        [HttpPost]
        public ActionResult Create(tbPatient tbpatient)
        {
            if (ModelState.IsValid)
            {
                db.tbPatient.Add(tbpatient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbpatient);
        }

        //
        // GET: /Patient/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbPatient tbpatient = db.tbPatient.Find(id);
            if (tbpatient == null)
            {
                return HttpNotFound();
            }
            return View(tbpatient);
        }

        //
        // POST: /Patient/Edit/5

        [HttpPost]
        public ActionResult Edit(tbPatient tbpatient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbpatient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbpatient);
        }

        //
        // GET: /Patient/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbPatient tbpatient = db.tbPatient.Find(id);
            if (tbpatient == null)
            {
                return HttpNotFound();
            }
            return View(tbpatient);
        }

        //
        // POST: /Patient/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPatient tbpatient = db.tbPatient.Find(id);
            db.tbPatient.Remove(tbpatient);
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