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

        [HttpGet]
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
            //attempt to retrieve provided email - cannot duplicate existing
            User newUser = newUserDAL.GetUser(model.Email);
            if (newUser.Email == null)
            {
                newUser.Email = model.Email;
                newUser.Password = model.Password;
                if (model.UserName == null)
                {
                    newUser.UserName = model.Email.Substring(0, model.Email.IndexOf('@'));
                }
                else
                {
                    newUser.UserName = model.UserName;
                }

                newUserDAL.Register(newUser);
            }
            else
            {
                ModelState.AddModelError("email-exists", "That email address exists, please contact Admin for password reset if needed.");
                return View("Register", model);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}