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
    public class VoicePlanController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /VoicePlan/

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
            var query = from s in db.tbVoice_Plan
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.File_Paths.ToLower().Contains(searchString.ToLower()) == true);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /VoicePlan/Details/5

        public ActionResult Details(int id = 0)
        {
            tbVoice_Plan tbvoice_plan = db.tbVoice_Plan.Find(id);
            if (tbvoice_plan == null)
            {
                return HttpNotFound();
            }
            return View(tbvoice_plan);
        }

        //
        // GET: /VoicePlan/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VoicePlan/Create

        [HttpPost]
        public ActionResult Create(tbVoice_Plan tbvoice_plan)
        {
            if (ModelState.IsValid)
            {
                db.tbVoice_Plan.Add(tbvoice_plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbvoice_plan);
        }

        //
        // GET: /VoicePlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbVoice_Plan tbvoice_plan = db.tbVoice_Plan.Find(id);
            if (tbvoice_plan == null)
            {
                return HttpNotFound();
            }
            return View(tbvoice_plan);
        }

        //
        // POST: /VoicePlan/Edit/5

        [HttpPost]
        public ActionResult Edit(tbVoice_Plan tbvoice_plan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbvoice_plan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbvoice_plan);
        }

        //
        // GET: /VoicePlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbVoice_Plan tbvoice_plan = db.tbVoice_Plan.Find(id);
            if (tbvoice_plan == null)
            {
                return HttpNotFound();
            }
            return View(tbvoice_plan);
        }

        //
        // POST: /VoicePlan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbVoice_Plan tbvoice_plan = db.tbVoice_Plan.Find(id);
            db.tbVoice_Plan.Remove(tbvoice_plan);
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