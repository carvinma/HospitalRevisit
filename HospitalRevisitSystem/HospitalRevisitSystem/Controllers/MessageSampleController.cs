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
    public class MessageSampleController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /MessageSample/

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
            var query = from s in db.tbMessage_Sample
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Message_Content.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Message_Sample_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /MessageSample/Details/5

        public ActionResult Details(int id = 0)
        {
            tbMessage_Sample tbmessage_sample = db.tbMessage_Sample.Find(id);
            if (tbmessage_sample == null)
            {
                return HttpNotFound();
            }
            return View(tbmessage_sample);
        }

        //
        // GET: /MessageSample/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MessageSample/Create

        [HttpPost]
        public ActionResult Create(tbMessage_Sample tbmessage_sample)
        {
            if (ModelState.IsValid)
            {
                db.tbMessage_Sample.Add(tbmessage_sample);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbmessage_sample);
        }

        //
        // GET: /MessageSample/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbMessage_Sample tbmessage_sample = db.tbMessage_Sample.Find(id);
            if (tbmessage_sample == null)
            {
                return HttpNotFound();
            }
            return View(tbmessage_sample);
        }

        //
        // POST: /MessageSample/Edit/5

        [HttpPost]
        public ActionResult Edit(tbMessage_Sample tbmessage_sample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbmessage_sample).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbmessage_sample);
        }

        //
        // GET: /MessageSample/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbMessage_Sample tbmessage_sample = db.tbMessage_Sample.Find(id);
            if (tbmessage_sample == null)
            {
                return HttpNotFound();
            }
            return View(tbmessage_sample);
        }

        //
        // POST: /MessageSample/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMessage_Sample tbmessage_sample = db.tbMessage_Sample.Find(id);
            db.tbMessage_Sample.Remove(tbmessage_sample);
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