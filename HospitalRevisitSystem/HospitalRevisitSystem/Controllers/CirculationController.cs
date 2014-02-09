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
    public class CirculationController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /Circulation/

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
            var query = from s in db.tbCirculation
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.tbDepartment.Department_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Circulation/Details/5

        public ActionResult Details(int id = 0)
        {
            tbCirculation tbcirculation = db.tbCirculation.Find(id);
            if (tbcirculation == null)
            {
                return HttpNotFound();
            }
            return View(tbcirculation);
        }

        //
        // GET: /Circulation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Circulation/Create

        [HttpPost]
        public ActionResult Create(tbCirculation tbcirculation)
        {
            if (ModelState.IsValid)
            {
                db.tbCirculation.Add(tbcirculation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbcirculation);
        }

        //
        // GET: /Circulation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbCirculation tbcirculation = db.tbCirculation.Find(id);
            if (tbcirculation == null)
            {
                return HttpNotFound();
            }
            return View(tbcirculation);
        }

        //
        // POST: /Circulation/Edit/5

        [HttpPost]
        public ActionResult Edit(tbCirculation tbcirculation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbcirculation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbcirculation);
        }

        //
        // GET: /Circulation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbCirculation tbcirculation = db.tbCirculation.Find(id);
            if (tbcirculation == null)
            {
                return HttpNotFound();
            }
            return View(tbcirculation);
        }

        //
        // POST: /Circulation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCirculation tbcirculation = db.tbCirculation.Find(id);
            db.tbCirculation.Remove(tbcirculation);
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