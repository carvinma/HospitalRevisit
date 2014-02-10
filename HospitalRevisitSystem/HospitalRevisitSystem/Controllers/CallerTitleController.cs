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
    public class CallerTitleController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /CallerTitle/

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
            var query = from s in db.tbCaller_Title
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Caller_Title_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Caller_Title_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /CallerTitle/Details/5

        public ActionResult Details(int id = 0)
        {
            tbCaller_Title tbcaller_title = db.tbCaller_Title.Find(id);
            if (tbcaller_title == null)
            {
                return HttpNotFound();
            }
            return View(tbcaller_title);
        }

        //
        // GET: /CallerTitle/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CallerTitle/Create

        [HttpPost]
        public ActionResult Create(tbCaller_Title tbcaller_title)
        {
            if (ModelState.IsValid)
            {
                db.tbCaller_Title.Add(tbcaller_title);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbcaller_title);
        }

        //
        // GET: /CallerTitle/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbCaller_Title tbcaller_title = db.tbCaller_Title.Find(id);
            if (tbcaller_title == null)
            {
                return HttpNotFound();
            }
            return View(tbcaller_title);
        }

        //
        // POST: /CallerTitle/Edit/5

        [HttpPost]
        public ActionResult Edit(tbCaller_Title tbcaller_title)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbcaller_title).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbcaller_title);
        }

        //
        // GET: /CallerTitle/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbCaller_Title tbcaller_title = db.tbCaller_Title.Find(id);
            if (tbcaller_title == null)
            {
                return HttpNotFound();
            }
            return View(tbcaller_title);
        }

        //
        // POST: /CallerTitle/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCaller_Title tbcaller_title = db.tbCaller_Title.Find(id);
            db.tbCaller_Title.Remove(tbcaller_title);
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