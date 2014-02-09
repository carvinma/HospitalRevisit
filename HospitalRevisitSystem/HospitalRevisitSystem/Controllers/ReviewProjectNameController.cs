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
    public class ReviewProjectNameController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /ReviewProjectName/

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
            var query = from s in db.tbReview_Project_Name
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Review_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderBy(s => s.Review_Project_Name_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /ReviewProjectName/Details/5

        public ActionResult Details(int id = 0)
        {
            tbReview_Project_Name tbreview_project_name = db.tbReview_Project_Name.Find(id);
            if (tbreview_project_name == null)
            {
                return HttpNotFound();
            }
            return View(tbreview_project_name);
        }

        //
        // GET: /ReviewProjectName/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReviewProjectName/Create

        [HttpPost]
        public ActionResult Create(tbReview_Project_Name tbreview_project_name)
        {
            if (ModelState.IsValid)
            {
                db.tbReview_Project_Name.Add(tbreview_project_name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbreview_project_name);
        }

        //
        // GET: /ReviewProjectName/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbReview_Project_Name tbreview_project_name = db.tbReview_Project_Name.Find(id);
            if (tbreview_project_name == null)
            {
                return HttpNotFound();
            }
            return View(tbreview_project_name);
        }

        //
        // POST: /ReviewProjectName/Edit/5

        [HttpPost]
        public ActionResult Edit(tbReview_Project_Name tbreview_project_name)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbreview_project_name).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbreview_project_name);
        }

        //
        // GET: /ReviewProjectName/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbReview_Project_Name tbreview_project_name = db.tbReview_Project_Name.Find(id);
            if (tbreview_project_name == null)
            {
                return HttpNotFound();
            }
            return View(tbreview_project_name);
        }

        //
        // POST: /ReviewProjectName/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbReview_Project_Name tbreview_project_name = db.tbReview_Project_Name.Find(id);
            db.tbReview_Project_Name.Remove(tbreview_project_name);
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