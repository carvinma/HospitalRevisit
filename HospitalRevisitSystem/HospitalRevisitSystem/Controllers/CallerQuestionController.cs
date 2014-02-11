using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Collections;
using HospitalRevisitSystem.ViewModels;

namespace HospitalRevisitSystem.Controllers
{
    public class CallerQuestionController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /CallerQuestion/

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
            var query = from s in db.tbCaller_Question
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.tbCaller_Title.Caller_Title_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Caller_Question_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /CallerQuestion/Details/5

        public ActionResult Details(int id = 0)
        {
            tbCaller_Question tbcaller_question = db.tbCaller_Question.Find(id);
            if (tbcaller_question == null)
            {
                return HttpNotFound();
            }
            return View(tbcaller_question);
        }

        //
        // GET: /CallerQuestion/Create

        public ActionResult Create()
        {
            ArrayList arr = new ArrayList();
            arr = InvestigationQuestionTree.getAllQuestions(db);
            ViewBag.Questions = arr;
            ViewBag.Caller_Title_ID = new SelectList(db.tbCaller_Title, "Caller_Title_ID", "Caller_Title_Name");
            ViewBag.Caller_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content");
            return View();
        }

        //
        // POST: /CallerQuestion/Create

        [HttpPost]
        public ActionResult Create(tbCaller_Question tbcaller_question)
        {
            if (ModelState.IsValid)
            {
                db.tbCaller_Question.Add(tbcaller_question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Caller_Title_ID = new SelectList(db.tbCaller_Title, "Caller_Title_ID", "Caller_Title_Name", tbcaller_question.Caller_Title_ID);
            ViewBag.Caller_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content", tbcaller_question.Caller_Question_ID);
            return View(tbcaller_question);
        }

        //
        // GET: /CallerQuestion/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbCaller_Question tbcaller_question = db.tbCaller_Question.Find(id);
            if (tbcaller_question == null)
            {
                return HttpNotFound();
            }
            ViewBag.Caller_Title_ID = new SelectList(db.tbCaller_Title, "Caller_Title_ID", "Caller_Title_Name", tbcaller_question.Caller_Title_ID);
            ViewBag.Caller_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content", tbcaller_question.Caller_Question_ID);
            return View(tbcaller_question);
        }

        //
        // POST: /CallerQuestion/Edit/5

        [HttpPost]
        public ActionResult Edit(tbCaller_Question tbcaller_question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbcaller_question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Caller_Title_ID = new SelectList(db.tbCaller_Title, "Caller_Title_ID", "Caller_Title_Name", tbcaller_question.Caller_Title_ID);
            ViewBag.Caller_Question_ID = new SelectList(db.tbInvestigation_Question, "Investigation_Question_ID", "Review_Content", tbcaller_question.Caller_Question_ID);
            return View(tbcaller_question);
        }

        //
        // GET: /CallerQuestion/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbCaller_Question tbcaller_question = db.tbCaller_Question.Find(id);
            if (tbcaller_question == null)
            {
                return HttpNotFound();
            }
            return View(tbcaller_question);
        }

        //
        // POST: /CallerQuestion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCaller_Question tbcaller_question = db.tbCaller_Question.Find(id);
            db.tbCaller_Question.Remove(tbcaller_question);
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