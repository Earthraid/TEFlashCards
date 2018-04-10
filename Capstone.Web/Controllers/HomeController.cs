using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("LoginView");
        }

        public ActionResult Register()
        {
            return View("RegisterView");
        }

        
        [HttpPost]
        public ActionResult Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterView", model);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}