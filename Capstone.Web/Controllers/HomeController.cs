using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }

        public ActionResult Login()
        {
            return View("LoginView");
        }

        //POST: /User/Login
        [HttpPost]
        public ActionResult Login(User formValues)
        {
            //Lookup user
            UserSqlDAL dal = new UserSqlDAL(connectionString);
            User user = dal.GetUser(formValues.UserName);
            if (user.UserName == null)
            {
                //todo: count attempts
                return RedirectToAction("LoginView");
            }
            //bool correctPassword = user.CheckPassword(user, formValues.Password);
            //if (!correctPassword)
            //{
            //    //to do: count attempts
            //    return RedirectToAction("Login");
            //}

            Session["userid"] = user.UserName;
            Session["admin"] = user.IsAdmin;

            HttpCookie aCookie = new HttpCookie("user");
            aCookie.Values["userName"] = user.UserName;
            aCookie.Values["loggedIn"] = DateTime.Now.ToString();
            aCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(aCookie);

            return RedirectToAction("Index", "Home");
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