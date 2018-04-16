using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {


        private string connectionString = ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString;
        // GET: Home
        public ActionResult Index()
        {
            //temporary user id
            Session["userid"] = "2";

            return View("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            UserSqlDAL userDal = new UserSqlDAL(connectionString);

            User user = userDal.GetUser(model.Email);

            // user does not exist or password is wrong
            //PROBLEM HERE WITH PASSWORD VERIFICATION?? user.Password contains a bunch of spaces after the password put into the login
            //With the password part commented out below, you can log in with ANY password and a valid email.
            if (user.Email == null || user.Password != model.Password)
            {
                ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                return View("Login", model);
            }
            Session["userid"] = user.Id;
            Session["admin"] = user.IsAdmin;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["userid"] = null;
            return View("Logout");
        }

        public ActionResult Register()
        {
            return View("Register");
        }


        [HttpPost]
        public ActionResult Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            UserSqlDAL newUserDAL = new UserSqlDAL(connectionString);
            User newUser = newUserDAL.GetUser(model.Email);

            newUser.Email = model.Email;
            newUser.UserName = model.UserName;
            newUser.Password = model.Password;

            newUserDAL.Register(newUser);
            
            return RedirectToAction("Index", "Home");
        }

    }
}