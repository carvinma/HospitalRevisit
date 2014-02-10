using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HospitalRevisitSystem.ViewModels;

namespace HospitalRevisitSystem.Controllers
{
    public class ReferenceInformationController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /ReferenceInformation/

        public ActionResult Index2(string sortOrder, string currentFilter, string searchString, int? page)
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
            var query = from s in db.tbReference_Information
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Information_Content.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Reference_Information_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /ReferenceInformation/Details/5

        public ActionResult Details(int id = 0)
        {
            tbReference_Information tbreference_information = db.tbReference_Information.Find(id);
            if (tbreference_information == null)
            {
                return HttpNotFound();
            }
            return View(tbreference_information);
        }

        //
        // GET: /ReferenceInformation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReferenceInformation/Create

        [HttpPost]
        public ActionResult Create(tbReference_Information tbreference_information)
        {
            if (ModelState.IsValid)
            {
                db.tbReference_Information.Add(tbreference_information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbreference_information);
        }

        //
        // GET: /ReferenceInformation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbReference_Information tbreference_information = db.tbReference_Information.Find(id);
            if (tbreference_information == null)
            {
                return HttpNotFound();
            }
            return View(tbreference_information);
        }

        //
        // POST: /ReferenceInformation/Edit/5

        [HttpPost]
        public ActionResult Edit(tbReference_Information tbreference_information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbreference_information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbreference_information);
        }

        //
        // GET: /ReferenceInformation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbReference_Information tbreference_information = db.tbReference_Information.Find(id);
            if (tbreference_information == null)
            {
                return HttpNotFound();
            }
            return View(tbreference_information);
        }

        //
        // POST: /ReferenceInformation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbReference_Information tbreference_information = db.tbReference_Information.Find(id);
            db.tbReference_Information.Remove(tbreference_information);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index(int id = 0)
        {
            TreeModel treeList = new TreeModel();
            ReferenceInformationTree.getAllChildren(id, ref treeList,ref db);
            TreeModel tree = null;
            if (treeList.List.Count > 0)
            {
                tree = treeList.List[0];
            }
            return View(tree);
        }
    }
}