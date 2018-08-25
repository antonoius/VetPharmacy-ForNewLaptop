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
   
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserMe user)
        {
            UserMe e = db.UserMes.Where(a => a.UserEmail.Equals(user.UserEmail) && a.UserPassword.Equals(user.UserPassword)).FirstOrDefault();
            if(e!=null)
            {
                
                Session["UserName"] = e.UserName;
                Session["UserEmail"] = e.UserEmail;
                Session["UserId"] = e.UserId;
                

                


                
                return RedirectToAction("ShiftPage", "Shifts", null);
         //       return View("ShiftPage", "Shifts", null);
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