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
    public class RoleController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /Role/

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
            var query = from s in db.tbRole
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Role_Name.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Role_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id = 0)
        {
            tbRole tbrole = db.tbRole.Find(id);
            if (tbrole == null)
            {
                return HttpNotFound();
            }
            return View(tbrole);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(tbRole tbrole)
        {
            //if (ModelState.IsValid)
            //{
                db.tbRole.Add(tbrole);
                db.SaveChanges();
                return RedirectToAction("Index");
           // }

            //return View(tbrole);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbRole tbrole = db.tbRole.Find(id);
            if (tbrole == null)
            {
                return HttpNotFound();
            }
            return View(tbrole);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(tbRole tbrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbrole);
        }

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbRole tbrole = db.tbRole.Find(id);
            if (tbrole == null)
            {
                return HttpNotFound();
            }
            return View(tbrole);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbRole tbrole = db.tbRole.Find(id);
            db.tbRole.Remove(tbrole);
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