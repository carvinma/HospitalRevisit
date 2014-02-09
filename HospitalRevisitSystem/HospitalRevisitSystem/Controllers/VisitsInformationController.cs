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
    public class VisitsInformationController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /VisitsInformation/

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
            var query = from s in db.tbVisits_Information
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Remarks.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderBy(s => s.Visits_Information_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /VisitsInformation/Details/5

        public ActionResult Details(int id = 0)
        {
            tbVisits_Information tbvisits_information = db.tbVisits_Information.Find(id);
            if (tbvisits_information == null)
            {
                return HttpNotFound();
            }
            return View(tbvisits_information);
        }

        //
        // GET: /VisitsInformation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VisitsInformation/Create

        [HttpPost]
        public ActionResult Create(tbVisits_Information tbvisits_information)
        {
            if (ModelState.IsValid)
            {
                db.tbVisits_Information.Add(tbvisits_information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbvisits_information);
        }

        //
        // GET: /VisitsInformation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbVisits_Information tbvisits_information = db.tbVisits_Information.Find(id);
            if (tbvisits_information == null)
            {
                return HttpNotFound();
            }
            return View(tbvisits_information);
        }

        //
        // POST: /VisitsInformation/Edit/5

        [HttpPost]
        public ActionResult Edit(tbVisits_Information tbvisits_information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbvisits_information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbvisits_information);
        }

        //
        // GET: /VisitsInformation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbVisits_Information tbvisits_information = db.tbVisits_Information.Find(id);
            if (tbvisits_information == null)
            {
                return HttpNotFound();
            }
            return View(tbvisits_information);
        }

        //
        // POST: /VisitsInformation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbVisits_Information tbvisits_information = db.tbVisits_Information.Find(id);
            db.tbVisits_Information.Remove(tbvisits_information);
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