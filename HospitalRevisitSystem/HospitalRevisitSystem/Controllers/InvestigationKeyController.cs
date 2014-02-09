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
    public class InvestigationKeyController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /InvestigationKey/

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
            var query = from s in db.tbInvestigation_Key
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Answer_Content.ToLower().Contains(searchString.ToLower()) == true);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /InvestigationKey/Details/5

        public ActionResult Details(int id = 0)
        {
            tbInvestigation_Key tbinvestigation_key = db.tbInvestigation_Key.Find(id);
            if (tbinvestigation_key == null)
            {
                return HttpNotFound();
            }
            return View(tbinvestigation_key);
        }

        //
        // GET: /InvestigationKey/Create

        public ActionResult Create()
        {
            ViewBag.Iinvestigatio_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content");
            return View();
        }

        //
        // POST: /InvestigationKey/Create

        [HttpPost]
        public ActionResult Create(tbInvestigation_Key tbinvestigation_key)
        {
            if (ModelState.IsValid)
            {
                db.tbInvestigation_Key.Add(tbinvestigation_key);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Iinvestigatio_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content", tbinvestigation_key.Iinvestigatio_Question_ID);
            return View(tbinvestigation_key);
        }

        //
        // GET: /InvestigationKey/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbInvestigation_Key tbinvestigation_key = db.tbInvestigation_Key.Find(id);
            if (tbinvestigation_key == null)
            {
                return HttpNotFound();
            }
            ViewBag.Iinvestigatio_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content", tbinvestigation_key.Iinvestigatio_Question_ID);
            return View(tbinvestigation_key);
        }

        //
        // POST: /InvestigationKey/Edit/5

        [HttpPost]
        public ActionResult Edit(tbInvestigation_Key tbinvestigation_key)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbinvestigation_key).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Iinvestigatio_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content", tbinvestigation_key.Iinvestigatio_Question_ID);
            return View(tbinvestigation_key);
        }

        //
        // GET: /InvestigationKey/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbInvestigation_Key tbinvestigation_key = db.tbInvestigation_Key.Find(id);
            if (tbinvestigation_key == null)
            {
                return HttpNotFound();
            }
            return View(tbinvestigation_key);
        }

        //
        // POST: /InvestigationKey/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInvestigation_Key tbinvestigation_key = db.tbInvestigation_Key.Find(id);
            db.tbInvestigation_Key.Remove(tbinvestigation_key);
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