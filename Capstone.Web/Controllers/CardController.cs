using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CardCreate()
        {
            //dal.CreateCard

            return View();
        }

        public ActionResult CardSubmit()
        {
            //dal.CreateCard

            return View("CardView");
        }

        public ActionResult CardSearch(string searchString)
        {
            //dal.SearchCards

            //List<Card> as model

            return View("CardSearch");
        }

        public ActionResult CardView()
        {
            //dal.ViewAllCards

            //List<Card> as model

            return View();
        }

        public ActionResult CardModify()
        {
            //dal.ModifyCard

            return View();
        }

        public ActionResult CardToDeck()
        {
            //dal.AddCardToDeck

            return View("CardView");
        }
    }
}