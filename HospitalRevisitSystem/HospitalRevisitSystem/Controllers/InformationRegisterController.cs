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
    public class InformationRegisterController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /InformationRegister/

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
            var query = from s in db.tbInformation_Register
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Consulting_Phone_Number.ToLower().Contains(searchString.ToLower()) == true);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /InformationRegister/Details/5

        public ActionResult Details(int id = 0)
        {
            tbInformation_Register tbinformation_register = db.tbInformation_Register.Find(id);
            if (tbinformation_register == null)
            {
                return HttpNotFound();
            }
            return View(tbinformation_register);
        }

        //
        // GET: /InformationRegister/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /InformationRegister/Create

        [HttpPost]
        public ActionResult Create(tbInformation_Register tbinformation_register)
        {
            if (ModelState.IsValid)
            {
                db.tbInformation_Register.Add(tbinformation_register);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbinformation_register);
        }

        //
        // GET: /InformationRegister/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbInformation_Register tbinformation_register = db.tbInformation_Register.Find(id);
            if (tbinformation_register == null)
            {
                return HttpNotFound();
            }
            return View(tbinformation_register);
        }

        //
        // POST: /InformationRegister/Edit/5

        [HttpPost]
        public ActionResult Edit(tbInformation_Register tbinformation_register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbinformation_register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbinformation_register);
        }

        //
        // GET: /InformationRegister/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbInformation_Register tbinformation_register = db.tbInformation_Register.Find(id);
            if (tbinformation_register == null)
            {
                return HttpNotFound();
            }
            return View(tbinformation_register);
        }

        //
        // POST: /InformationRegister/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInformation_Register tbinformation_register = db.tbInformation_Register.Find(id);
            db.tbInformation_Register.Remove(tbinformation_register);
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