using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HospitalRevisitSystem.ViewModels;
using System.Collections;

namespace HospitalRevisitSystem.Controllers
{
    public class InvestigationQuestionController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /InvestigationQuestion/

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
            var query = from s in db.tbInvestigation_Question
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Review_Content.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Investigation_Question_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /InvestigationQuestion/Details/5

        public ActionResult Details(int id = 0)
        {
            tbInvestigation_Question tbinvestigation_question = db.tbInvestigation_Question.Find(id);
            if (tbinvestigation_question == null)
            {
                return HttpNotFound();
            }
            return View(tbinvestigation_question);
        }

        //
        // GET: /InvestigationQuestion/Create

        public ActionResult Create()
        {
            ViewBag.Investigation_Question_ID = new SelectList(db.tbCaller_Question, "Caller_Question_ID", "Caller_Question_ID");
            return View();
        }

        //
        // POST: /InvestigationQuestion/Create

        [HttpPost]
        public ActionResult Create(tbInvestigation_Question tbinvestigation_question)
        {
            if (ModelState.IsValid)
            {
                db.tbInvestigation_Question.Add(tbinvestigation_question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Investigation_Question_ID = new SelectList(db.tbCaller_Question, "Caller_Question_ID", "Caller_Question_ID", tbinvestigation_question.Investigation_Question_ID);
            return View(tbinvestigation_question);
        }

        //
        // GET: /InvestigationQuestion/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbInvestigation_Question tbinvestigation_question = db.tbInvestigation_Question.Find(id);
            if (tbinvestigation_question == null)
            {
                return HttpNotFound();
            }
            ViewBag.Investigation_Question_ID = new SelectList(db.tbCaller_Question, "Caller_Question_ID", "Caller_Question_ID", tbinvestigation_question.Investigation_Question_ID);
            return View(tbinvestigation_question);
        }

        //
        // POST: /InvestigationQuestion/Edit/5

        [HttpPost]
        public ActionResult Edit(tbInvestigation_Question tbinvestigation_question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbinvestigation_question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Investigation_Question_ID = new SelectList(db.tbCaller_Question, "Caller_Question_ID", "Caller_Question_ID", tbinvestigation_question.Investigation_Question_ID);
            return View(tbinvestigation_question);
        }

        //
        // GET: /InvestigationQuestion/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbInvestigation_Question tbinvestigation_question = db.tbInvestigation_Question.Find(id);
            if (tbinvestigation_question == null)
            {
                return HttpNotFound();
            }
            return View(tbinvestigation_question);
        }

        //
        // POST: /InvestigationQuestion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInvestigation_Question tbinvestigation_question = db.tbInvestigation_Question.Find(id);
            db.tbInvestigation_Question.Remove(tbinvestigation_question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [HttpPost]        
        public JsonResult LoadQuestion()
        {
            ArrayList arr = new ArrayList();
            arr=InvestigationQuestionTree.getAllQuestions(db);           
            return new JsonResult { Data = arr };
        }
    }
}