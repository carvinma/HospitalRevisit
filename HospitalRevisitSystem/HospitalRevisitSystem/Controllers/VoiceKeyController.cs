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
    public class VoiceKeyController : Controller
    {
        private dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();

        //
        // GET: /VoiceKey/

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
            var query = from s in db.tbVoice_Key
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.Answer_Marked_No.ToLower().Contains(searchString.ToLower()) == true);
            }
            query = query.OrderByDescending(s => s.Voice_Key_ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /VoiceKey/Details/5

        public ActionResult Details(int id = 0)
        {
            tbVoice_Key tbvoice_key = db.tbVoice_Key.Find(id);
            if (tbvoice_key == null)
            {
                return HttpNotFound();
            }
            return View(tbvoice_key);
        }

        //
        // GET: /VoiceKey/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VoiceKey/Create

        [HttpPost]
        public ActionResult Create(tbVoice_Key tbvoice_key)
        {
            if (ModelState.IsValid)
            {
                db.tbVoice_Key.Add(tbvoice_key);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbvoice_key);
        }

        //
        // GET: /VoiceKey/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbVoice_Key tbvoice_key = db.tbVoice_Key.Find(id);
            if (tbvoice_key == null)
            {
                return HttpNotFound();
            }
            return View(tbvoice_key);
        }

        //
        // POST: /VoiceKey/Edit/5

        [HttpPost]
        public ActionResult Edit(tbVoice_Key tbvoice_key)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbvoice_key).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbvoice_key);
        }

        //
        // GET: /VoiceKey/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbVoice_Key tbvoice_key = db.tbVoice_Key.Find(id);
            if (tbvoice_key == null)
            {
                return HttpNotFound();
            }
            return View(tbvoice_key);
        }

        //
        // POST: /VoiceKey/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbVoice_Key tbvoice_key = db.tbVoice_Key.Find(id);
            db.tbVoice_Key.Remove(tbvoice_key);
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