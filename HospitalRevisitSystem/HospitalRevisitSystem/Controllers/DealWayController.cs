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
    public class DealWayController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /DealWay/

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
            var query = from s in db.tbDeal_Way
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Processing_Method.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Deal_Way_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /DealWay/Details/5

        public ActionResult Details(int id = 0)
        {
            tbDeal_Way tbdeal_way = db.tbDeal_Way.Find(id);
            if (tbdeal_way == null)
            {
                return HttpNotFound();
            }
            return View(tbdeal_way);
        }

        //
        // GET: /DealWay/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DealWay/Create

        [HttpPost]
        public ActionResult Create(tbDeal_Way tbdeal_way)
        {
            if (ModelState.IsValid)
            {
                db.tbDeal_Way.Add(tbdeal_way);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbdeal_way);
        }

        //
        // GET: /DealWay/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbDeal_Way tbdeal_way = db.tbDeal_Way.Find(id);
            if (tbdeal_way == null)
            {
                return HttpNotFound();
            }
            return View(tbdeal_way);
        }

        //
        // POST: /DealWay/Edit/5

        [HttpPost]
        public ActionResult Edit(tbDeal_Way tbdeal_way)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbdeal_way).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbdeal_way);
        }

        //
        // GET: /DealWay/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbDeal_Way tbdeal_way = db.tbDeal_Way.Find(id);
            if (tbdeal_way == null)
            {
                return HttpNotFound();
            }
            return View(tbdeal_way);
        }

        //
        // POST: /DealWay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDeal_Way tbdeal_way = db.tbDeal_Way.Find(id);
            db.tbDeal_Way.Remove(tbdeal_way);
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