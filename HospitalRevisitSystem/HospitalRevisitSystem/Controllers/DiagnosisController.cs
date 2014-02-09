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
    public class DiagnosisController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /Diagnosis/

        public ActionResult Index(int id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPatientID = id;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var query = from s in db.tbDiagnosis
                        where s.Patient_ID == id
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Rediagnose_Doctor.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderBy(s => s.Diagnosis_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Diagnosis/Details/5

        public ActionResult Details(int id = 0)
        {
            tbDiagnosis tbdiagnosis = db.tbDiagnosis.Find(id);
            if (tbdiagnosis == null)
            {
                return HttpNotFound();
            }
            return View(tbdiagnosis);
        }

        //
        // GET: /Diagnosis/Create

        public ActionResult Create(int id)
        {
            ViewBag.Patient_ID = id;
            return View();
        }

        //
        // POST: /Diagnosis/Create

        [HttpPost]
        public ActionResult Create(tbDiagnosis tbdiagnosis)
        {
            if (ModelState.IsValid)
            {
                db.tbDiagnosis.Add(tbdiagnosis);
                db.SaveChanges();
                return RedirectToAction("Index/" + tbdiagnosis.Patient_ID);
            }

            return View(tbdiagnosis);
        }

        //
        // GET: /Diagnosis/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbDiagnosis tbdiagnosis = db.tbDiagnosis.Find(id);
            if (tbdiagnosis == null)
            {
                return HttpNotFound();
            }
            return View(tbdiagnosis);
        }

        //
        // POST: /Diagnosis/Edit/5

        [HttpPost]
        public ActionResult Edit(tbDiagnosis tbdiagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbdiagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbdiagnosis);
        }

        //
        // GET: /Diagnosis/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbDiagnosis tbdiagnosis = db.tbDiagnosis.Find(id);
            if (tbdiagnosis == null)
            {
                return HttpNotFound();
            }
            return View(tbdiagnosis);
        }

        //
        // POST: /Diagnosis/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDiagnosis tbdiagnosis = db.tbDiagnosis.Find(id);
            db.tbDiagnosis.Remove(tbdiagnosis);
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