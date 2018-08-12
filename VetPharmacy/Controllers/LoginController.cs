using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VetPharmacy.Models;

namespace VetPharmacy.Controllers
{
    public class LoginController : Controller
    {
        VetPharmaDB db = new VetPharmaDB();
        // GET: Login
   
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserMe user, string ReturnUrl="")
        {
            UserMe e = db.UserMes.Where(a => a.UserName.Equals(user.UserName) && a.UserPassword.Equals(user.UserPassword)).FirstOrDefault();
            if(e!=null)
            {
                FormsAuthentication.SetAuthCookie(e.UserName, true);
                if(Url.IsLocalUrl(ReturnUrl))
                {
                   return Redirect(ReturnUrl);
                }
                else
                {

                return RedirectToAction("AddOrder", "Orders");
                }
            }
            else
            {
                RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserMe user)
        {
            db.UserMes.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login", "Login",null);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");   
        }
    }
}