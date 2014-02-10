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
    public class MessageSendPlanController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /MessageSendPlan/

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
            var query = from s in db.tbMessage_Send_Plan
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Phone_Number.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Message_Send_Plan_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /MessageSendPlan/Details/5

        public ActionResult Details(int id = 0)
        {
            tbMessage_Send_Plan tbmessage_send_plan = db.tbMessage_Send_Plan.Find(id);
            if (tbmessage_send_plan == null)
            {
                return HttpNotFound();
            }
            return View(tbmessage_send_plan);
        }

        //
        // GET: /MessageSendPlan/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MessageSendPlan/Create

        [HttpPost]
        public ActionResult Create(tbMessage_Send_Plan tbmessage_send_plan)
        {
            if (ModelState.IsValid)
            {
                db.tbMessage_Send_Plan.Add(tbmessage_send_plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbmessage_send_plan);
        }

        //
        // GET: /MessageSendPlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbMessage_Send_Plan tbmessage_send_plan = db.tbMessage_Send_Plan.Find(id);
            if (tbmessage_send_plan == null)
            {
                return HttpNotFound();
            }
            return View(tbmessage_send_plan);
        }

        //
        // POST: /MessageSendPlan/Edit/5

        [HttpPost]
        public ActionResult Edit(tbMessage_Send_Plan tbmessage_send_plan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbmessage_send_plan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbmessage_send_plan);
        }

        //
        // GET: /MessageSendPlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbMessage_Send_Plan tbmessage_send_plan = db.tbMessage_Send_Plan.Find(id);
            if (tbmessage_send_plan == null)
            {
                return HttpNotFound();
            }
            return View(tbmessage_send_plan);
        }

        //
        // POST: /MessageSendPlan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbMessage_Send_Plan tbmessage_send_plan = db.tbMessage_Send_Plan.Find(id);
            db.tbMessage_Send_Plan.Remove(tbmessage_send_plan);
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