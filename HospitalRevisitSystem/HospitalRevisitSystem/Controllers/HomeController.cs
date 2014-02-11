using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalRevisitSystem.ViewModels;
using System.Web.Security;
using System.Net;
using PagedList;

namespace HospitalRevisitSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        dbHospitalRevisitEntities db = new dbHospitalRevisitEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOn()
        {            
            return View();
        }
        public ActionResult Quit()
        {
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("LogOn");
        }
        [HttpPost]
        public ActionResult LogOn(tbStaff model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                string secretPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "SHA1");
                var result = db.tbStaff.Where(s => s.Staff_Name == model.Staff_Name).Where(s => s.Password == secretPassword);

                if (result.ToList().Count > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.Staff_Name, true);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["Staff_ID"] = result.ToList()[0].Staff_ID;
                        System.Web.HttpContext.Current.Session["Staff_Name"] = result.ToList()[0].Staff_Name;
                        System.Web.HttpContext.Current.Session["Second_Login_Tag"] = 0;
                        System.Web.HttpContext.Current.Session["Action_Name"] = "LogOn";
                        System.Web.HttpContext.Current.Session["Controller_Name"] = "Home";
                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "");

                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

    }
}
