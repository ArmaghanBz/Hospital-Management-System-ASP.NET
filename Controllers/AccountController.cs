using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult login(Models.Membership model)
        {
            using(var context = new HMSEntities1())
            {
                bool isValid = context.user.Any(x => x.userName == model.userName && x.password == model.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.userName, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username and password");
            }
            return View();
        }
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signup(user model)
        {
            using(var context = new HMSEntities1())
            {
                context.user.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}