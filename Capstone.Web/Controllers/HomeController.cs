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
<<<<<<< HEAD
            Session["userid"] = "7";
=======
            Session["userid"] = "2";
>>>>>>> 3ec9f63cc7473a69d7f3619db808e4334a177006

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