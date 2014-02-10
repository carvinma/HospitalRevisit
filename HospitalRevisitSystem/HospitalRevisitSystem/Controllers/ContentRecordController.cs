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
    public class ContentRecordController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /ContentRecord/

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
            var query = from s in db.tbContent_Record
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Superior_No.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Content_Record_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /ContentRecord/Details/5

        public ActionResult Details(int id = 0)
        {
            tbContent_Record tbcontent_record = db.tbContent_Record.Find(id);
            if (tbcontent_record == null)
            {
                return HttpNotFound();
            }
            return View(tbcontent_record);
        }

        //
        // GET: /ContentRecord/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ContentRecord/Create

        [HttpPost]
        public ActionResult Create(tbContent_Record tbcontent_record)
        {
            if (ModelState.IsValid)
            {
                db.tbContent_Record.Add(tbcontent_record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbcontent_record);
        }

        //
        // GET: /ContentRecord/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbContent_Record tbcontent_record = db.tbContent_Record.Find(id);
            if (tbcontent_record == null)
            {
                return HttpNotFound();
            }
            return View(tbcontent_record);
        }

        //
        // POST: /ContentRecord/Edit/5

        [HttpPost]
        public ActionResult Edit(tbContent_Record tbcontent_record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbcontent_record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbcontent_record);
        }

        //
        // GET: /ContentRecord/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbContent_Record tbcontent_record = db.tbContent_Record.Find(id);
            if (tbcontent_record == null)
            {
                return HttpNotFound();
            }
            return View(tbcontent_record);
        }

        //
        // POST: /ContentRecord/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbContent_Record tbcontent_record = db.tbContent_Record.Find(id);
            db.tbContent_Record.Remove(tbcontent_record);
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