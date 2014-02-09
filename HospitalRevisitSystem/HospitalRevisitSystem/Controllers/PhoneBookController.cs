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
    public class PhoneBookController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /PhoneBook/

        public ActionResult Index(int id,string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPatientID = id;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var query = from s in db.tbPhoneBook
                        where s.Patient_ID == id
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Phone_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderBy(s => s.PhoneBook_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /PhoneBook/Details/5

        public ActionResult Details(int id = 0)
        {
            tbPhoneBook tbphonebook = db.tbPhoneBook.Find(id);
            if (tbphonebook == null)
            {
                return HttpNotFound();
            }
            return View(tbphonebook);
        }

        //
        // GET: /PhoneBook/Create

        public ActionResult Create(int id)
        {
            ViewBag.Patient_ID = id;
            return View();
        }

        //
        // POST: /PhoneBook/Create

        [HttpPost]
        public ActionResult Create(tbPhoneBook tbphonebook)
        {
            if (ModelState.IsValid)
            {
                
                db.tbPhoneBook.Add(tbphonebook);
                db.SaveChanges();
                return RedirectToAction("Index/"+tbphonebook.Patient_ID);
            }

            ViewBag.Patient_ID =  tbphonebook.Patient_ID;
            return View(tbphonebook);
        }

        //
        // GET: /PhoneBook/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbPhoneBook tbphonebook = db.tbPhoneBook.Find(id);
            if (tbphonebook == null)
            {
                return HttpNotFound();
            }
            ViewBag.Patient_ID = new SelectList(db.tbPatient, "Patient_ID", "Heath_Card", tbphonebook.Patient_ID);
            return View(tbphonebook);
        }

        //
        // POST: /PhoneBook/Edit/5

        [HttpPost]
        public ActionResult Edit(tbPhoneBook tbphonebook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbphonebook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Patient_ID = new SelectList(db.tbPatient, "Patient_ID", "Heath_Card", tbphonebook.Patient_ID);
            return View(tbphonebook);
        }

        //
        // GET: /PhoneBook/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbPhoneBook tbphonebook = db.tbPhoneBook.Find(id);
            if (tbphonebook == null)
            {
                return HttpNotFound();
            }
            return View(tbphonebook);
        }

        //
        // POST: /PhoneBook/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPhoneBook tbphonebook = db.tbPhoneBook.Find(id);
            db.tbPhoneBook.Remove(tbphonebook);
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